using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace KTANEAssistant.Controls
{
    /// <summary>
    /// Interaction logic for DropdownButton.xaml
    /// </summary>
    public partial class DropdownButton : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty StateDependency = DependencyProperty.Register("State", typeof(bool), typeof(DropdownButton), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});

        public DropdownButton()
        {
            InitializeComponent();
        }

        public bool State
        {
            get
            {
                return (bool)GetValue(StateDependency);
            }
            set
            {
                SetValue(StateDependency, value);
                OnPropertyChanged(nameof(State));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            State = !State;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
