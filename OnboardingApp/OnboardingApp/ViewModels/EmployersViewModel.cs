using HandyControl.Tools.Command;
using OnboardingApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

public class EmployeesViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Employee> _employees;
    private string _name;
    private string _department;

    public ObservableCollection<Employee> Employees
    {
        get => _employees;
        set
        {
            _employees = value;
            OnPropertyChanged(nameof(Employees));
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
            AddEmployeeCommand.RaiseCanExecuteChanged(); // Обновляем состояние команды при изменении имени
        }
    }

    public string Department
    {
        get => _department;
        set
        {
            _department = value;
            OnPropertyChanged(nameof(Department));
            AddEmployeeCommand.RaiseCanExecuteChanged(); // Обновляем состояние команды при изменении отдела
        }
    }

    public RelayCommand AddEmployeeCommand { get; }
    public RelayCommand<Employee> RemoveEmployeeCommand { get; }

    public EmployeesViewModel()
    {
        Employees = new ObservableCollection<Employee>();
        AddEmployeeCommand = new RelayCommand(AddEmployee, CanAddEmployee);
        RemoveEmployeeCommand = new RelayCommand<Employee>(RemoveEmployee);
    }

    private void AddEmployee()
    {
        if (CanAddEmployee())
        {
            var newEmployeeId = Employees.Count > 0 ? Employees[^1].Id + 1 : 1; // Уникальный ID
            Employees.Add(new Employee { Id = newEmployeeId, Name = Name, Department = Department });
            Name = string.Empty; // Очистка имени после добавления
            Department = string.Empty; // Очистка отдела после добавления
        }
    }

    private bool CanAddEmployee()
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Department);
    }

    private void RemoveEmployee(Employee employee)
    {
        if (employee != null)
        {
            Employees.Remove(employee);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}



