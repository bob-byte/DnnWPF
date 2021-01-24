using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.Win32;
using System;
using System.Windows;

namespace DnnWPF.ViewModels
{
    internal partial class MainViewModel
    {
        private RelayCommand selectImageCommand;

        public RelayCommand SelectImageCommand
        {
            get =>
                selectImageCommand ?? (selectImageCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        SelectImage(out openFile);
                        pictureBox.Source = ConvertToBitmapSource.LoadBitmap(image.ToBitmap());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    ValidLabelName = "";
                    PredictedLabelName = "";

                    isPredicted = false;
                }));
        }

        private void SelectImage(out OpenFileDialog openImage)
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
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
