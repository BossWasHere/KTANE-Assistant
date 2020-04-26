using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace KTANEAssistant.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly MainWindow window;
        public MainPage(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
            DataContext = window.DefaultModel;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
