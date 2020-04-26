using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace KTANEAssistant.Pages
{
    /// <summary>
    /// Interaction logic for CreditsPage.xaml
    /// </summary>
    public partial class CreditsPage : Page
    {
        private readonly MainWindow window;
        public CreditsPage(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            window.Manager.SetPage(window.Manager.MainPage);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
