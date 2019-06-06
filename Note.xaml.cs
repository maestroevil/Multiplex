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
    /// Логика взаимодействия для Note.xaml
    /// </summary>
    public partial class Note : Window
    {
        public Note()
        {
            InitializeComponent();
            InitNote();
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Img_select.SelectedIndex > 0)
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Mydruy\\Desktop\\Курсовой\\Курсовой\\Cursach\\Multiplex.mdf;Integrated Security=True");
                con.Open();

                SqlCommand readCommand = new SqlCommand("INSERT  INTO [Note] (text,img) VALUES (N'" + textBox.Text + "'," + Img_select.SelectedIndex + ") " + button.Tag, con);
                readCommand.ExecuteNonQuery();
                MainWindow mw = new MainWindow();
                mw.Show();
            }
            else
                MessageBox.Show("Введите все данные");

        }
        private void Pod_Click(object sender, RoutedEventArgs e) {
            var button = (sender as Button);
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Mydruy\\Desktop\\Курсовой\\Курсовой\\Cursach\\Multiplex.mdf;Integrated Security=True");
            con.Open();

            SqlCommand readCommand = new SqlCommand("DELETE FROM [Note] WHERE id = "+ button.Tag, con);
            readCommand.ExecuteNonQuery();
            MessageBox.Show("Заметка успешно удалена");
            con.Close();

        }
        private void InitNote()
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Mydruy\\Desktop\\Курсовой\\Курсовой\\Cursach\\Multiplex.mdf;Integrated Security=True");
            con.Open();

            SqlCommand readCommand = new SqlCommand("SELECT * FROM [Note] ORDER BY id DESC", con);
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
                            image.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/Resource/note-"+ Convert.ToInt32(dataRead["img"]) + ".png") as ImageSource;
                        
                        //  image.Source = (new ImageSourceConverter()).ConvertFromString(dataRead["img"].ToString()) as ImageSource;
                        TableUser.Children.Add(image);

                        Label lab = new Label();

                        lab.Content = dataRead["text"].ToString();
                        lab.Foreground = System.Windows.Media.Brushes.White;
                        lab.Margin = new Thickness(0, 30, 0, 0);
                        TableUser.Children.Add(lab);

                        Label lab1 = new Label();
                        lab1.Content = "Одесский Офисс";
                        lab1.Foreground = System.Windows.Media.Brushes.White;
                        lab1.Margin = new Thickness(0, 30, 0, 0);
                        TableUser.Children.Add(lab1);

                        Button c = new Button();
                        c.Tag = dataRead["id"];
                        c.Click += Pod_Click;
                        c.Content = "Удалить";
                        c.Width = 100;
                        c.Height = 30;

                        TableUser.Children.Add(c);

                    }
                }
            }
        }
    }
}
