using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OnboardingApp.Views
{
    /// <summary>
    /// Логика взаимодействия для OfficeMapView.xaml
    /// </summary>
    public partial class OfficeMapView : UserControl
    {
        public OfficeMapView()
        {
            InitializeComponent();
            InitializeWebView2();
        }

        async void InitializeWebView2()
        {
            await myWebView2.EnsureCoreWebView2Async();
            try
            {
                string mapString = System.Text.Encoding.UTF8.GetString(Properties.Resources.mappast);
                myWebView2.NavigateToString(mapString);

            }
            catch (Exception ex)
            {
                // Обработка исключения, например, вывод сообщения об ошибке
                MessageBox.Show($"Ошибка при загрузке карты: {ex.Message}");
            }
        }
    }
}
