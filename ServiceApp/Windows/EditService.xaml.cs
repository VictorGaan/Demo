using ServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace ServiceApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditService.xaml
    /// </summary>
    public partial class EditService : Window
    {
        ServicesContext servicesContext = new ServicesContext();
        Service serviceThis;
        public EditService()
        {
            Service service = new Service();
            DataContext = service;

            InitializeComponent();
            SaveChange.Click += SaveChange_Click;
        }
        public EditService(Service service)
        {
            serviceThis = service;
            DataContext = service;

            InitializeComponent();

            SaveChange.Click += SaveEditChange_Click;
        }
        private void SaveEditChange_Click(object sender, RoutedEventArgs e)
        {
            var query = servicesContext.Services.Where(t => t.Id == serviceThis.Id).First();
            query.Title = Title.Text;
            query.Cost = Convert.ToDecimal(Cost.Text, CultureInfo.InvariantCulture);
            query.DurationInSeconds = Convert.ToInt32(Duration.Text) * 60;
            query.Description = Description.Text;
            query.Discount = Convert.ToInt32(Discount.Text);
            servicesContext.SaveChanges();
            MessageBox.Show("Данные успешно сохранены");
            this.Close();
        }
        private void SaveChange_Click(object sender, RoutedEventArgs e)
        {
            var query = servicesContext.Services.Where(t => t.Title == Title.Text).Count();
            if (query != 0)
            {
                MessageBox.Show("Такая услуга уже существует!");
                return;
            }
            servicesContext.Services.Add(new Service{
                Title = Title.Text,
                Cost = Convert.ToInt32(Cost.Text),
                DurationInSeconds = Convert.ToInt32(Duration.Text) * 60,
                Description = Description.Text,
                Discount = Convert.ToInt32(Discount.Text),
                
            });
            servicesContext.SaveChanges();
        }
    }
}
