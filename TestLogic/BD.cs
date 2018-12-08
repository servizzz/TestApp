using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace TestLogic
{
    public class BD
    {
        public static List<string> GetDatabasesHere(string server)
        {
            List<string> dbList = new List<string>();
            SqlConnection myConn = null;
            try
            {
                using (myConn = new SqlConnection("Data Source=" + server + "\\SQLEXPRESS;Integrated security=SSPI;Initial Catalog=master"))
                {
                    using (SqlCommand myCommand = new SqlCommand("SELECT name FROM sys.databases", myConn))
                    {
                    
                            myConn.Open();
                            var reader = myCommand.ExecuteReader();
                            while (reader.Read())
                            {
                                dbList.Add(reader[0].ToString());
                            }

                    
                    }               
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (myConn.State == System.Data.ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
            return dbList;
        }
        
        public static void CreateBD(string server, string bd)
        {
            String str;
            SqlConnection myConn = null;
            try
            {
                using (myConn = new SqlConnection("Data Source = " + server + "\\SQLEXPRESS; Integrated security = SSPI; Initial Catalog= master"))
                {
                    str = "CREATE DATABASE " + bd + ";";
                    using (SqlCommand myCommand = new SqlCommand(str, myConn))
                    {
                        myConn.Open();
                        myCommand.ExecuteNonQuery();
                    }
                }
                using (myConn = new SqlConnection("Data Source = " + server + "\\SQLEXPRESS; Integrated security = SSPI; Initial Catalog=" + bd))
                {
                   
                    str = @"create table Customers
                        (
                            ID int identity not null,
                            F varchar(20) not null,
                            I varchar(20) not null,
                            O varchar(20),
                            INSERT_DATE date not null,

                        CONSTRAINT PK_Customers PRIMARY KEY(ID)
                        );
                        create table Params
                        (
                            ID int not null,
                            City varchar(20) not null,
                            Age int not null,
                            CONSTRAINT FK_Params_Customers FOREIGN KEY(ID)
                    
                            REFERENCES Customers(ID)                    
                            ON DELETE CASCADE                    
                            ON UPDATE CASCADE
                        );
                        create table Salary
                        (
                            ID int not null,
                            Income money not null,
                            CONSTRAINT FK_Salary_Customers FOREIGN KEY(ID)
                    
                            REFERENCES Customers(ID)                    
                            ON DELETE CASCADE                    
                            ON UPDATE CASCADE
                        );

                        insert into Customers(F, I, O, INSERT_DATE) Values
                        ('Иванов', 'Иван', 'Иванович', '05.11.2018')
                        ,('Петров', 'Петр', 'Петрович', '05.11.2018') 
                        ,('Кузнецов', 'Иван', 'Федорович', '05.11.2018') 
                        ,('Смирнов', 'Владимир', 'Филимонович', '05.11.2018') 
                        ,('Горбатов','Владимир','Александрович', '05.11.2018')
                        , ('Ахматова','Анна','Андреевна', '05.11.2018')
                        , ('Бьерн','Страуструп','', '05.11.2018')
                        , ('Рихтер','Джеффри','', '05.11.2018')
                        , ('Пушкин','Александр','Сергеевич', '05.11.2018')
                        , ('Тьюринг','Алан','', '05.12.2018')
                        , ('Чуковский','Корней','Иванович', '05.12.2018')
                        , ('Менделеев','Дмитрий','Иванович', '05.12.2018')
                        , ('Ульянов','Владимир','Ильич', '05.12.2018')
                        , ('Макконнелл','Стив','', '05.12.2018')
                        , ('Уэлльс','Герберт','', '05.12.2018')
                        , ('Шилдт','Герберт','', '05.12.2018')
                        , ('Лем','Станислав','', '05.12.2018')
                        , ('Толстой','Лев','Николаевич', '06.12.2018')
                        , ('Стругацкий','Аркадий','Натанович', '06.12.2018')
                        , ('Стругацкий','Борис','Натанович', '06.12.2018')
                        , ('Буденный','Семен','Михайлович', '06.12.2018')
                        , ('Ворошилов','Климент','Ефремович', '06.12.2018')
                        , ('Ушаков','Федор','Федорович', '06.12.2018')
                        , ('Гагарин','Юрий','Алексеевич', '06.12.2018')
                        , ('Годунов','Борис','Федорович', '06.12.2018')
                        , ('Ангаль-Цербтская','Софья','Августа-Фредерика', '06.12.2018')
                        , ('Пугачева','Анна','Борисовна', '07.12.2018')
                        , ('Дэвис','Анджела','Ивонна', '07.12.2018')
                        , ('Тесла','Никола','Милутинович', '07.12.2018')
                        , ('Филопатор','Клеопатра','', '07.12.2018')
                        , ('Нефертити','Нефер-неферу-атон','', '07.12.2018');

                        insert into Params values
                        (1, 'Самара', 19)
                        ,(2, 'Бердянск', 48)
                        ,(3, 'Кишинев', 37)
                        ,(4, 'Муром', 24)
                        ,(5, 'Вильнюс', 71)
                        ,(6, 'Санкт-Петербург', 18)
                        ,(7, 'Орхус', 67)
                        ,(8, 'Нью-Йорк', 54)
                        ,(9, 'Царское Село', 37)
                        ,(10, 'Гастингс', 41)
                        ,(11, 'Санкт-Петербург', 87)
                        ,(12, 'Тобольск', 32)
                        ,(13, 'Симбирск', 23)
                        ,(14, 'Вашингтон', 26)
                        ,(15, 'Лондон', 59)
                        ,(16, 'Чикаго', 47)
                        ,(17, 'Львов', 24)
                        ,(18, 'Тула', 22)
                        ,(19, 'Батуми', 36)
                        ,(20, 'Ленинград', 79)
                        ,(21, 'Ростов', 90)
                        ,(22, 'Екатеринослав', 29)
                        ,(23, 'Ярославль', 42)
                        ,(24, 'Гжатск', 67)
                        ,(25, 'Вязьма', 49)
                        ,(26, 'Щецин', 38)
                        ,(27, 'Москва', 25)
                        ,(28, 'Бирменгем', 87)
                        ,(29, 'Смилян', 44)
                        ,(30, 'Александрия', 17)
                        ,(31, 'Фивы', 17);

                        insert into salary values
                        (1, 50000)
                        ,(2, 123000)
                        ,(3, 73000)
                        ,(4, 185200)
                        ,(5, 23000)
                        ,(6, 13000)
                        ,(7, 45000)
                        ,(8, 27000)
                        ,(9, 33000)
                        ,(10, 7000)
                        ,(11, 123000)
                        ,(12, 23000)
                        ,(13, 155000)
                        ,(14, 12000)
                        ,(15, 26000)
                        ,(16, 78000)
                        ,(17, 56000)
                        ,(18, 177000)
                        ,(19, 258000)
                        ,(20, 45000)
                        ,(21, 67000)
                        ,(22, 156000)
                        ,(23, 230000)
                        ,(24, 73000)
                        ,(25, 89000)
                        ,(26, 190000)
                        ,(27, 173000)
                        ,(28, 34000)
                        ,(29, 13000)
                        ,(30, 22000)
                        ,(31, 28000); ";

                    using (SqlCommand myCommand = new SqlCommand(str, myConn))
                    {
                        myConn.Open();
                        myCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }
        
        public static List<Customer> SelectBD(string server, string bd, CustomersCriteria criteria)
        {
            List<Customer> dt = new List<Customer>();
            SqlConnection myConn = null;
            try
            {
                using (myConn = new SqlConnection("Data Source=" + server + "\\SQLEXPRESS;Integrated security=SSPI;Initial Catalog=" + bd))
                {
                    using (SqlCommand myCommand = new SqlCommand())
                    {
                        myCommand.Connection = myConn;
                        string query = "";
                        if ((criteria != null) && (criteria.isAgeSelect))
                        {
                            query += @"select top 3 id, age from Params where City in 
                                            (select City from Params p join Salary s on p.ID = s.ID group by City having avg(income) < @income_from) 
                                            order by age desc";
                            myCommand.Parameters.Add(new SqlParameter("@income_from", criteria.income_from ?? 0));
                            myCommand.CommandText = query;
                            myConn.Open();
                            var reader = myCommand.ExecuteReader();
                            while (reader.Read())
                            {
                                Customer customer = new Customer(Convert.ToInt32(reader["id"]), "", "", "", "", "", Convert.ToInt32(reader["age"]), 0, DateTime.Now);
                                dt.Add(customer);
                            }
                        }
                        else if ((criteria != null) && (criteria.isCitySelect))
                        {
                            query += @"select City from customers c join params p on c.ID = p.ID join salary s on c.id = s.id
                                        group by City having avg(income) > @income_to";
                            myCommand.Parameters.Add(new SqlParameter("@income_to", criteria.income_to ?? 0));
                            myCommand.CommandText = query;
                            myConn.Open();
                            var reader = myCommand.ExecuteReader();
                            while (reader.Read())
                            {
                                Customer customer = new Customer(0, "", "", "", "", reader["city"].ToString(), 0, 0, DateTime.Now);
                                dt.Add(customer);
                            }
                        }
                        else
                        {
                            query = @"select c.id, RTRIM(f + ' ' + i + ' ' + o) FIO, City, Age, Income, INSERT_DATE 
                                                  from customers c join params p on c.ID = p.ID join salary s on c.id = s.id ";
                            if (criteria != null)
                            {
                                query += " WHERE (1 = 1) ";
                                if (criteria.isLastMounth)
                                {
                                    query += @"AND insert_date between DATEADD(day, 1, DATEADD(month, -2, EOMONTH(getdate()))) 
                                                and DATEADD(month, -1, EOMONTH(getdate())) ";
                                }
                                else if (criteria.insert_date.HasValue)
                                {
                                    query += "AND INSERT_DATE = @insert_date ";
                                    myCommand.Parameters.Add(new SqlParameter("@insert_date", criteria.insert_date.Value));
                                }
                                if (criteria.age_from.HasValue)
                                {
                                    query += "AND age >= @age_from ";
                                    myCommand.Parameters.Add(new SqlParameter("@age_from", criteria.age_from.Value));
                                }
                                if (criteria.age_to.HasValue)
                                {
                                    query += "AND age <= @age_to ";
                                    myCommand.Parameters.Add(new SqlParameter("@age_to", criteria.age_to.Value));
                                }
                                if (criteria.income_from.HasValue)
                                {
                                    query += "AND income >= @income_from ";
                                    myCommand.Parameters.Add(new SqlParameter("@income_from", criteria.income_from.Value));
                                }
                                if (criteria.income_to.HasValue)
                                {
                                    query += "AND income <= @income_to ";
                                    myCommand.Parameters.Add(new SqlParameter("@income_to", criteria.income_to.Value));
                                }
                                if (!String.IsNullOrEmpty(criteria.city))
                                {
                                    query += "AND city = @city ";
                                    myCommand.Parameters.Add(new SqlParameter("@city", criteria.city));
                                }
                                
                            }
                            myCommand.CommandText = query;
                            myConn.Open();
                            var reader = myCommand.ExecuteReader();
                            while (reader.Read())
                            {
                                Customer customer = new Customer(Convert.ToInt32(reader["id"]), reader["fio"].ToString(), "", "", "", reader["city"].ToString(),
                                    Convert.ToInt32(reader["age"]), Convert.ToDecimal(reader["income"]), Convert.ToDateTime(reader["insert_date"]));
                                dt.Add(customer);
                            }
                        }
                        


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (myConn.State == System.Data.ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
            return dt;
        }

        public static void InsertCustomer(string server, string bd, Customer customer)
        {            
            SqlConnection myConn = null;
            try
            {
                using (myConn = new SqlConnection("Data Source=" + server + "\\SQLEXPRESS;Integrated security=SSPI;Initial Catalog=" + bd))
                {
                    using (SqlCommand myCommand = new SqlCommand())
                    {
                        string query = @"INSERT INTO CUSTOMERS(f, i, o, insert_date) 
                                            OUTPUT Inserted.ID VALUES (@f, @i, @o, @insert_date)";
                        myCommand.Connection = myConn;
                        myCommand.CommandText = query;
                        myCommand.Parameters.Add(new SqlParameter("@f",customer.f));
                        myCommand.Parameters.Add(new SqlParameter("@i", customer.i));
                        myCommand.Parameters.Add(new SqlParameter("@o", customer.o));
                        myCommand.Parameters.Add(new SqlParameter("@insert_date", customer.insert_date));
                        myConn.Open();
                        var reader = myCommand.ExecuteScalar();
                        customer.id = Convert.ToInt32(reader);

                        query = @"INSERT INTO PARAMS(id, age, city) VALUES (@id, @age, @city)";
                        myCommand.Connection = myConn;
                        myCommand.CommandText = query;
                        myCommand.Parameters.Clear();
                        myCommand.Parameters.Add(new SqlParameter("@id", customer.id));
                        myCommand.Parameters.Add(new SqlParameter("@age", customer.age));
                        myCommand.Parameters.Add(new SqlParameter("@city", customer.city));                      
                        myCommand.ExecuteNonQuery();

                        query = @"INSERT INTO SALARY(id, income) VALUES (@id, @income)";
                        myCommand.Connection = myConn;
                        myCommand.CommandText = query;
                        myCommand.Parameters.Clear();
                        myCommand.Parameters.Add(new SqlParameter("@id", customer.id));
                        myCommand.Parameters.Add(new SqlParameter("@income", customer.income));
                        myCommand.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (myConn.State == System.Data.ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        public static void DeleteCustomer(string server, string bd, int id)
        {
            SqlConnection myConn = null;
            try
            {
                using (myConn = new SqlConnection("Data Source=" + server + "\\SQLEXPRESS;Integrated security=SSPI;Initial Catalog=" + bd))
                {
                    using (SqlCommand myCommand = new SqlCommand())
                    {
                        string query = @"delete from CUSTOMERS where id = @id";
                        myCommand.Connection = myConn;
                        myCommand.CommandText = query;
                        myCommand.Parameters.Add(new SqlParameter("@id", id));
                        myConn.Open();                        
                        myCommand.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (myConn.State == System.Data.ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }
    }
}
