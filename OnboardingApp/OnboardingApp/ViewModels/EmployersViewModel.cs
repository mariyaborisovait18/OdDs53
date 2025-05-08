using HandyControl.Tools.Command;
using Stylet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnboardingApp.Models;
using OnboardingApp.Services;
using System.Windows.Documents;
using HandyControl.Controls;
using Newtonsoft.Json;
using System.IO;

namespace OnboardingApp.ViewModels
{
    public class EmployersViewModel : Screen, INotifyPropertyChanged
    {
        public EventHandler<EventArgs> GoToMainMenuEventHandler;

        public BindableCollection<Employee> Employees { get; set; } = new();

        private readonly FileOServices _fileService = new();
        private readonly string _path = $"{Environment.CurrentDirectory}\\todoEmployee.json";

        // Свойства для ввода нового сотрудника
        private string _newEmployeeId;
        private string _newEmployeeName;
        private string _newEmployeeDepartment;

        public string NewEmployeeId
        {
            get => _newEmployeeId;
            set
            {
                _newEmployeeId = value;
                OnPropertyChanged(nameof(NewEmployeeId));
            }
        }

        public string NewEmployeeName
        {
            get => _newEmployeeName;
            set
            {
                _newEmployeeName = value;
                OnPropertyChanged(nameof(NewEmployeeName));
            }
        }

        public string NewEmployeeDepartment
        {
            get => _newEmployeeDepartment;
            set
            {
                _newEmployeeDepartment = value;
                OnPropertyChanged(nameof(NewEmployeeDepartment));
            }
        }

        public EmployersViewModel()
        {
            try
            {
                //Employees = _fileService.LoadText(_path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddEmployeeCommand()
        {
            try
            {
                if (string.IsNullOrEmpty(NewEmployeeId) || string.IsNullOrEmpty(NewEmployeeName) || string.IsNullOrEmpty(NewEmployeeDepartment))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");

                }
                else
                {
                    Employee newEmployee = new();

                    newEmployee.Text1 = NewEmployeeId;
                    newEmployee.Text2 = NewEmployeeName;
                    newEmployee.Text3 = NewEmployeeDepartment;

                    Employees.Add(newEmployee);

                    SaveEmployees();

                    // Очистка полей после добавления
                    NewEmployeeId = string.Empty;
                    NewEmployeeName = string.Empty;
                    NewEmployeeDepartment = string.Empty;
                }
            }
            catch (Exception ex)
            {
                // Логирование или вывод сообщения об ошибке
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }



        private void SaveEmployees()
        {
            _fileService.SaveText(Employees, _path);
        }

        public void GoToMainMenuCommand()
        {
            GoToMainMenuEventHandler?.Invoke(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}