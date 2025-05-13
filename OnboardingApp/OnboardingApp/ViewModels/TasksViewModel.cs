using Stylet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnboardingApp.Services;
using System.IO;

namespace OnboardingApp.ViewModels
{
    public class TasksViewModel : Screen
    {
        private readonly FileIOService fileIOService;
        private BindingList<TodoModel> allTasks;

        public BindingList<TodoModel> OrganizationalTasks { get; set; } = new BindingList<TodoModel>();
        public BindingList<TodoModel> TechnicalTasks { get; set; } = new BindingList<TodoModel>();
        public BindingList<TodoModel> TrainingTasks { get; set; } = new BindingList<TodoModel>();

        public TasksViewModel()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "tasks.json");
            fileIOService = new FileIOService(path);
            LoadTasks();
        }

        private void LoadTasks()
        {
            allTasks = fileIOService.LoadData();

            OrganizationalTasks = new BindingList<TodoModel>(allTasks.Where(t => t.Category == "Организационные задачи").ToList());
            TechnicalTasks = new BindingList<TodoModel>(allTasks.Where(t => t.Category == "Технические задачи").ToList());
            TrainingTasks = new BindingList<TodoModel>(allTasks.Where(t => t.Category == "Обучающие задачи").ToList());

            foreach (var task in allTasks)
            {
                task.PropertyChanged += Task_PropertyChanged;
            }
        }

        private void Task_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsDone")
            {
                fileIOService.SaveData(allTasks);
            }
        }

        public EventHandler<EventArgs> GoToMainMenuEventHandler;

        public void GoToMainMenuCommand()
        {
            GoToMainMenuEventHandler?.Invoke(this, EventArgs.Empty);
        }
    }

    public class TodoModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        private bool isDone;
        private string text;
        private string category;

        public bool IsDone
        {
            get { return isDone; }
            set
            {
                if (isDone == value) return;
                isDone = value;
                OnPropertyChanged("IsDone");
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                if (text == value) return;
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public string Category
        {
            get { return category; }
            set
            {
                if (category == value) return;
                category = value;
                OnPropertyChanged("Category");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}