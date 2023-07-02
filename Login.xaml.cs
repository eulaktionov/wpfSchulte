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

namespace wpfSchulte
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        string password;
        public Login(string password)
        {
            InitializeComponent();
            this.password = password;
        }
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password != password)
            {
                DialogResult = false;
                MessageBox.Show("Wrong password", "Schulte Table");
            }
            else
            {
                DialogResult = true;
            }
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }

}
