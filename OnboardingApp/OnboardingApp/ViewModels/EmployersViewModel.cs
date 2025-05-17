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
using Stylet;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace OnboardingApp.ViewModels
{
    public class EmployersViewModel : Screen
    {
        public event EventHandler<EventArgs> GoToMainMenuEventHandler;

        public BindableCollection<Employee> Employees { get; set; }
        public BindableCollection<Employee> FilteredEmployees { get; set; } // Коллекция для отображения отфильтрованных сотрудников
        private readonly FileOServices _fileService;
        private readonly string _path;

        private string _newEmployeeId;
        private string _newEmployeeName;
        private string _newEmployeeDepartment;
        private string _positionSearchText; // Для поиска по должности

        public string NewEmployeeId
        {
            get => _newEmployeeId;
            set => SetAndNotify(ref _newEmployeeId, value);
        }

        public string NewEmployeeName
        {
            get => _newEmployeeName;
            set => SetAndNotify(ref _newEmployeeName, value);
        }

        public string NewEmployeeDepartment
        {
            get => _newEmployeeDepartment;
            set => SetAndNotify(ref _newEmployeeDepartment, value);
        }

        public string PositionSearchText // Свойство для хранения текста поиска
        {
            get => _positionSearchText;
            set
            {
                if (SetAndNotify(ref _positionSearchText, value))
                {
                    FilterEmployees(); // Фильтрация сотрудников при изменении текста поиска
                }
            }
        }

        private readonly IWindowManager _windowManager;

        public EmployersViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            _fileService = new FileOServices();
            _path = Path.Combine(Environment.CurrentDirectory, "todoEmployee.json");
            Employees = new BindableCollection<Employee>();
            FilteredEmployees = new BindableCollection<Employee>(); // Инициализация коллекции для отфильтрованных сотрудников
            LoadInitialEmployees();
        }

        private void LoadInitialEmployees()
        {
            Employees.Add(new Employee { Text1 = "Директор", Text2 = "Иванов Иван Иваныч", Text3 = "Отдел Разработки" });
            Employees.Add(new Employee { Text1 = "Тестировщик", Text2 = "Сидорова Мария Анатольевна", Text3 = "Отдел Тестирования" });
            Employees.Add(new Employee { Text1 = "Директор", Text2 = "Кузнецов Дмитрий Фёдорович", Text3 = "Отдел Маркетинга" });
            Employees.Add(new Employee { Text1 = "Программист", Text2 = "Смирнова Екатерина Олеговна", Text3 = "HR" });

            // Изначально показываем всех сотрудников
            FilteredEmployees.AddRange(Employees);
        }

        // Метод для фильтрации сотрудников по должности
        private void FilterEmployees()
        {
            if (string.IsNullOrEmpty(PositionSearchText))
            {
                FilteredEmployees.Clear();
                FilteredEmployees.AddRange(Employees); // Показываем всех сотрудников, если текст поиска пустой
            }
            else
            {
                // Фильтрация сотрудников, у которых должность соответствует тексту поиска
                var filtered = Employees
                    .Where(emp => emp.Text1 != null && emp.Text1.Contains(PositionSearchText, StringComparison.OrdinalIgnoreCase));
                FilteredEmployees.Clear();
                FilteredEmployees.AddRange(filtered);
            }
        }

        public void AddEmployeeCommand()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NewEmployeeId)
                    || string.IsNullOrWhiteSpace(NewEmployeeName)
                    || string.IsNullOrWhiteSpace(NewEmployeeDepartment))
                {
                    _windowManager.ShowMessageBox("Пожалуйста, заполните все поля для нового сотрудника.", "Ошибка ввода");
                    return;
                }

                if (Employees.Any(emp => emp.Text1 == NewEmployeeId))
                {
                    _windowManager.ShowMessageBox($"Сотрудник с ID '{NewEmployeeId}' уже существует.", "Ошибка ввода");
                    return;
                }

                Employee newEmployee = new Employee
                {
                    Text1 = NewEmployeeId,
                    Text2 = NewEmployeeName,
                    Text3 = NewEmployeeDepartment
                };

                Employees.Add(newEmployee);
                FilterEmployees(); // Обновляем отфильтрованный список
                SaveEmployees();
                NewEmployeeId = string.Empty;
                NewEmployeeName = string.Empty;
                NewEmployeeDepartment = string.Empty;
            }
            catch (Exception ex)
            {
                _windowManager.ShowMessageBox($"Произошла ошибка при добавлении сотрудника: {ex.Message}", "Ошибка");
            }
        }

        private void SaveEmployees()
        {
            try
            {
                _fileService.SaveData(Employees.ToList(), _path);
            }
            catch (Exception ex)
            {
                _windowManager.ShowMessageBox($"Ошибка сохранения сотрудников: {ex.Message}", "Ошибка сохранения");
            }
        }

        public void GoToMainMenuCommand()
        {
            GoToMainMenuEventHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}
