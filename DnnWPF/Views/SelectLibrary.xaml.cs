using System.Windows;

namespace DnnWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для SelectLibrary.xaml
    /// </summary>
    public partial class SelectLibrary : Window
    {
        public SelectLibrary()
        {
            InitializeComponent();
        }

        private void SelectedEmguCV_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.TypeOfLibrary = "EmguCV";

            DialogResult = true;
            Close();
        }

        private void SelectedSharpCV_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.TypeOfLibrary = "SharpCV";

            DialogResult = true;
            Close();
        }
    }
}
