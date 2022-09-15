using ServiceApp.Windows;
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

namespace ServiceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Sigin_Click(object sender, RoutedEventArgs e)
        {
            CodeWindow codeWindow = new CodeWindow();
            if (Admin.IsChecked == true)
            {
                if (codeWindow.ShowDialog() == true)
                {
                    ServicesListWindow window = new ServicesListWindow(true);
                }
               
            }
            else
            {
                ServicesListWindow window = new ServicesListWindow(false);

                window.Show();
                Close();
                
            }
           

            
        }
    }
}
