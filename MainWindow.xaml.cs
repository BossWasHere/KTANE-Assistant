using KTANEAssistant.API;
using KTANEAssistant.ViewModels;
using System.Windows;

namespace KTANEAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DefaultModel = new MasterPageModel(this);
            Manager = new PageManager(this);
            Bomb = new BombManager();
        }

        public PageManager Manager { get; }
        public MasterPageModel DefaultModel { get; }
        public BombManager Bomb { get; private set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetMainPage();
        }

        protected internal void SetMainPage()
        {
            Manager.SetPage(Manager.MainPage);
        }

        protected internal void SetDefusalPage()
        {
            Manager.SetPage(Manager.DefusalPage);
        }

        protected internal void SetSettingsPage()
        {
            Manager.SetPage(Manager.SettingsPage);
        }

        protected internal void SetSetupPage()
        {
            Manager.SetPage(Manager.SetupPage);
        }

        protected internal void SetCreditsPage()
        {
            Manager.SetPage(Manager.CreditsPage);
        }

        protected internal void SetAddModulePage()
        {
            //Manager.SetPage(Manager.CreditsPage);
        }
    }
}
