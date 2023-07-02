using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace wpfSchulte
{
    public partial class MainWindow : Window
    {
        List<Brush> colors = new List<Brush>()
        { 
            Brushes.White, Brushes.Yellow, Brushes.LightBlue, Brushes.LightGreen,
            Brushes.LightGray, Brushes.LightPink, Brushes.LightGoldenrodYellow,
            Brushes.LightCoral, Brushes.LightCyan, Brushes.LightGoldenrodYellow,
            Brushes.LightSalmon, Brushes.LightSteelBlue, Brushes.Lime, Brushes.Magenta
        };
        Random random = new();

        int side;
        int nextNumber;
        DateTime startTime;

        public MainWindow()
        {
            InitializeComponent();

            closeButton.Click += (s, e) => Close();
            combo.SelectedIndex = 0;
            Login login = new Login("it's me");
            if (login.ShowDialog()==false)
            {
                Close();
            }
        }

        void CreateTable()
        {
            if (grid is null)
            {
                grid = new Grid();
            } 
            else
            {
                grid.ColumnDefinitions.Clear();
                grid.RowDefinitions.Clear();
                //grid.Children.Clear();
            }

            for(int i = 0; i < side; i++)
            {
                ColumnDefinition col = new()
                {
                    Width = new GridLength(1, GridUnitType.Star)
                };
                grid.ColumnDefinitions.Add(col);
            }
            for(int i = 0; i < side; i++)
            {
                RowDefinition row = new ()
                {
                    Height = new GridLength(1, GridUnitType.Star)
                };
                grid.RowDefinitions.Add(row);
            }
            List<int> numbers = Enumerable.Range
                (1, side * side).OrderBy(x => random.Next()).ToList();
            int k = 0;

            for(int j = 0; j < side; j++)
            {
                for(int i = 0; i < side; i++)
                {
                    Label cell = new()
                    {
                        Background = colors[random.Next(colors.Count)],
                        FontSize = 30,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Content = numbers[k++]
                    };
                    cell.MouseDown += Cell_MouseDown;
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);
                    grid.Children.Add(cell);
                }
            }
        }

        private void Cell_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int number = int.Parse((sender as Label).Content.ToString());
            if(number == nextNumber)
            {
                SystemSounds.Beep.Play();
                nextNumber++;
                if(nextNumber > side * side)
                {
                    SystemSounds.Exclamation.Play();
                    int totalSecons =
                        (int)(DateTime.Now - startTime).TotalSeconds;
                    string message = string.Format
                        ($"Ваше время: {totalSecons} секунд");
                    MessageBox.Show(this, message, "Тест");
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = (sender as ComboBox).SelectedItem;
            string value = (item as ComboBoxItem).Content.ToString();
            side = Convert.ToInt32(value);
            CreateTable();
            nextNumber = 1;
            startTime = DateTime.Now;
        }
    }
}
