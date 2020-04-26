using KTANEAssistant.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace KTANEAssistant.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private SettingsPageModel WrappedContext;
        public SettingsPage(MainWindow window)
        {
            InitializeComponent();
            WrappedContext = new SettingsPageModel(window);
            DataContext = WrappedContext;
        }

        private void CBoxAllowMods_CheckChanged(object sender, RoutedEventArgs e)
        {
            if (App.DoExternalLoading)
            {
                WrappedContext.CustomModuleDropdownVisibility = Visibility.Visible;
            }
            else
            {
                WrappedContext.CustomModuleDropdownVisibility = Visibility.Collapsed;
            }
            WrappedContext.OnPropertyChanged(nameof(WrappedContext.CustomModuleDropdownVisibility));
        }
    }
}
