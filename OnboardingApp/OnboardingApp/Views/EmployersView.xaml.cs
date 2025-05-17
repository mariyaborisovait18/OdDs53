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
using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using OnboardingApp.Models;
using OnboardingApp.Services;


namespace OnboardingApp.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployersView.xaml
    /// </summary>
    public partial class EmployersView : UserControl
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoEmployee.json";
        private BindableCollection<Employee> Employees = new();
        private FileOServices _fileOServices;

        public EmployersView()
        {
            InitializeComponent();



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {

            }
            catch (Exception ex)
            {


            }


        }



        private void _todoEmployee_ListChanged(object? sender, ListChangedEventArgs e)
        {


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }
    }
}
