using DnnWPF.Views;
using Emgu.CV.Dnn;
using Emgu.CV.Structure;
using System;
using System.Threading;
using System.Windows;

namespace DnnWPF.ViewModels
{
    internal partial class MainViewModel
    {
        private RelayCommand recognizeCommand;

        public RelayCommand RecognizeCommand
        {
            get => recognizeCommand ?? 
                (recognizeCommand = new RelayCommand(obj => Recognize(recognition, modelNetwork)));
        }

        private void Recognize(RecognisingTypeOfRoadSign<Bgr, Byte> recognising, Object model) 
        {
            var selectLibrary = new SelectLibrary();
            var resultDialog = selectLibrary.ShowDialog().GetValueOrDefault(false);

            try
            {
                if (resultDialog)
                {
                    if (TypeOfLibrary.Contains("Emgu") && !(recognising is RecognisingEmguCVNoCLAHE<Bgr, Byte>))
                    {
                        recognising = new RecognisingEmguCVNoCLAHE<Bgr, Byte>();

                        model = (Net)recognising.LoadModel("netWithoutCLAHE.onnx");
                    }
                    else if (TypeOfLibrary.Contains("Sharp") && !(recognising is RecognisingSharpCVNoCLAHE<Bgr, Byte>))
                    {
                        recognising = new RecognisingSharpCVNoCLAHE<Bgr, Byte>();

                        model = (SharpCV.Net)recognising.LoadModel("netWithoutCLAHE.onnx");
                    }

                    ThreadStart recognitionDelegate = new ThreadStart(RunThreadRecognition);
                    Thread threadRecognition = new Thread(recognitionDelegate)
                    {
                        Priority = ThreadPriority.Highest
                    };

                    threadRecognition.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RunThreadRecognition()
        {
            try
            {
                predictedId = (Byte)recognition.GetPredictedIdOfRoadSign<Double>(modelNetwork, image);
                isPredicted = true;

                dispatcher.Invoke(delegate ()
                {
                    PredictedLabelName = $"Predicted road sign: {query.GetNameOfPredictedRoadSign(predictedId)}";

                    //For checking whether image exist in table
                    if (!openFile.FileName.Contains($"GTSRB-german-traffic-sign\\Test\\{openFile.SafeFileName}"))
                    {
                        MessageBox.Show("This image doesn\'t exist in database", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        ValidLabelName = $"Valid road sign: {query.GetNameOfValidRoadSign(openFile.SafeFileName)}";
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
