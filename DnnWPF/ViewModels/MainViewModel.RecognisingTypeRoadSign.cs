using DnnWPF.Views;

using Emgu.CV;
using Emgu.CV.Dnn;
using Emgu.CV.Structure;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DnnWPF.ViewModels
{
    internal partial class MainViewModel
    {
        private RelayCommand recognizeCommand;

        public RelayCommand RecognizeCommand
        {
            get => recognizeCommand ?? 
                (recognizeCommand = new RelayCommand( async obj => (recognising, modelNetwork) = await RecognizeAsync(recognising, modelNetwork)));
        }

        private async Task<(RecognisingTypeOfRoadSign<Bgr, Byte> recognising, Object model)> RecognizeAsync(RecognisingTypeOfRoadSign<Bgr, Byte> recognising, Object model) 
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

                        await Task.Run(() => predictedId = (Byte)recognising.GetPredictedIdOfRoadSign(modelNetwork, image));

                        isPredicted = true;

                        PredictedLabelName = $"Predicted road sign: {query.GetNameOfPredictedRoadSign(predictedId)}";
                        ValidLabelName = $"Valid road sign: {query.GetNameOfValidRoadSign(openFile.SafeFileName)}";
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

            return (recognising, model);
        }
    }
}
