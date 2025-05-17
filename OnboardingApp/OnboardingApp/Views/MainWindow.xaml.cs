using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OnboardingApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем выбран ли элемент в ListBox
            if (BookList.SelectedItem is ListBoxItem selectedItem)
            {
                string pdfFilePath = selectedItem.Tag.ToString(); // Получаем путь к PDF файлу
                // Открываем PDF файл с помощью стандартного приложения
                Process.Start(new ProcessStartInfo(pdfFilePath) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите учебник для загрузки.");
            }
        }
    }
}
