using DnnWPF.Models;
using DnnWPF.Models.Domain;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DnnWPF.ViewModels
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        private TypesRoadSigns selectedTypeRoadSign;
        private RelayCommand helpCommand;
        private DataGrid dataGrid;
        private ObservableCollection<TestedImages> testedImagesForShow = new ObservableCollection<TestedImages>();

        public ObservableCollection<TypesRoadSigns> TypesRoadSignsCollection { get; }

        public ObservableCollection<TestedImages> TestedImagesForShow 
        {
            get => testedImagesForShow;
            set
            {
                testedImagesForShow = value;
                OnPropertyChanged();
            }
        }

        public TypesRoadSigns SelectedTypeRoadSign
        {
            get 
            {
                Int32 classId = dataGrid.SelectedIndex;
                if (classId != -1)
                {
                    using(var query = new Query())
                    {
                        TestedImagesForShow = query.GetTestedImagesByValidId((Byte)classId);
                    }
                }
                return selectedTypeRoadSign;
            }
            set
            {
                selectedTypeRoadSign = value;
            }
        }

        public StatisticsViewModel(DataGrid dataGrid)
        {
            using(var query = new Query())
            {
                TypesRoadSignsCollection = query.GetAllTypesRoadSigns();
            }
            this.dataGrid = dataGrid;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]String prop = "") =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public RelayCommand HelpCommand
        {
            get => helpCommand ?? (helpCommand = new RelayCommand(obj =>
            {
                MessageBox.Show("Click on any row and you will see tested images according to selected road sign", 
                    "Help", MessageBoxButton.OK, MessageBoxImage.Information);
            }));
        }
    }
}
