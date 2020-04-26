using KTANEAssistant.API;
using KTANEAssistant.ModuleData;
using System.Windows;

namespace KTANEAssistant.Pages.Modules
{
    /// <summary>
    /// Interaction logic for SimonSaysPage.xaml
    /// </summary>
    public partial class SimonSaysPage : ModulePage
    {

        private SimonSaysModule ModuleData;

        public SimpleCommand SimonEnterColorEvent { get; }
        public SimonSaysPage()
        {
            SimonEnterColorEvent = new SimpleCommand(SimonEnterColor);
            ModuleData = new SimonSaysModule();
            InitializeComponent();
        }

        private void SimonEnterColor(object parameter)
        {
            MessageBox.Show("color " + parameter);
            ModuleData.FlashSequence = new SimonColors[4];
        }
    }
}
