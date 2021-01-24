using DnnWPF.Models;
using Emgu.CV.Structure;
using Ookii.Dialogs.Wpf;
using System;
using System.IO;
using System.Threading;
using System.Windows;

namespace DnnWPF.ViewModels
{
    internal partial class MainViewModel
    {
        private RelayCommand testCommand;
        private RelayCommand enterDataOneImageCommand;

        public RelayCommand TestCommand
        {
            get => testCommand ?? (testCommand = new RelayCommand(obj =>
            {
                VistaFolderBrowserDialog dialogSelectFolder = new VistaFolderBrowserDialog();
                try
                {
                    if (dialogSelectFolder.ShowDialog().GetValueOrDefault())
                    {
                        var dialog = MessageBox.Show($"Are you sure that you want to test images in {dialogSelectFolder.SelectedPath}?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                        if (dialog == MessageBoxResult.Yes)
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
            }));
        }

        private void RunThreadTesting(Object dialogObj)
        {
            try
            {
                VistaFolderBrowserDialog dialog = dialogObj as VistaFolderBrowserDialog;
                if (dialog == null)
                {
                    throw new InvalidCastException("Can\'t convert Object to VistaFolderBrowserDialog");
                }

                Testing(dialog.SelectedPath);

                MessageBox.Show("Images successfully added to database", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Testing(String path)
        {
            var test = new Test<Bgr, Byte, Double>();
            test.Testing(recognition, modelNetwork, path, SearchOption.TopDirectoryOnly);
        }

        public RelayCommand EnterDataOneImageCommand
        {
            get => enterDataOneImageCommand ?? (enterDataOneImageCommand = new RelayCommand(obj =>
            {
                if(isPredicted)
                {
                    try
                    {
                        EnterDataOneImage();

                        MessageBox.Show("Image successfully added to database", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("First recognize type of road sign", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }));
        }

        private void EnterDataOneImage()
        {
            try
            {
                Byte validId = query.GetValidId($"Test/{openFile.SafeFileName}");

                query.AddImage(openFile.SafeFileName, validId, predictedId, true);
                query.UpdateTypesRoadSigns(validId);
            }
            catch
            {
                throw;
            }
        }
    }
}
