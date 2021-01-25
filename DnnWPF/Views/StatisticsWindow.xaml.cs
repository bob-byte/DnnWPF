using DnnWPF.Models.Domain;
using DnnWPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace DnnWPF.Views
{
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
