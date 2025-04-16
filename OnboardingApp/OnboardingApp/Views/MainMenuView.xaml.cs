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
using System.Windows.Controls;
using OnboardingApp.ViewModels;

namespace OnboardingApp.Views
{
    public partial class MainMenuView : UserControl
    {
        public MainMenuView()
        {
            InitializeComponent();
            DataContext = new MainMenuViewModel(); // Устанавливаем контекст данных
        }

        private void EmployeesList_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Выполните команду, если нужно
            var viewModel = DataContext as MainMenuViewModel;
            viewModel?.ShowEmployeesCommand.Execute(null); // Выполнение команды
        }
    }
}

