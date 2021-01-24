using DnnWPF.ViewModels;
using System.ComponentModel;

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
