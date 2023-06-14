using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppData;Integrated Security=True;");
                con.Open();
                string check_data = "SELECT * FROM [dbo].[LoginData] where Login=@login and Password=@password";
                SqlCommand cmd = new SqlCommand(check_data, con);


                cmd.Parameters.AddWithValue("@login", login.Text);
                cmd.Parameters.AddWithValue("@password", password.Text);
                cmd.ExecuteNonQuery();

                int Count = Convert.ToInt32(cmd.ExecuteScalar());

                con.Close();

                login.Text = "";
                password.Text = "";
                if (Count>0)
                {
                    MainWindow w1 = new MainWindow();
                    this.Close();
                    w1.Show();
                }
                else
                {
                    MessageBox.Show("Login albo hasło jest niepoprawne!");
                }
            }
            catch
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Register r1 = new Register();
            this.Close();
            r1.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }


}
