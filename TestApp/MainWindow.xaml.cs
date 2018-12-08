using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestLogic;
using System.Data;

namespace TestApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string server;
        string bd;
        List<Customer> customers_table = null;

        public MainWindow(string server, string bd)
        {
            this.server = server;
            this.bd = bd;

            InitializeComponent();

            customers_table = BD.SelectBD(server, bd, null);
            dgCustomers.ItemsSource = customers_table;
            lCount.Content = String.Format("Количество: {0}", customers_table.Count.ToString());
            lSum.Content = String.Format("Сумма: {0}", customers_table.Sum(x => x.income).ToString());
        }

        private void bSelect_Click(object sender, RoutedEventArgs e)
        {
            int? _age_from = null;
            if (!String.IsNullOrEmpty(tAgeFrom.Text)) _age_from = Convert.ToInt32(tAgeFrom.Text);
            int? _age_to = null;
            if (!String.IsNullOrEmpty(tAgeTo.Text)) _age_to = Convert.ToInt32(tAgeTo.Text);
            decimal? _income_from = null;
            if (!String.IsNullOrEmpty(tIncomeFrom.Text)) _income_from = Convert.ToDecimal(tIncomeFrom.Text);
            decimal? _income_to = null;
            if (!String.IsNullOrEmpty(tIncomeTo.Text)) _income_to = Convert.ToDecimal(tIncomeTo.Text);
            DateTime? _insert_date = null;
            if (!String.IsNullOrEmpty(dDateInsert.Text)) _insert_date = Convert.ToDateTime(dDateInsert.Text);

            CustomersCriteria criteria = new CustomersCriteria()
            {
                age_from = _age_from,
                age_to = _age_to,
                income_from = _income_from,
                income_to = _income_to,
                city = tCity.Text,
                insert_date = _insert_date,
                isLastMounth = chIsLastMounth.IsChecked.HasValue && chIsLastMounth.IsChecked.Value,
                isAgeSelect = chIsAgeSelect.IsChecked.HasValue && chIsAgeSelect.IsChecked.Value,
                isCitySelect = chIsCitySelect.IsChecked.HasValue && chIsCitySelect.IsChecked.Value
            };
            customers_table = BD.SelectBD(server, bd, criteria);
            dgCustomers.ItemsSource = customers_table;

            if (chIsAgeSelect.IsChecked.HasValue && chIsAgeSelect.IsChecked.Value)
            {
                dgCustomers.Columns[0].Visibility = Visibility.Collapsed;
                dgCustomers.Columns[1].Visibility = Visibility.Collapsed;
                dgCustomers.Columns[2].Visibility = Visibility.Visible;
                dgCustomers.Columns[3].Visibility = Visibility.Collapsed;
                dgCustomers.Columns[4].Visibility = Visibility.Collapsed;
            }
            else if (chIsCitySelect.IsChecked.HasValue && chIsCitySelect.IsChecked.Value)
            {
                dgCustomers.Columns[0].Visibility = Visibility.Collapsed;
                dgCustomers.Columns[1].Visibility = Visibility.Visible;
                dgCustomers.Columns[2].Visibility = Visibility.Collapsed;
                dgCustomers.Columns[3].Visibility = Visibility.Collapsed;
                dgCustomers.Columns[4].Visibility = Visibility.Collapsed;
            }
            else
            {
                dgCustomers.Columns[0].Visibility = Visibility.Visible;
                dgCustomers.Columns[1].Visibility = Visibility.Visible;
                dgCustomers.Columns[2].Visibility = Visibility.Visible;
                dgCustomers.Columns[3].Visibility = Visibility.Visible;
                dgCustomers.Columns[4].Visibility = Visibility.Visible;
            }

            lCount.Content = String.Format("Количество: {0}", customers_table.Count.ToString());
            lSum.Content = String.Format("Сумма: {0}", customers_table.Sum(x => x.income).ToString());
        }

        private void chIsLastMounth_Checked(object sender, RoutedEventArgs e)
        {
            dDateInsert.Text = "";
            dDateInsert.IsEnabled = false;
        }

        private void chIsLastMounth_Unchecked(object sender, RoutedEventArgs e)
        {
            dDateInsert.IsEnabled = true;
        }

        private void chIsCitySelect_Checked(object sender, RoutedEventArgs e)
        {
            chIsAgeSelect.IsChecked = false;
        }

        private void chIsAgeSelect_Checked(object sender, RoutedEventArgs e)
        {
            chIsCitySelect.IsChecked = false;
        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tF.Text == String.Empty)
            {
                MessageBox.Show("Введите фамилию!");
                return;
            }
            if (tI.Text == String.Empty)
            {
                MessageBox.Show("Введите имя!");
                return;
            }
            if (tAgeAdd.Text == String.Empty)
            {
                MessageBox.Show("Введите возраст!");
                return;
            }
            if (tCityAdd.Text == String.Empty)
            {
                MessageBox.Show("Введите город!");
                return;
            }
            if (tIncomeAdd.Text == String.Empty)
            {
                MessageBox.Show("Введите доход!");
                return;
            }

            try
            {
                Customer customer = new Customer(null, tF.Text + " " + tI.Text + ""+ tO.Text, tF.Text, tI.Text, tO.Text, tCityAdd.Text, Convert.ToInt32(tAgeAdd.Text),
                    Convert.ToDecimal(tIncomeAdd.Text), DateTime.Now);
                BD.InsertCustomer(server, bd, customer);
                customers_table.Add(customer);
                dgCustomers.ItemsSource = null;
                dgCustomers.ItemsSource = customers_table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось добавить нового сотрудника: " + ex.Message);
                return;
            }
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomers.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать сотрудника из таблицы!");
                return;
            }
            int id = (dgCustomers.SelectedItem as Customer).id.Value;
            if (id != 0)
                BD.DeleteCustomer(server, bd, id);
            customers_table.Remove(customers_table.Where(x => x.id == id).SingleOrDefault());
            dgCustomers.ItemsSource = null;
            dgCustomers.ItemsSource = customers_table;
        }
    }
}
