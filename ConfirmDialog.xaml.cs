using System.Collections.ObjectModel;
using System.Windows;

namespace KTANEAssistant
{
    /// <summary>
    /// Interaction logic for ConfirmDialog.xaml
    /// </summary>
    public partial class ConfirmDialog : Window
    {
        public static readonly string ExclamationImagePath = "/KTANE-Assistant;component/Resources/exclamation.png";

        private ConfirmationDataContext CurrentContext;
        private bool Acted;

        public ConfirmDialog(ConfirmationDataContext dataContext)
        {
            InitializeComponent();
            CurrentContext = dataContext;
            DataContext = CurrentContext;
            Acted = false;
        }

        public ConfirmDialog(ConfirmationDataContext dataContext, Window parent)
        {
            InitializeComponent();
            CurrentContext = dataContext;
            DataContext = CurrentContext;
            Acted = false;
            Owner = parent;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CurrentContext.DefaultAction != ConfirmActions.None && !Acted)
            {
                CurrentContext.ActionCallback(CurrentContext.DefaultAction);
            }
        }

        private void ActionSelection(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            if (element.Tag.GetType().Equals(typeof(ConfirmActions)))
            {
                ConfirmActions action = (ConfirmActions)element.Tag;
                CurrentContext.ActionCallback(action);
            }
            Acted = true;
            Close();
        }

        public static ConfirmDialog YesNoDialog(string title, string description, string imagePath, ConfirmationDataContext.CallbackAction callback)
        {
            return new ConfirmDialog(new ConfirmationDataContext
            {
                ActionCallback = callback,
                ActionTitle = title,
                ActionDescriptor = description,
                Actions = new ObservableCollection<ConfirmActions>() { ConfirmActions.Yes, ConfirmActions.No },
                ImageSource = imagePath
            });
        }
    }

    public struct ConfirmationDataContext
    {
        public string ActionTitle { get; set; }
        public string ActionDescriptor { get; set; }
        public string ImageSource { get; set; }
        public ObservableCollection<ConfirmActions> Actions { get; set; }
        public delegate void CallbackAction(ConfirmActions action);
        public ConfirmActions DefaultAction { get; set; }
        public CallbackAction ActionCallback { get; set; }
    }

    public enum ConfirmActions
    {
        Cancel, Yes, No, Agree, Disagree, None
    }
}
