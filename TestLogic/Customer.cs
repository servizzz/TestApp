using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLogic
{
    public class Customer
    {
        public int? id { get; set; }
        public string fio { get; set; }
        public string f { get; set; }
        public string i { get; set; }
        public string o { get; set; }
        public string city { get; set; }
        public int age { get; set; }
        public decimal income { get; set; }
        public DateTime insert_date { get; set; }

        public Customer(int? id, string fio, string f, string i, string o, string city, int age, decimal income, DateTime insert_date)
        {
            this.id = id;
            this.fio = fio;
            this.f = f;
            this.i = i;
            this.o = o;
            this.city = city;
            this.age = age;
            this.income = income;
            this.insert_date = insert_date;
        }
    }
}
