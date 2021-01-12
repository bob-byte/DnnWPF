using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using DnnWPF.ViewModels;
using System.ComponentModel;
using Emgu.CV.Dnn;
using DnnWPF.Models;
using Ookii.Dialogs.Wpf;
using System.Threading;

namespace DnnWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private OpenFileDialog openImage;
        private String pathToImage;
        private Image<Bgr, Byte> image;
        private Query query = new Query();
        private Byte validId, predictedId;
        private Boolean isPredicted;
        private RecognisingTypeOfRoadSign<Bgr, Byte> recognising = new RecognisingEmguCVNoCLAHE<Bgr, Byte>();
        private Object model;

        internal static String TypeOfLibrary { private get; set; }

        public MainWindow()
        {
            InitializeComponent();
            model = (Net)recognising.LoadModel("netWithoutCLAHE.onnx");
        }

        private void ShowStatistic_Click(object sender, RoutedEventArgs e)
        {
            StatisticsWindow statisticsWindow = new StatisticsWindow();
            statisticsWindow.ShowDialog();
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            openImage = new OpenFileDialog
            {
                Filter = "Image files|*.bmp;*.jpg;*.gif;*.png;*.ppm;*.tif|All files|*.*"
            };

            Boolean? findImage = openImage.ShowDialog();

            if (findImage == true)
            {
                try
                {
                    image = new Image<Bgr, Byte>(openImage.FileName);
                    pathToImage = openImage.SafeFileName;

                    pictureBox1.Source = ConvertToBitmapSource.LoadBitmap(image.ToBitmap());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                T_B_ValidLabelName.Text = "";
                T_B_PredictedLabelName.Text = "";

                isPredicted = false;
            }
        }

        private void Recognise_Click(object sender, RoutedEventArgs e)
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

                    predictedId = (Byte)recognising.GetPredictedIdOfRoadSign<Double>(model, image);
                    isPredicted = true;

                    T_B_PredictedLabelName.Text = $"Predicted road sign: {query.GetNameOfPredictedRoadSign(predictedId)}";

                    //For checking whether image exist in table
                    if (!openImage.FileName.Contains($"GTSRB-german-traffic-sign\\Test\\{openImage.SafeFileName}"))
                    {
                        MessageBox.Show("This image doesn\'t exist in table \"ImageForTest\"");
                    }
                    else
                    {
                        T_B_ValidLabelName.Text = $"Valid road sign: {query.GetNameOfValidRoadSign(openImage.SafeFileName)}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



        private void SaveImage_Click(object sender, RoutedEventArgs e)
        {
            if (pictureBox1.Source != null)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Image Files(*.PNG)|*.PNG";
                Boolean? dialog = saveDialog.ShowDialog();

                if (dialog == true)
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)pictureBox1.Source));

                    using (FileStream stream = new FileStream(saveDialog.FileName, FileMode.Create))
                    {
                        encoder.Save(stream);
                    }
                }
            }
            else
            {
                MessageBox.Show("Picture box can\'t be empty", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EnterData_Click(object sender, RoutedEventArgs e)
        {
            if (isPredicted)
            {
                try
                {
                    validId = query.GetValidId($"Test/{pathToImage}");

                    query.AddImage(pathToImage, validId, predictedId, true);
                    query.UpdateTypesRoadSigns(validId);

                    MessageBox.Show("Image successfully added to database", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }            
        }

        private void RunThreadTesting(Object dialogObj)
        {
            try
            {
                VistaFolderBrowserDialog dialog = dialogObj as VistaFolderBrowserDialog;
                if(dialog == null)
                {
                    throw new InvalidCastException("Can\'t convert Object to VistaFolderBrowserDialog");
                }
                
                var test = new Test<Bgr, Byte, Double>();
                test.Testing(recognising, model, dialog.SelectedPath, SearchOption.TopDirectoryOnly);

                MessageBox.Show("Images successfully added to database", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialogSelectFolder = new VistaFolderBrowserDialog();
            try
            {
                if (dialogSelectFolder.ShowDialog(this).GetValueOrDefault())
                {
                    var dialog = MessageBox.Show($"Are you sure that you want to test images in {dialogSelectFolder.SelectedPath}?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                    if(dialog == MessageBoxResult.Yes)
                    {
                        ParameterizedThreadStart testingDelegate = new ParameterizedThreadStart(RunThreadTesting);

                        Thread threadInputData = new Thread(testingDelegate)
                        {
                            Priority = ThreadPriority.Highest
                        };

                        threadInputData.Start(dialogSelectFolder);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            query.Dispose();

            if (image != null)
            {
                image.Dispose();
            }

            base.OnClosing(e);
        }
    }
}
