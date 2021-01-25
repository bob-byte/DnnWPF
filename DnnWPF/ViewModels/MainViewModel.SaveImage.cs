using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DnnWPF.ViewModels
{
    internal partial class MainViewModel
    {
        private RelayCommand saveImageCommand;

        public RelayCommand SaveImageCommand
        {
            get => saveImageCommand ?? (saveImageCommand = new RelayCommand(obj =>
            {
                if (pictureBox.Source != null)
                {
                    SaveImage((BitmapSource)pictureBox.Source);

                    MessageBox.Show("Image successfully saved", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Picture box can\'t be empty to save image", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }));
        }

        private void SaveImage(BitmapSource bitmap)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Image Files(*.PNG)|*.PNG";

            Boolean? dialog = saveDialog.ShowDialog();

            if (dialog == true)
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));

                using (FileStream stream = new FileStream(saveDialog.FileName, FileMode.Create))
                {
                    encoder.Save(stream);
                }
            }
        }
    }
}
