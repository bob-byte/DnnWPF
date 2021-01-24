using DnnWPF.Views;

namespace DnnWPF.ViewModels
{
    internal partial class MainViewModel
    {
        private RelayCommand showStatisticsCommand;

        public RelayCommand ShowStatisticsCommand
        {
            get => showStatisticsCommand ?? (showStatisticsCommand = new RelayCommand(obj =>
            {
                StatisticsWindow statisticsWindow = new StatisticsWindow();
                statisticsWindow.ShowDialog();
            }));
        }
    }
}
