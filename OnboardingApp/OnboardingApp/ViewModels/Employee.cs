using Stylet;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingApp.Models
{
    public class Employee : INotifyPropertyChanged
    {
        private string _text1;
        private string _text2;
        private string _text3;

        public string Text1
        {
            get { return _text1; }
            set
            {
                if (_text1 == value)
                    return;
                _text1 = value;
                OnPropertyChanged(nameof(Text1));
            }
        }

        public string Text2
        {
            get { return _text2; }
            set
            {
                if (_text2 == value)
                    return;
                _text2 = value;
                OnPropertyChanged(nameof(Text2));
            }
        }

        public string Text3
        {
            get { return _text3; }
            set
            {
                if (_text3 == value)
                    return;
                _text3 = value;
                OnPropertyChanged(nameof(Text3));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class EmployeesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Employee> _employees = new();

        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        // Реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


