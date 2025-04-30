using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp
{
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoDataList.json";

        private BindingList<TodoModel> _organizationalTasks;
        private BindingList<TodoModel> _technicalTasks;
        private BindingList<TodoModel> _trainingTasks;

        private FileIOService _fileIOService;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);

            try
            {
                var allTasks = _fileIOService.LoadData();

                _organizationalTasks = new BindingList<TodoModel>(allTasks.Where(t => t.Category == "Знакомство с компанией").ToList());
                _technicalTasks = new BindingList<TodoModel>(allTasks.Where(t => t.Category == "Знакомство с проектом").ToList());
                _trainingTasks = new BindingList<TodoModel>(allTasks.Where(t => t.Category == "Знакомство с командой").ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
                return;
            }

            dgOrganizational.ItemsSource = _organizationalTasks;
            dgTechnical.ItemsSource = _technicalTasks;
            dgTraining.ItemsSource = _trainingTasks;

            _organizationalTasks.ListChanged += AnyListChanged;
            _technicalTasks.ListChanged += AnyListChanged;
            _trainingTasks.ListChanged += AnyListChanged;
        }

        private void AnyListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemChanged || e.ListChangedType == ListChangedType.ItemDeleted)
            {
                try
                {
                    var allTasks = _organizationalTasks.Concat(_technicalTasks).Concat(_trainingTasks).ToList();
                    _fileIOService.SaveData(allTasks);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }


        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrganizational.SelectedItem is TodoModel selectedOrg)
                _organizationalTasks.Remove(selectedOrg);
            else if (dgTechnical.SelectedItem is TodoModel selectedTech)
                _technicalTasks.Remove(selectedTech);
            else if (dgTraining.SelectedItem is TodoModel selectedTrain)
                _trainingTasks.Remove(selectedTrain);
        }
    }
}