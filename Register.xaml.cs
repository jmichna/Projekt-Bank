using Microsoft.Data.SqlClient;
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

namespace Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppData;Integrated Security=True;");
                con.Open();

                string add_data = "INSERT INTO [dbo].[LoginData] (Login, Password) VALUES (@login, @password);" +
                    "INSERT INTO [dbo].[Data] (IDLogin, Name, LastName) VALUES (SCOPE_IDENTITY(), @name, @lastname);" +
                    "INSERT INTO [dbo].[Balance] (IDLogin) VALUES (IDENT_CURRENT('[LoginData]'))";
                SqlCommand cmd = new SqlCommand(add_data, con);

                cmd.Parameters.AddWithValue("@login", register_login.Text);
                cmd.Parameters.AddWithValue("@password", register_password.Text);
                cmd.Parameters.AddWithValue("@name", register_name.Text);
                cmd.Parameters.AddWithValue("@lastname", register_lastname.Text);
                cmd.ExecuteNonQuery();


                con.Close();
                register_login.Text = "";
                register_password.Text = "";
                register_name.Text = "";
                register_lastname.Text = "";
                MainWindow w1 = new MainWindow();
                this.Close();
                w1.Show();
            }
            catch
            {
                MessageBox.Show("Błąd!");
            }
        }
    }
}
