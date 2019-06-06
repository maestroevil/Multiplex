using System;

using System.Collections.Generic;
using System.Data.SqlClient;
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

using Microsoft.Win32;

namespace Cursach
{
    /// <summary>
    /// Interaction logic for staff_list.xaml
    /// </summary>
    public partial class staff_list : Window
    {
        //Определяет входные данные (0 - дефолт(запись), 1 - детальная информация, 2 - редактирование( );
        public int logic_model = 0;
        public int id_user = 0;
        public string filename = "";
        public  SqlConnection con;
        public staff_list(int lg_m,int id_u)
        {
            InitializeComponent();
            con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Mydruy\\Desktop\\Курсовой\\Курсовой\\Cursach\\Multiplex.mdf;Integrated Security=True");
            logic_model = lg_m;
            id_user = id_u;
            windowLoad();
        }
        private void windowLoad()
        {
            loadMainFunctionButton();
        }
        public void loadMainFunctionButton()
        {
            if (logic_model == 0)
            {
                MainFunctionFormButton.Content = "Записать сотрудника";
            }
            else
            {
                LoadUserInformation();
                MainFunctionFormButton.Content = "Редактировать запись";
            }
        }

        private void LoadUserInformation()
        {
            con.Open();
            if (id_user != 0)
            {
                SqlCommand readCommand = new SqlCommand("SELECT * FROM [Staff] WHERE id = '"+id_user+"'", con);
                readCommand.ExecuteNonQuery();
                
                using (SqlDataReader dataRead = readCommand.ExecuteReader())
                {
                    if (dataRead != null)
                    {
                        while (dataRead.Read())
                        {
                            Name.Text = dataRead["name"].ToString();
                            Famil.Text = dataRead["famil"].ToString();
                            Otch.Text = dataRead["otch"].ToString();
                            Place_life.Text = dataRead["city"].ToString();
                            date_3.Text = dataRead["date_day"].ToString() ;
                            date_2.Text = dataRead["date_mounth"].ToString();
                            date_1.Text = dataRead["date_year"].ToString();
                            Telephone.Text = dataRead["phone"].ToString();
                            
                            iu.Source = (new ImageSourceConverter()).ConvertFromString(dataRead["img"].ToString()) as ImageSource;
                            iu.Width = 100;
                            iu.Height = 300;

                            med_book.IsChecked = Convert.ToBoolean(dataRead["medical_book"]);
                            accommodation.SelectedIndex = Convert.ToInt32(dataRead["position"]);

                           // MessageBox.Show(dataRead["name"].ToString());
                        }
                    }
                }
            }
            con.Close();
        }
        private void MainFunctionForm(object sender, RoutedEventArgs e)
        {
            con.Open();
            if (logic_model == 0)
            {
                InserData();
            }
                
            else
            {
                DetalInfo();
            }
               
         
            


             con.Close();
        }
        private void InserData()
        {
            var isChecked = med_book.IsChecked.HasValue ? med_book.IsChecked.HasValue : false;
            SqlCommand fam;

            if (Name.Text != "" && Otch.Text != "" && Famil.Text != "" && Place_life.Text != "" && date_1.Text != "" && date_2.Text != "" && date_3.Text != "" && Telephone.Text != "" && accommodation.Text != "" && accommodation.SelectedIndex > -1)
            {
                fam = new SqlCommand("INSERT INTO[Staff](name, famil, otch, city, date_year, date_mounth, date_day, medical_book,phone,accommodation,position,img) VALUES(N'" + Name.Text + "', N'" + Famil.Text + "', N'" + Otch.Text + "', N'" + Place_life.Text + "', N'" + Convert.ToInt32(date_1.Text) + "',N'" + Convert.ToInt32(date_2.Text) + "', N'" + Convert.ToInt32(date_3.Text) + "', N'" + isChecked.ToString() + "',N'" + Telephone.Text + "',N'" + Place_life.Text + "',N'" + accommodation.SelectedIndex + "',N'" + filename + "');", con);
                fam.ExecuteNonQuery();
                MessageBox.Show("Сотрудник компании успешной записан!");
            }

            else
            {
                MessageBox.Show("Введите все данные");
            }
        }
        
        private void DetalInfo()
        {
            var isChecked = med_book.IsChecked.HasValue ? med_book.IsChecked.HasValue : false;
            SqlCommand fam;

            if (Name.Text != "" && Otch.Text != "" && Famil.Text != "" && Place_life.Text != "" && date_1.Text != "" && date_2.Text != "" && date_3.Text != "" && Telephone.Text != "" && accommodation.Text != "" && accommodation.SelectedIndex > -1)
            {
               
                fam = new SqlCommand("UPDATE [Staff] SET name = N'" + Name.Text + "',  famil = N'" + Famil.Text + "', otch = N'" + Otch.Text + "', city = N'" + Place_life.Text + "', date_year = "+ Convert.ToInt32(date_3.Text) + ", date_mounth =  "+ Convert.ToInt32(date_2.Text) + ", date_day =  "+ Convert.ToInt32(date_1.Text) + ", medical_book = '"+ isChecked.ToString() +"',phone = N'"+ Telephone.Text + "',accommodation = N'"+ Place_life.Text + "',position = "+ accommodation.SelectedIndex + " WHERE id='"+id_user+"'", con);
                fam.ExecuteNonQuery();
                MessageBox.Show("Сотрудник компании успешно изменён!");
            }

            else
            {
                MessageBox.Show("Введите все данные");
            }

        }
        OpenFileDialog dialog = new OpenFileDialog()
        {
            CheckFileExists = false,
            CheckPathExists = true,
            Multiselect = false,
            Title = "Выберите файл"
        };

        private void OpenFildDialog(object sender, RoutedEventArgs e)
        {
            if (dialog.ShowDialog() == true)
            {
                filename = dialog.FileName;
               
            }
        }

        private void Back_form_Click(object sender, RoutedEventArgs e)
        {
            Staff staff_form = new Staff();
            staff_form.Show();
            this.Close();
        }
    }
}
