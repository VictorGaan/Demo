using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.EntityFrameworkCore.Infrastructure;
using ServiceApp.Models;

namespace ServiceApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для ServicesListWindow.xaml
    /// </summary>
    /// 

    public partial class ServicesListWindow : Window
    {
        int maxSizeList;
        int allElemList;

        ObservableCollection<Service> services;

        public Visibility isAdmin { get; set; } = Visibility.Collapsed;

        ServicesContext context = new ServicesContext();
        public ServicesListWindow(bool roleValue)
        {
            DataContext = isAdmin;
            InitializeComponent();
            if (roleValue)
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
            var sources = context.Services.Select(t => t).ToList();
            services = new ObservableCollection<Service>(sources);

            maxSizeList = services.Count;
            allElemList = services.Count;
            ElemCount();

            ServicesList.ItemsSource = services;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int id = Convert.ToInt16(btn.Tag);
            var query = context.Services.Where(s => s.Id == id).FirstOrDefault();

            EditService editService = new EditService(query);
            editService.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Tag.ToString());

            var query = context.Services.Where(t => t.Id == id).First();
            services.Remove(query);
            context.Services.Remove(query);
            context.SaveChanges();

        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            EditService editService = new EditService();
            editService.ShowDialog();

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }
        private void Sort()
        {
            if(SortCombo == null) return;
            if (SortCombo.SelectedItem == DescCost)
            {
                ServicesList.ItemsSource = services.OrderByDescending(t => t.Cost).ToList();
            }
            else if(SortCombo.SelectedItem == AsdCost)
            {
                ServicesList.ItemsSource = services.OrderBy(t=>t.Cost).ToList();
            }
        }
        private void Filtration()
        {
            if(services == null) return;
            ObservableCollection<Service> queryServices;
            if(FiltrCombo.SelectedItem == Filtr0)
            {
                services = new ObservableCollection<Service>(context.Services.Select(t => t).ToList());
            }
            if(FiltrCombo.SelectedItem == Filtr5)
            {
                services = new ObservableCollection<Service>(context.Services.Where(t => t.Discount < 5 || t.Discount == null).ToList());
            }
            if (FiltrCombo.SelectedItem == Filtr15)
            {
                services = new ObservableCollection<Service>(context.Services.Where(t => t.Discount >= 5 && t.Discount < 15 ).ToList());
            }
            if (FiltrCombo.SelectedItem == Filtr30)
            {
                services = new ObservableCollection<Service>(context.Services.Where(t => t.Discount >= 15 && t.Discount < 30 ).ToList());
            }
            if (FiltrCombo.SelectedItem == Filtr70)
            {
                services = new ObservableCollection<Service>(context.Services.Where(t => t.Discount >= 30 && t.Discount < 70 ).ToList());
            }
            if(FiltrCombo.SelectedItem == Filtr100)
            {
                services = new ObservableCollection<Service>(context.Services.Where(t => t.Discount >= 70 && t.Discount < 100 ).ToList());
            }
            allElemList = services.Count;
            ServicesList.ItemsSource = services;
        }
        private void FiltrCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtration();
            ElemCount();
            Sort();
        }

        private void ElemCount()
        {
            if (services == null) return;
            Counter.Text = $"{allElemList} из {maxSizeList}";
        }

        private void SerachingText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtration();
            services = new ObservableCollection<Service>(services.Where(t => t.Title.ToLower().Contains(SerachingText.Text)).ToList());
            if(SerachingText.Text == "")
            {
                services = new ObservableCollection<Service>(context.Services.Select(t => t).ToList());
            }
            ServicesList.ItemsSource = services;
            Sort();
        }
    }
}
