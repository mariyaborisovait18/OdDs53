using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Controls;
using HandyControl.Tools.Command;
using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnboardingApp.Models;


namespace OnboardingApp.ViewModels
{

    public class KnowlegeBaseViewModel : Screen, INotifyPropertyChanged
    {
        private readonly IArticleRepository _articleRepository;

        public ObservableCollection<Article> FilteredArticles { get; set; }
        public ObservableCollection<string> Categories { get; set; }

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                RefreshFilteredArticles();
            }
        }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                RefreshFilteredArticles();
            }
        }

        public ICommand RefreshCommand { get; }
        //
        public ICommand GoToMainMenuCommand { get; }

        public EventHandler<EventArgs> GoToMainMenuEventHandler;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void GoToMainMenu()
        {
            //Используйте событие для уведомления о переходе на главное меню
            GoToMainMenuEventHandler?.Invoke(this, EventArgs.Empty);
        }

        public KnowlegeBaseViewModel()
        {
            _articleRepository = new ArticleRepository();
            FilteredArticles = new ObservableCollection<Article>(_articleRepository.GetAllArticles());
            Categories = new ObservableCollection<string> { "Все", "Инструкции", "Правила" };

            RefreshCommand = new RelayCommand(_ => RefreshFilteredArticles());
            //
            GoToMainMenuCommand = new RelayCommand(_ => GoToMainMenu());


        }

        private void RefreshFilteredArticles()
        {
            var articles = _articleRepository.GetAllArticles();

            if (!string.IsNullOrEmpty(SearchText))
            {

                articles = articles.Where(a => a.Title != null && a.Title.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrEmpty(SelectedCategory) && SelectedCategory != "Все")
            {
                articles = articles.Where(a => a.Category == SelectedCategory);
            }

            FilteredArticles.Clear();
            foreach (var article in articles)
            {
                FilteredArticles.Add(article);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
        
    }

    // Реализация ICommand для команд (например, нажатие кнопки)
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

    }

}
