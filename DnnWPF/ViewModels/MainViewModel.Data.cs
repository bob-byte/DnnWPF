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
        private RecognisingTypeOfRoadSign<Bgr, Byte> recognition;
        private Object modelNetwork;
        private Query query;
        private Byte predictedId;
        private Boolean isPredicted;
        private String predictedLabelName;
        private String validLabelName;
        private bool disposedValue = false; // Для определения избыточных вызовов

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

        public MainViewModel(Dispatcher dispatcher, Image pictureBox)
        {
            this.dispatcher = dispatcher;
            this.pictureBox = pictureBox;
            query = new Query();
            recognition = new RecognisingEmguCVNoCLAHE<Bgr, Byte>();

            modelNetwork = (Net)recognition.LoadModel("netWithoutCLAHE.onnx");
        }

        public void OnPropertyChanged([CallerMemberName]String prop = "") =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                    GC.Collect();
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.
                query?.Dispose();
                image?.Dispose();
                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        ~MainViewModel()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(false);
        }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
