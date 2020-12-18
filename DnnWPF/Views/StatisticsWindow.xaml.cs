using DnnWPF.Models.Domain;
using DnnWPF.ViewModels;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //DnnWPF.RoadSignsDnnDataSet roadSignsDnnDataSet = ((DnnWPF.RoadSignsDnnDataSet)(this.FindResource("roadSignsDnnDataSet")));
            //// Загрузить данные в таблицу TestedImages. Можно изменить этот код как требуется.
            //DnnWPF.RoadSignsDnnDataSetTableAdapters.TypesRoadSignsTableAdapter roadSignsDnnDataSetTestedImagesTableAdapter = new DnnWPF.RoadSignsDnnDataSetTableAdapters.TypesRoadSignsTableAdapter();
            //roadSignsDnnDataSetTestedImagesTableAdapter.Fill(roadSignsDnnDataSet.TypesRoadSigns);
            //System.Windows.Data.CollectionViewSource testedImagesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("typesRoadSigns")));
            //testedImagesViewSource.View.MoveCurrentToFirst();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        
    }
}
