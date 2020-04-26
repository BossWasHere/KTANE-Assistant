using KTANEAssistant.API;
using System.ComponentModel;

namespace KTANEAssistant.ViewModels
{
    public class MasterPageModel : INotifyPropertyChanged
    {
        internal MainWindow Window;

        public event PropertyChangedEventHandler PropertyChanged;

        public BombManager Bomb
        {
            get
            {
                return Window.Bomb;
            }
        }
        public SimpleCommand MenuClickHome { get; }
        public SimpleCommand MenuClickDefusal { get; }
        public SimpleCommand MenuClickSetup { get; }
        public SimpleCommand MenuClickCredits { get; }
        public SimpleCommand MenuClickSettings { get; }
        public SimpleCommand MenuClickCustom { get; }

        public MasterPageModel(MainWindow window)
        {
            Window = window;
            MenuClickHome = new SimpleCommand(window.SetMainPage);
            MenuClickDefusal = new SimpleCommand(window.SetDefusalPage);
            MenuClickSetup = new SimpleCommand(window.SetSetupPage);
            MenuClickCredits = new SimpleCommand(window.SetCreditsPage);
            MenuClickSettings = new SimpleCommand(window.SetSettingsPage);
            MenuClickCustom = new SimpleCommand(window.SetAddModulePage);
        }

        internal void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
