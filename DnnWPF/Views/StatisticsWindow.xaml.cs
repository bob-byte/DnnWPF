using DnnWPF.Models.Domain;
using DnnWPF.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace DnnWPF.Views
{
    public class Phone
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public int Price { get; set; }
    }

    /// <summary>
    /// Логика взаимодействия для StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        internal StatisticsViewModel Statistic { get; }
        internal ObservableCollection<TestedImages> TestedImages { get; }

        public StatisticsWindow()
        {
            InitializeComponent();

            Statistic = new StatisticsViewModel(typesRoadSignsDataGrid);
            DataContext = Statistic;

            TestedImages = Statistic.TestedImagesForShow;
        }
    }
}
