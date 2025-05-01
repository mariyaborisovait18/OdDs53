using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using OnboardingApp.ViewModels;

namespace OnboardingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var cultureInfo = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
            base.OnStartup(e);
        }
    }

    public class AppViewModel // Или любое другое название
    {
        public MainMenuViewModel MainMenuViewModel { get; private set; }
        public KnowlegeBaseViewModel KnowledgeBaseViewModel { get; private set; }

        public AppViewModel()
        {
            MainMenuViewModel = new MainMenuViewModel();
            KnowledgeBaseViewModel = new KnowlegeBaseViewModel();
        }
    }
}