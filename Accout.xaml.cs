using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
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
using System.Data;

namespace Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy Accout.xaml
    /// </summary>
    public partial class Accout : Window
    {
        public int id;
        public string[] names { get; set; }
        public Accout()
        {
            InitializeComponent();

            {
                names = new string[] { "Wpłata środków", "Wypłata środków" };

                DataContext = this;
            }

            login_display.Text = ((MainWindow)Application.Current.MainWindow).login.Text;

        }

        private List<T> GetList<T>(IDataReader reader)
        {
            List<T> list = new List<T>();
            while(reader.Read())
            {
                var type = typeof(T);
                T obj = (T)Activator.CreateInstance(type);
                foreach(var prop in type.GetProperties())
                {
                    var propType = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType( reader[prop.Name].ToString(), propType));
                }
                list.Add(obj);
            }
            return list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            List<History> history = new List<History>();

            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppData;Integrated Security=True;"))
            {
                con.Open();

                string select_data = "SELECT [History].[IDLogin], [History].[Date], [History].[OldBalance], [History].[NewBalance] FROM [dbo].[LoginData] INNER JOIN [dbo].[History] ON [dbo].[LoginData].[ID]=[dbo].[History].[IDLogin] WHERE Login=@login;";
                SqlCommand cmd = new SqlCommand(select_data, con);

                cmd.Parameters.AddWithValue("@login", login_display.Text);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        History entry = new History();
                        entry.Idlogin = dataReader.GetInt32(0);
                        entry.Date = dataReader.GetDateTime(1);
                        entry.OldBalance = dataReader.GetInt32(2);
                        entry.NewBalance = dataReader.GetInt32(3);
                                                                     

                        history.Add(entry);
                    }
                }
            }

            if (history.Count > 0)
            {
                dgv.ItemsSource = history;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<Balance> balance = new List<Balance>();

            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppData;Integrated Security=True;"))
            {
                con.Open();

                string select_data = "SELECT [LoginData].[ID], [LoginData].[Login], [Balance].[Balance] FROM [dbo].[LoginData] INNER JOIN [dbo].[Balance] ON [dbo].[LoginData].[ID]=[dbo].[Balance].[IDLogin] WHERE Login=@login;";
                SqlCommand cmd = new SqlCommand(select_data, con);

                cmd.Parameters.AddWithValue("@login", login_display.Text);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Balance entry = new Balance();
                        entry.Idlogin = dataReader.GetInt32(0);
                        entry.Balance1 = dataReader.GetInt32(2);

                        balance.Add(entry);
                    }
                }
            }

            int account_balance = balance[0].Balance1;

            int textbox_value;

            if(combobox1.Text == "Wpłata środków")
            {
                try
                {
                    textbox_value = int.Parse(balance_textbox.Text);
                    int new_balance = account_balance + textbox_value;

                    if (new_balance < 0) { MessageBox.Show("Nie możesz mieć ujemnego salda!"); }
                    else 
                    { 
                        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppData;Integrated Security=True;");
                        con.Open();

                        string add_data = $"UPDATE [Balance] SET [Balance] = {new_balance} WHERE [IDLogin]={id};";
                        string add_history = "INSERT INTO [History] (IDLogin, [Date], OldBalance, NewBalance) VALUES (@idlogin, GETDATE(), @oldbalance, @newbalance);";
                        SqlCommand cmd = new SqlCommand(add_data, con);
                        cmd.ExecuteNonQuery();

                        SqlCommand cmd_history = new SqlCommand(add_history, con);
                        cmd_history.Parameters.AddWithValue("@idlogin", id);
                        cmd_history.Parameters.AddWithValue("@oldbalance", account_balance);
                        cmd_history.Parameters.AddWithValue("@newbalance", new_balance);
                        cmd_history.ExecuteNonQuery();



                        con.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Wpisz poprawną liczbę całkowitą!");
                }
            }
            else if(combobox1.Text == "Wypłata środków")
            {
                textbox_value = int.Parse(balance_textbox.Text);
                int new_balance = account_balance - textbox_value;

                if (new_balance < 0) { MessageBox.Show("Nie możesz mieć ujemnego salda!"); }
                else {
                    SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppData;Integrated Security=True;");
                    con.Open();

                    string add_data = $"UPDATE [Balance] SET [Balance] = {new_balance} WHERE [IDLogin]={id};";
                    string add_history = "INSERT INTO [History] (IDLogin, [Date], OldBalance, NewBalance) VALUES (@idlogin, GETDATE(), @oldbalance, @newbalance);";
                    SqlCommand cmd = new SqlCommand(add_data, con);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd_history = new SqlCommand(add_history, con);
                    cmd_history.Parameters.AddWithValue("@idlogin", id);
                    cmd_history.Parameters.AddWithValue("@oldbalance", account_balance);
                    cmd_history.Parameters.AddWithValue("@newbalance", new_balance);
                    cmd_history.ExecuteNonQuery();


                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Błąd!");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<Balance> balance = new List<Balance>();

            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppData;Integrated Security=True;"))
            {
                con.Open();

                string select_data = "SELECT [LoginData].[ID], [LoginData].[Login], [Balance].[Balance] FROM [dbo].[LoginData] INNER JOIN [dbo].[Balance] ON [dbo].[LoginData].[ID]=[dbo].[Balance].[IDLogin] WHERE Login=@login;";
                SqlCommand cmd = new SqlCommand(select_data, con);

                cmd.Parameters.AddWithValue("@login", login_display.Text);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Balance entry = new Balance();
                        entry.Idlogin = dataReader.GetInt32(0);
                        entry.Balance1 = dataReader.GetInt32(2);

                        balance.Add(entry);
                    }
                }
            }

            balance_display.Text = balance[0].Balance1.ToString();
            id = balance[0].Idlogin;
        }
    }
}
