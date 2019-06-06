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
using System.Windows.Threading;

namespace Cursach
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    public partial class Table : Window
    {
        double windowWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
        double windowHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
        //Количество людей 
        public int users_count = 30;
        //Количество дней в месяце
        public int date_count = 31;
        //Переменная содержит в себе состояние графика(12,23 ...)
        public int keyboard_click_point = 0;
        String [] date_name =  new String [7] {"ПН","ВТ","СР","ЧТ","ПТ","СБ","ВС"};

        Dictionary<int, String> dataUsers = new Dictionary<int, String>();

        Dictionary<int, TextBox> dateTextBoxArray = new Dictionary<int, TextBox>();
        Dictionary<int, TextBox> usersTextBoxArray = new Dictionary<int, TextBox>();

        Dictionary<int, Label> workingDaysLabelArray = new Dictionary<int, Label>();
        Dictionary<int, Label> weekendDaysLabelArray = new Dictionary<int, Label>();

        //Массив для рассчетов, который содержит в себе значение ячеек
        List<List <int>> list = new List<List<int>>();

        public Table()
        {
            InitializeComponent();
            userConfiguration();
            
        }
        //ModaleWindow
        void initModaleWindow()
        {
            initMDSettingsDate();
        }
        void openModaleWindow(String tag)
        {

        }
        void closeModaleWindow(String tag)
        {

        }
        void initMDSettingsDate()
        {
            SettingsDate.Width = windowWidth/9;
            SettingsDate.Height = windowHeight / 9;

        }
        //LeftTopPanel
        void createTable()
        {
            int count_ = users_count * date_count;
            int x_table = 0;
            int y_table = 0;
            for (int i = 0; i < count_;i++ )
            {
                if(y_table == date_count)
                {
                    x_table++;
                    y_table = 0;
                }
                createYacheyka(0, x_table, y_table, "ClickTable");
                y_table++;
            }    
        }
        //RightTable
        void createRightTable()
        {
            RightTableWeekends.Width = TableShedule.Width / users_count - 2;
            RightTableWorkingDay.Width = TableShedule.Width / users_count - 2;
            for (int i = 0; i < users_count; i++ ) {
                RightTableWorkingDays(i);
                RightTableWeekend(i);
            }
            initWorkingDays();
            initWeekendgDays();
        }
        void RightTableWorkingDays(int id)
        {
            Label lb = new Label();
            workingDaysLabelArray[id] = lb;
            lb.HorizontalAlignment = HorizontalAlignment.Center;
            lb.Height = TableShedule.Height/ users_count;
            lb.Foreground = Brushes.White;
            RightTableWorkingDay.Children.Add(lb);
        }
        void RightTableWeekend(int id)
        {
            Label lb = new Label();
            weekendDaysLabelArray[id] = lb;
            lb.HorizontalAlignment = HorizontalAlignment.Center;
            lb.Height = TableShedule.Height / users_count;
            lb.Tag = 1;
            lb.Foreground = Brushes.White;
            RightTableWeekends.Children.Add(lb);
        }
        void initWorkingDays()
        {
            for (int i = 0; i < workingDaysLabelArray.Count; i++) {
                workingDaysLabelArray[i].Content = workingDaysLabelArray[i].Tag; 
            }
        }
        void initWeekendgDays()
        {
            for (int i = 0; i < weekendDaysLabelArray.Count; i++)
            {
                weekendDaysLabelArray[i].Content = weekendDaysLabelArray[i].Tag ;
            }
        }
        //TopDate
        void initValueDateInTextBox(int value)
        {
            int count = 7;
            int cd = 0;
            while (true)
            {
                if (value > 6)
                    value = 0;
                dateTextBoxArray[cd].Text = date_name[value];
                if (value == 5 || value == 6)
                    dateTextBoxArray[cd].Background = Brushes.Red;
                cd++;
                value++;
                if (cd > date_count-1)
                   break;
            }
        }
        void createNumberTopDateLabel(int numbers)
        {
            Label tb = new Label();
            tb.Content = numbers.ToString();
            tb.Width = (TableShedule.Width / date_count);
            tb.HorizontalContentAlignment = HorizontalAlignment.Center;
            tb.Foreground = Brushes.White;
            TopNumberDatePanel.Children.Add(tb);
        }
        void createTopDate() {
            TopNumberDatePanel.Width = TableShedule.Width;
            TopNumberDatePanel.Orientation = Orientation.Horizontal;

            TopDatePanel.Orientation = Orientation.Horizontal;
            
            for (int i = 0; i < date_count; i++) {
                createNumberTopDateLabel(i+1);
                createTopDateTextBox(i);
            }
        }
        void createTopDateTextBox(int id)
        {
     
            TextBox tb = new TextBox();
            dateTextBoxArray.Add(id,tb);
            tb.Background = Brushes.Gray;
            tb.Foreground = Brushes.White;
            tb.Height = TableShedule.Height / users_count ;
            tb.Width = TableShedule.Width / date_count;
            tb.HorizontalContentAlignment = HorizontalAlignment.Center;
            tb.VerticalContentAlignment = VerticalAlignment.Center;
            TopDatePanel.Children.Add(tb);            
        }
        //EndTopDate
        
        //LeftTable
        void createLeftTable()
        {
            
            
            for (int i = 0; i < users_count; i++)
            {
                createUserTextBox(i);
            }
            initValueTextBox();
        }
        void createUserTextBox(int id)
        {
            TextBox tb = new TextBox();
            usersTextBoxArray[id] = tb;
            tb.Width = windowWidth*0.18;
            tb.Height = TableShedule.Height / users_count;
            tb.Foreground = Brushes.Black;
            LeftTable.Children.Add(tb);
        }
        //Функция для считывания с БД
        void initValueTextBox()
        {
            string[] name = new string[10] { "Евгений Голопотылюк", "Кротова Екатерина","Олег Абдулаевич","Жизнев Дмитро","Волков Богдан","Сюзанский Илья","Никита Витов","Пигида Максим","Арнаутова Анна","Стратов Стас"};
            for (int i = 0; i < usersTextBoxArray.Count; i++)
                if (i < 10)
                    dataUsers[i] = name[i];
                else
                    dataUsers[i] = "";
                
            for (int i = 0; i < usersTextBoxArray.Count; i++)
                usersTextBoxArray[i].Text = dataUsers[i];

        }
        //EndLeftTable

        //SheduleTable
        //Возвращает значения X or Y с Tag
        public int tagsXY(string tag, int val) {
            
            int index = tag.IndexOf("|");
            string x = tag.Remove(index,tag.Length - index);
            string y = tag.Remove(0, index+1);
            return val == 0 ? Convert.ToInt16(x) : Convert.ToInt16(y);
        }
        void updateWeekend(int x,int days)
        {
            weekendDaysLabelArray[x].Content = days;
        }
        void createYacheyka(int type, int x, int y,string click)
        {
            list.Add(new List<int>());
            Button button = new Button();
            button.Foreground = Brushes.White;
            button.Tag = x.ToString() + "|" + y.ToString();
            list[x].Add(type);
            button.Click += tableYacheyka_Click;
            if (type == 0)
            {
                button.Background = Brushes.White;
                button.Content = "";
            }
            //12
            if (type == 1)
            {
                button.Background = Brushes.Blue;
                button.Content = "12";
                
            }
            //23    
            if(type == 2)
            {
                button.Background = Brushes.DarkOliveGreen;
                button.Content = "23";
                
            }
            if (type == 3)
            {
                button.Background = Brushes.Black;
                button.Content = "34";
            }
            if (type == 4)
            {
                button.Background = Brushes.Olive;
                button.Content = "123";
            }
            if (type == 5)
            {
                button.Background = Brushes.Green;
                button.Content = "234";
            }
            
            TableShedule.Children.Add(button);
        }
        private void tableYacheyka_Click(object sender, RoutedEventArgs e)
        {
            
            var button = (sender as Button);
            var tag = button.Tag.ToString();
            var tagX = tagsXY(tag,0);
            var tagY = tagsXY(tag, 1);
            //MessageBox.Show(tag);
            //MessageBox.Show( tagsXY(tag,1).ToString());
           // updateWeekend(tagX,20);
            if (keyboard_click_point == 0)
            {
                button.Background = Brushes.White;
                button.Content = "";
            }
            //12
            if (keyboard_click_point == 1)
            {
                button.Background = Brushes.Blue;
                button.Content = "12";

            }
            //23    
            if (keyboard_click_point == 2)
            {
                button.Background = Brushes.DarkOliveGreen;
                button.Content = "23";

            }
            if (keyboard_click_point == 3)
            {
                button.Background = Brushes.Black;
                button.Content = "34";
            }
            if (keyboard_click_point == 4)
            {
                button.Background = Brushes.Olive;
                button.Content = "123";
            }
            if (keyboard_click_point == 5)
            {
                button.Background = Brushes.Green;
                button.Content = "234";
            }
        }

        //EndSheduleTable
        void userConfiguration()
        {
            rectangleTop.Width = windowWidth;
            TableShedule.Width = windowWidth - (windowWidth * 0.3) ;
            TableShedule.Height = windowHeight - 100;

            TableShedule.Rows = users_count;
            TableShedule.Columns = date_count;

            createTable();
            createLeftTable();
            createTopDate();
           //reateRightTable();
            initModaleWindow();       


        }
        //EventClick
        public void KeyDown_Event(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D0)
                keyboard_click_point = 0;
            if (e.Key == Key.D1)
                keyboard_click_point = 1;
            if (e.Key == Key.D2)
                keyboard_click_point = 2;
            if (e.Key == Key.D3)
                keyboard_click_point = 3;
            if (e.Key == Key.D4)
                keyboard_click_point = 4;
            if (e.Key == Key.D5)
                keyboard_click_point = 5;
        }

        private void SettingsDateOpenMD_Click(object sender, RoutedEventArgs e)
        {
            initValueDateInTextBox(3);
        }

        private void Back_form_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainw_form = new MainWindow();
            mainw_form.Show();
            this.Close();
        }
    }
}


