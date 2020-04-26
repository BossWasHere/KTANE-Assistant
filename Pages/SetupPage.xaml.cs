using KTANEAssistant.API;
using KTANEAssistant.Controls;
using System.Windows;
using System.Windows.Controls;

namespace KTANEAssistant.Pages
{
    /// <summary>
    /// Interaction logic for SetupPage.xaml
    /// </summary>
    public partial class SetupPage : Page
    {
        private readonly MainWindow window;
        public SetupPage(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
            DataContext = window.DefaultModel;
        }

        private void Batteries_ValueChanged(object sender, RoutedEventArgs e)
        {
            window.Bomb.Batteries = (e.OriginalSource as NumberBox).Value;
        }

        private void BatteryHolders_ValueChanged(object sender, RoutedEventArgs e)
        {
            window.Bomb.BatteryHolders = (e.OriginalSource as NumberBox).Value;
        }

        private void Strikes_ValueChanged(object sender, RoutedEventArgs e)
        {
            window.Bomb.Strikes = (e.OriginalSource as NumberBox).Value;
        }

        private void IndicatorStatus_StatusChanged(object sender, RoutedEventArgs e)
        {
            if (sender is IndicatorStatus control)
            {
                if (control.Tag is Indicators ind)
                {
                    window.Bomb.SetIndicator(ind, control.Value);
                }
            }
        }

        private void ResetBomb_Click(object sender, RoutedEventArgs e)
        {
            var dialog = ConfirmDialog.YesNoDialog("Confirm Action", "Reset the current bomb?", ConfirmDialog.ExclamationImagePath, ConfirmResetBomb);
            dialog.Owner = window;
            dialog.ShowDialog();
        }

        public void ConfirmResetBomb(ConfirmActions action)
        {
            if (action.Equals(ConfirmActions.Yes))
            {
                var generator = IndicatorSet.ItemContainerGenerator;
                for (int i = 0; i < IndicatorSet.Items.Count; i++)
                {
                    if (generator.ContainerFromIndex(i) is ContentPresenter cp)
                    {
                        if (cp.ContentTemplate.FindName("IndicatorItem", cp) is IndicatorStatus indicator)
                        {
                            indicator.SuperSecretAntiEventRaiser = false;
                            indicator.Value = 0;
                            indicator.SuperSecretAntiEventRaiser = true;
                        }
                    }
                }
                window.Bomb.Reset();
            }
        }
    }
}
