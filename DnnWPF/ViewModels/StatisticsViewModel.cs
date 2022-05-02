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
                try
                {
                    Int32 selectedIndex = dataGrid.SelectedIndex;

                    if (selectedIndex != -1)
                    {
                        var roadSign = dataGrid.Items[selectedIndex] as TypesRoadSigns;

                        using (var query = new Query())
                        {
                            TestedImagesForShow = query.GetTestedImagesByValidId(roadSign.ClassId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            this.dataGrid = dataGrid;

            using (var query = new Query())
            {
                TypesRoadSignsCollection = query.GetAllTypesRoadSigns();
            }
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
