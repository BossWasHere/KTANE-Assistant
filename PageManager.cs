using KTANEAssistant.Pages;
using System.Windows.Controls;

namespace KTANEAssistant
{
    public class PageManager
    {
        private readonly MainWindow Window;
        public PageManager(MainWindow window)
        {
            Window = window;
            MainPage = new MainPage(window);
            DefusalPage = new DefusalPage(window);
            SetupPage = new SetupPage(window);
            CreditsPage = new CreditsPage(window);
            SettingsPage = new SettingsPage(window);
        }

        public void SetPage(Page page)
        {
            Window.Content = page;
        }

        public Page MainPage { get; }
        public Page DefusalPage { get; }
        public Page SetupPage { get; }
        public Page CreditsPage { get; }
        public Page SettingsPage { get; }
    }
}
