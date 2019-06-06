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
using System.Net;

using System.Windows.Media.Animation;
namespace Cursach

    
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    
    public partial class MainWindow : Window
    {
        public Staff _staff;
        public Table _table;
        public Information _information;
        //Переменная которая хранит в себе состояние открытости панели.
        public bool burger_open = false;
        public MainWindow()
        {
            InitializeComponent();
            //Центрируем окно
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;


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

        private void User_Click(object sender, RoutedEventArgs e)
        {

        
            
        }

        private void Staff_Click(object sender, RoutedEventArgs e)
        {
            _staff = new Staff();
            _staff.Show();
            this.Close();
        }

        private void Shedule_Click(object sender, RoutedEventArgs e)
        {
            _table = new Table();
            _table.Show();
            this.Close();
        }

        private void InformationCompany_Click(object sender, RoutedEventArgs e)
        {
            _information = new Information();
            _information.Show();
            this.Close();
        }

        private void Notes_Click(object sender, RoutedEventArgs e)
        {
            Note note = new Note();
            note.Show();
            this.Close();
        }
    }
}
