using System;
using System.ComponentModel;

namespace TodoApp.Models
{
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
