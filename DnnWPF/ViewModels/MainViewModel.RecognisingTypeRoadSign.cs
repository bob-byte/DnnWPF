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
                (recognizeCommand = new RelayCommand(obj => Recognize(RunThreadRecognition, ref recognising, ref modelNetwork)));
        }

        private void Recognize(Action runThreadRecognition, ref RecognisingTypeOfRoadSign<Bgr, Byte> recognising, ref Object model) 
        {
            if(pictureBox.Source != null)
            {
                var selectLibrary = new SelectLibrary();
                var resultDialog = selectLibrary.ShowDialog().GetValueOrDefault(false);

                if (resultDialog)
                {
                    try
                    {
                        if (TypeOfLibrary.Contains("Emgu") && !(recognising is RecognisingEmguCvNoClahe<Bgr, Byte>))
                        {
                            recognising = new RecognisingEmguCvNoClahe<Bgr, Byte>();

                            model = (Net)recognising.LoadModel("netWithoutCLAHE.onnx");
                        }
                        else if (TypeOfLibrary.Contains("Sharp") && !(recognising is RecognisingSharpCvNoClahe<Bgr, Byte>))
                        {
                            recognising = new RecognisingSharpCvNoClahe<Bgr, Byte>();

                            model = (SharpCV.Net)recognising.LoadModel("netWithoutCLAHE.onnx");
                        }

                        ThreadStart recognisingDelegate = new ThreadStart(runThreadRecognition);
                        Thread threadRecognising = new Thread(recognisingDelegate)
                        {
                            Priority = ThreadPriority.Highest
                        };

                        threadRecognising.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("First you should select image", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RunThreadRecognition()
        {
            try
            {
                predictedId = (Byte)recognising.GetPredictedIdOfRoadSign<Double>(modelNetwork, image);
                isPredicted = true;

                dispatcher.Invoke(delegate ()
                {
                    PredictedLabelName = $"Predicted road sign: {query.GetNameOfPredictedRoadSign(predictedId)}";
                    ValidLabelName = $"Valid road sign: {query.GetNameOfValidRoadSign(openFile.SafeFileName)}";
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
