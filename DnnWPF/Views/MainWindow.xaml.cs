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
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DnnWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        MainViewModel MainViewModel { get; }

        public MainWindow()
        {
            InitializeComponent();
            MainViewModel = new MainViewModel(Dispatcher, pictureBox1);
            DataContext = MainViewModel;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MainViewModel.Dispose();

            base.OnClosing(e);
        }
    }
}
