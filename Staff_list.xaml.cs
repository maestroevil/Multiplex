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
using System.Data.SqlClient;
namespace Cursach
{
    /// <summary>
    /// Interaction logic for Staff_list.xaml
    /// </summary>
    public partial class Staff_list : Window
    {
        public Staff_list()
        {
            
            InitializeComponent();
            viewListStaff();    
   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        //Выводим данные о сотрудниках
        void viewListStaff()
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Mydruy\\Desktop\\Курсовой\\Курсовой\\Cursach\\Multiplex.mdf;Integrated Security=True");
            con.Open();

            SqlCommand readCommand = new SqlCommand("SELECT * FROM [Staff]", con);
            StaticArray staticArray = new StaticArray();
            



            TableUser.Width = this.Width;

            Button b = new Button();
            b.Content = "22";
            b.Width = 100;
            b.Height = 100;

            TableUser.Rows = 17;
            TableUser.Columns = 4;
            using (SqlDataReader dataRead = readCommand.ExecuteReader())
            {
                if (dataRead != null)
                {
                    while (dataRead.Read())
                    {
                        Image image = new Image();
                        image.Width = 100;
                        image.Margin = new Thickness(0, 10, 0, 10);
                        if(dataRead["img"] == "")
                            image.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/Resource/user.png") as ImageSource;
                        else
                            image.Source = (new ImageSourceConverter()).ConvertFromString(dataRead["img"].ToString()) as ImageSource;
                        TableUser.Children.Add(image);

                        Label lab = new Label();

                        lab.Content = dataRead["name"].ToString() +" "+ dataRead["famil"].ToString()+" "+ dataRead["otch"].ToString();
                        lab.Foreground = System.Windows.Media.Brushes.White;
                        lab.Margin = new Thickness(0, 30, 0, 0);
                        TableUser.Children.Add(lab);
                        
                        Label lab1 = new Label();
                        lab1.Content = staticArray.stringPosition[Convert.ToInt32(dataRead["position"])];
                        lab1.Foreground = System.Windows.Media.Brushes.White;
                        lab1.Margin = new Thickness(0, 30, 0, 0);
                        TableUser.Children.Add(lab1);

                        Button c = new Button();
                        c.Tag = dataRead["id"];
                        c.Click += Pod_Click;
                        c.Content = "Подробно";
                        c.Width = 100;
                        c.Height = 30;

                        TableUser.Children.Add(c);
                        
                    }
                }
            }

            for (int i = 0; i < 100; i++)
            {
                
            }

        }

        private void Pod_Click(object sender, RoutedEventArgs e)
        {
            var button = (sender as Button);
            var id_user = Convert.ToInt32(button.Tag);

            staff_list insert_list_form = new staff_list(1,id_user);
            insert_list_form.Show();
            this.Close();
            
        }

        private void Back_form_Click(object sender, RoutedEventArgs e)
        {
            Staff staff_form = new Staff();
            staff_form.Show();
            this.Close();
        }
    }
}
