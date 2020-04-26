using System.Windows;
using System.Windows.Controls;

namespace KTANEAssistant.Controls
{
    /// <summary>
    /// Interaction logic for ArrowButton.xaml
    /// </summary>
    public partial class ArrowButton : UserControl
    {

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("ArrowClickEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ArrowButton));
        public static readonly DependencyProperty FacingDependency = DependencyProperty.Register("Facing", typeof(Direction), typeof(ArrowButton), new UIPropertyMetadata(Direction.Left));

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        public Direction Facing
        {
            get
            {
                return (Direction)GetValue(FacingDependency);
            }
            set
            {
                SetValue(FacingDependency, value);
            }
        }

        public ArrowButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent);
            RaiseEvent(args);
        }
    }
}
