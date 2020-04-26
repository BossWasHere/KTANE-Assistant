using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KTANEAssistant.Controls
{
    /// <summary>
    /// Interaction logic for IndicatorStatus.xaml
    /// </summary>
    public partial class IndicatorStatus : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty ValueDependency = DependencyProperty.Register("State", typeof(int), typeof(IndicatorStatus));
        public static readonly RoutedEvent StatusChangedEventHandler = EventManager.RegisterRoutedEvent("StatusChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IndicatorStatus));

        public event RoutedEventHandler StatusChanged
        {
            add { AddHandler(StatusChangedEventHandler, value); }
            remove { RemoveHandler(StatusChangedEventHandler, value); }
        }

        public IndicatorStatus()
        {
            InitializeComponent();
        }

        internal bool SuperSecretAntiEventRaiser = false;

        public int Value
        {
            get
            {
                return (int)GetValue(ValueDependency);
            }
            set
            {
                SetValue(ValueDependency, value);
                if (SuperSecretAntiEventRaiser) RaiseEvent(new RoutedEventArgs(StatusChangedEventHandler));
                OnPropertyChanged(nameof(Value));
            }
        }

        private void UserControl_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta == 120)
            {
                if (Value < 2) Value++;
            }
            else if (e.Delta == -120)
            {
                if (Value > 0) Value--;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
