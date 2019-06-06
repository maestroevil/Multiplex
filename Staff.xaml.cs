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

using System.Windows.Media.Animation;
namespace Cursach
{
    /// <summary>
    /// Interaction logic for Staff.xaml
    /// </summary>
    public partial class Staff : Window
    {
        public bool burger_open = false;
        public Staff()
        {
            InitializeComponent();
        }

        private void burger_Click(object sender, RoutedEventArgs e)
        {
            if (burger_open == true)
            {
                Animation_Function_burger(0);
                burger_open = false;
            }
            else
            {
                Animation_Function_burger(450);
                burger_open = true;
            }

        }
        //Анимация панели
        private void Animation_Function_burger(float width)
        {

            DoubleAnimation buttonAnimation = new DoubleAnimation();
            buttonAnimation.From = burger_panel.ActualWidth;
            buttonAnimation.To = width;
            buttonAnimation.Duration = TimeSpan.FromSeconds(1.5);
            burger_panel.BeginAnimation(Button.WidthProperty, buttonAnimation);

        }

        private void insert_staff_Click(object sender, RoutedEventArgs e)
        {
            var insert_staff = new staff_list(0,0);
            insert_staff.Show();
            this.Close() ;
        }

        private void list_staff_Click(object sender, RoutedEventArgs e)
        {
            var list_staff = new Staff_list();
            list_staff.Show();
            this.Close();
        }

        private void Back_form_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow_form = new MainWindow();
            mainwindow_form.Show();
            this.Close();
        }
    }
}
