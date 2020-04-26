using KTANEAssistant.ModuleData;
using System.ComponentModel;
using System.Windows.Controls;

namespace KTANEAssistant.Controls
{
    /// <summary>
    /// Interaction logic for BigButton.xaml
    /// </summary>
    public partial class BigButton : UserControl, INotifyPropertyChanged
    {
        public ButtonColor ButtonColor { get; private set; }
        public ButtonText ButtonText { get; private set; }

        public BigButton()
        {
            InitializeComponent();
        }

        public void SetButtonColor(ButtonColor buttonColor)
        {
            ButtonColor = buttonColor;
            OnPropertyChanged(nameof(ButtonColor));
        }

        public void SetButtonText(ButtonText buttonText)
        {
            ButtonText = buttonText;
            OnPropertyChanged(nameof(ButtonText));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
