using System.Windows;
using System.Windows.Controls;

namespace KTANEAssistant.Controls
{
    /// <summary>
    /// Interaction logic for PlaceholderTextbox.xaml
    /// </summary>
    public partial class PlaceholderTextbox : UserControl
    {
        public static readonly DependencyProperty InputTextDependency;
        public static readonly DependencyProperty PlaceholderTextDependency;
        public static readonly DependencyProperty TextWrappingDependency;

        public static readonly RoutedEvent TextChangedEvent;

        static PlaceholderTextbox()
        {
            InputTextDependency = DependencyProperty.Register("InputText", typeof(string), typeof(PlaceholderTextbox));
            PlaceholderTextDependency = DependencyProperty.Register("PlaceholderText", typeof(string), typeof(PlaceholderTextbox));
            TextWrappingDependency = DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(PlaceholderTextbox));
            TextChangedEvent = EventManager.RegisterRoutedEvent("TextChangedEvent", RoutingStrategy.Bubble, typeof(TextChangedEventHandler), typeof(PlaceholderTextbox));
        }

        public PlaceholderTextbox()
        {
            InitializeComponent();
        }

        public event TextChangedEventHandler TextChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }

        public string InputText
        {
            get
            {
                return (string)GetValue(InputTextDependency);
            }
            set
            {
                SetValue(InputTextDependency, value);
            }
        }

        public string PlaceholderText
        {
            get
            {
                return (string)GetValue(PlaceholderTextDependency);
            }
            set
            {
                SetValue(PlaceholderTextDependency, value);
            }
        }

        public TextWrapping TextWrapping
        {
            get
            {
                return (TextWrapping)GetValue(TextWrappingDependency);
            }
            set
            {
                SetValue(TextWrappingDependency, value);
            }
        }

        private void TextEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChangedEventArgs args = new TextChangedEventArgs(TextChangedEvent, UndoAction.None, e.Changes)
            {
                Source = sender
            };
            RaiseEvent(args);
        }
    }
}
