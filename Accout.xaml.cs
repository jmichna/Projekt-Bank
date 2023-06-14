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
        public Accout()
        {
            InitializeComponent();

            //((MainWindow)Application.Current.MainWindow).login.Text

            siemka.Text = ((MainWindow)Application.Current.MainWindow).login.Text;


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
            //List<History> history = null;
            //SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppData;Integrated Security=True;");
            //con.Open();
            //string select_data = "SELECT * FROM [dbo].[History] where IDLogin=@login";
            //SqlCommand cmd = new SqlCommand(select_data, con);

            //siemka.Text = ((MainWindow)Application.Current.MainWindow).login.Text;

            //cmd.Parameters.AddWithValue("@login", siemka.Text);

            //var dataReader = cmd.ExecuteReader();
            //history=GetList<History>(dataReader);

            //if (history != null)
            //{
            //    dgv.DataContext = history;
            //}

            List<History> history = new List<History>(); // Zainicjalizuj listę historii

            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppData;Integrated Security=True;"))
            {
                con.Open();

                string select_data = "SELECT [History].[IDLogin], [History].[Date], [History].[OldBalance], [History].[NewBalance] FROM [dbo].[LoginData] INNER JOIN [dbo].[History] ON [dbo].[LoginData].[ID]=[dbo].[History].[IDLogin] WHERE Login=@login;";
                SqlCommand cmd = new SqlCommand(select_data, con);

                cmd.Parameters.AddWithValue("@login", siemka.Text);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        History entry = new History();
                        entry.Idlogin = dataReader.GetInt32(0); // Przyjmując, że ID jest pierwszą kolumną (indeks 0)
                        entry.Date = dataReader.GetDateTime(1); // Przyjmując, że Description jest drugą kolumną (indeks 1)
                        entry.OldBalance = dataReader.GetInt32(2);
                        entry.NewBalance = dataReader.GetInt32(3);
                                                                     

                        history.Add(entry); // Dodaj wpis do listy historii
                    }
                }
            }

            if (history.Count > 0)
            {
                dgv.ItemsSource = history; // Ustaw źródło danych dla DataGrid na listę historii
            }
        }
    }
}
