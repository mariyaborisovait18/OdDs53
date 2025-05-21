using OnboardingApp.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace OnboardingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }
    public class AppViewModel // Или любое другое название
    {
        public MainMenuViewModel MainMenuViewModel { get; private set; }
        public KnowlegeBaseViewModel KnowledgeBaseViewModel { get; private set; }

        public AppViewModel()
        {
            MainMenuViewModel = new MainMenuViewModel();
            KnowledgeBaseViewModel = new KnowlegeBaseViewModel();

            // Подписка на событие
            KnowledgeBaseViewModel.GoToMainMenuEventHandler += MainMenuViewModel.OnGoToMainMenu;
        }
    }
}