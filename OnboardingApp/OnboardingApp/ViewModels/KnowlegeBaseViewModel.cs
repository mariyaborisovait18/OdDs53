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
using System.IO;
using System.Windows;
using System.Diagnostics;
using System.Security.Policy;


namespace OnboardingApp.ViewModels
{

    public class KnowlegeBaseViewModel : Screen, INotifyPropertyChanged
    {
        private readonly IArticleRepository _articleRepository;

        public ObservableCollection<Article> FilteredArticles { get; set; }
        public ObservableCollection<string> Categories { get; set; }

        public Article SelectedArticle { get; set; }

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

        private string _selectedCategory = "Всё";
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
        public ICommand OpenInstructionCommand { get; }
        public ICommand OpenRulesCommand { get; }
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
            Categories = new ObservableCollection<string> { "Все", "Учебники", "Правила" };

            RefreshCommand = new RelayCommand(_ => RefreshFilteredArticles());
            //
            GoToMainMenuCommand = new RelayCommand(_ => GoToMainMenu());
            //// Новый: Команды для открытия PDF
            //OpenInstructionCommand = new RelayCommand(_ => OpenPdf("Учебник по с++ Стефан Р. Дэвис.pdf"));
            //OpenRulesCommand = new RelayCommand(_ => OpenPdf("Учебник по с шарп М.А.Медведев.pdf"));
        }

        public void SelectedArticleChangedCommand()
        {
            if (SelectedArticle == null) return;
            OpenPdf(SelectedArticle.PdfFilePath);
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

        private void OpenPdf(string filename)
        {
            try
            {
                string pdfFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "PDF", filename);

                if (File.Exists(pdfFilePath))
                {
                    System.Diagnostics.Process.Start(new ProcessStartInfo(pdfFilePath) { UseShellExecute = true });
                }
                else
                {
                    MessageBox.Show("PDF файл не найден: " + pdfFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при открытии файла: " + ex.Message);
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

