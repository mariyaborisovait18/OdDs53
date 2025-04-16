using OnboardingApp.Views;
using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OnboardingApp.ViewModels
{
    public class MainMenuViewModel : Screen
    {
        private object _mainContent;

        public object MainContent
        {
            get => _mainContent;
            set
            {
                _mainContent = value;
                NotifyOfPropertyChange(() => MainContent);
            }
        }

        public ICommand ShowEmployeesCommand { get; private set; }

        public MainMenuViewModel()
        {
            // Инициализация команды для отображения сотрудников
            ShowEmployeesCommand = new RelayCommand(ShowEmployees);
        }

        private void ShowEmployees()
        {
            var employeesView = new EmployeesView();
            employeesView.DataContext = new EmployeesViewModel(); // Установка контекста данных для EmployeesView
            MainContent = employeesView; // Здесь мы устанавливаем содержимое
        }
    }

    // Реализация RelayCommand для применения ICommand
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
