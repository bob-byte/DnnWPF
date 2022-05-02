using DnnWPF.Models;
using Emgu.CV;
using Emgu.CV.Dnn;
using Emgu.CV.Structure;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DnnWPF.ViewModels
{
    internal partial class MainViewModel : INotifyPropertyChanged, IDisposable
    {
        private OpenFileDialog openFile;

        private Image<Bgr, Byte> image;
        private Image pictureBox;

        private Dispatcher dispatcher;

        private RecognisingTypeOfRoadSign<Bgr, Byte> recognising;
        private Object modelNetwork;

        private Query query;
        private Byte predictedId;
        private Boolean isPredicted;
        private String predictedLabelName;
        private String validLabelName;

        private Boolean disposedValue = false; // Для определения избыточных вызовов

        internal static String TypeOfLibrary { private get; set; }

        public String PredictedLabelName
        {
            get => predictedLabelName;
            private set
            {
                predictedLabelName = value;
                OnPropertyChanged();
            }
        }

        public String ValidLabelName 
        {
            get => validLabelName;
            private set
            {
                validLabelName = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //Replace in better location
        public MainViewModel(Dispatcher dispatcher, Image pictureBox)
        {
            this.dispatcher = dispatcher;
            this.pictureBox = pictureBox;
            query = new Query();//Initialize in methods MainViewModel.EnterData.Testing and MainViewModel.EnterData.EnterDataOneImage
            recognising = new RecognisingEmguCvNoClahe<Bgr, Byte>();

            modelNetwork = (Net)recognising.LoadModel("netWithoutCLAHE.onnx");
        }

        public void OnPropertyChanged([CallerMemberName]String prop = "") =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        //Delete using destructor
        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    GC.Collect();
                }

                query.Dispose();
                image?.Dispose();
                disposedValue = true;
            }
        }

        ~MainViewModel()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
