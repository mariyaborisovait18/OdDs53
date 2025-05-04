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

    public class EmployersViewModel : Screen, INotifyPropertyChanged // Реализуем INotifyPropertyChanged
    {
        //переключение на главное меню
        public EventHandler<EventArgs> GoToMainMenuEventHandler;

        private BindableCollection<Employee> Employees = new();

        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoEmployee.json";

        public EmployersViewModel()
        {
            try
            {
                Employees = LoadText(PATH);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private BindableCollection<Employee> LoadText(string PATH)
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindableCollection<Employee>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindableCollection<Employee>>(fileText);
            }

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



