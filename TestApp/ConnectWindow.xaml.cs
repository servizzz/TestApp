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
using System.Windows.Shapes;
using TestLogic;
using System.Data;

namespace TestApp
{
    /// <summary>
    /// Логика взаимодействия для ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
        List<string> bd_list = null;
        string server = "";
        string bd = "";

        public ConnectWindow()
        {
            InitializeComponent();
            cbServers.ItemsSource = SqlServer.GetServersList().DefaultView;
            cbServers.DisplayMemberPath = "ServerName";
            cbServers.SelectedIndex = 0;
        }

        private void cbServers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbServers.SelectedValue != null)
            {
                try
                {
                    server = ((DataRowView)cbServers.SelectedValue)["ServerName"].ToString();
                    bd_list = BD.GetDatabasesHere(server);
                    cbDB.ItemsSource = bd_list;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bConnect_Click(object sender, RoutedEventArgs e)
        {
            if (cbServers.SelectedValue == null)
            {
                MessageBox.Show("На компьютере не найдено Sql Servera! Работа программы невозможна!");
                return;
            }
            if (cbDB.Text == String.Empty)
            {
                MessageBox.Show("Необходимо выбрать имя загружаемой БД или задать новое!");
                return;
            }
            if (!bd_list.Contains(cbDB.Text))
            {
                try
                {
                    BD.CreateBD(server, cbDB.Text);
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
            MainWindow main = new MainWindow(server, cbDB.Text);
            main.Show();
            this.Close();
           
            
        }
    }
}
