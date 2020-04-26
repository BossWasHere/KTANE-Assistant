using KTANEAssistant.API;
using KTANEAssistant.ViewModels;
using System.Windows;
using System.Windows.Controls;

/*
 * Defusal isn't a word in the Oxford Dictionary :)
 */
namespace KTANEAssistant.Pages
{
    /// <summary>
    /// Interaction logic for DefusalPage.xaml
    /// </summary>
    public partial class DefusalPage : Page
    {
        protected DefusalPageModel WrappedContext;
        internal readonly MainWindow window;

        public DefusalPage(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
            WrappedContext = new DefusalPageModel(this);
            DataContext = WrappedContext;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WrappedContext.LoadDefaults();
        }

        public void ModuleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                if (sender is IModuleListPage)
                {
                    foreach (var item in ModuleList.Items)
                    {
                        if (item is FrameworkElement element && e.AddedItems[0] is IModuleModel model)
                        {
                            if (element.Tag is IModuleModel compareModel)
                            {
                                if (compareModel.Equals(model))
                                {
                                    ModuleList.SelectedItem = item;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    WrappedContext.ModuleSelectionChanged(sender, e);
                }
            }
        }

        public void SetOutput(string output)
        {
            MessageOutput.Text = output;
        }

    }
}
