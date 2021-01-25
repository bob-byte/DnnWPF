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
            get => selectImageCommand ?? (selectImageCommand = new RelayCommand(obj =>
            {
                try
                {
                    SelectImage(ref image, out openFile, out Boolean isSelected);

                    if(isSelected)
                    {
                        pictureBox.Source = ConvertToBitmapSource.LoadBitmap(image.ToBitmap());
                    }
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

        private void SelectImage(ref Image<Bgr, Byte> image, out OpenFileDialog openImage, out Boolean isSelected)
        {
            openImage = new OpenFileDialog
            {
                Filter = "Image files|*.bmp;*.jpg;*.gif;*.png;*.ppm;*.tif|All files|*.*"
            };

            isSelected = openImage.ShowDialog().GetValueOrDefault(false);

            if (isSelected)
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
