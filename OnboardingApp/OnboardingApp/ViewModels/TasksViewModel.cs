using Stylet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingApp.ViewModels
{
    public class TasksViewModel : Screen
    {
        //переключение на главное меню
        public EventHandler<EventArgs> GoToMainMenuEventHandler;

        public void GoToMainMenuCommand()
        {
            GoToMainMenuEventHandler?.Invoke(this, EventArgs.Empty);
        }
    }

    class TodoModel : INotifyPropertyChanged
    {
        public int Id { get; set; }  // Уникальный идентификатор задачи

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
