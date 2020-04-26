using KTANEAssistant.API;
using KTANEAssistant.ModuleData;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace KTANEAssistant.Pages.Modules
{
    /// <summary>
    /// Interaction logic for ButtonPage.xaml
    /// </summary>
    public partial class ButtonPage : ModulePage, INotifyPropertyChanged
    {
        private ButtonModule ModuleData;
        public SimpleCommand StripColorChange { get; }
        public SimpleCommand ButtonColorChange { get; }
        public Brush StripColor { get; private set; }

        public ButtonPage()
        {
            InitializeComponent();
            StripColorChange = new SimpleCommand(StripColorChangeEvent);
            ButtonColorChange = new SimpleCommand(ButtonColorChangeEvent);
            ModuleData = new ButtonModule();
            ModuleData.SolutionChangedEvent += ModuleData_SolutionChangedEvent;
        }

        private void ModuleData_SolutionChangedEvent(object sender, SolutionChangedEventArgs<ButtonPhase> args)
        {
            DefusalPage.MessageOutput.Text = args.CurrentSolution.ToString();
        }

        private void StripColorChangeEvent(object parameter)
        {
            Brush brush = parameter as Brush;
            if (brush.GetType().Equals(typeof(SolidColorBrush)))
            {
                Color color = ((SolidColorBrush)parameter).Color;
                if (color.Equals(Colors.Red))
                {
                    ModuleData.Strip = ButtonColor.Red;
                }
                else if (color.Equals(Colors.Blue))
                {
                    ModuleData.Strip = ButtonColor.Blue;
                }
                else if (color.Equals(Colors.Yellow))
                {
                    ModuleData.Strip = ButtonColor.Yellow;
                }
                else if (color.Equals(Colors.White))
                {
                    ModuleData.Strip = ButtonColor.White;
                }
            }
            else
            {
                ModuleData.Strip = ButtonColor.Disabled;
            }
            StripColor = brush;
            OnPropertyChanged(nameof(StripColor));
            ModuleSolver.SolveButton(BombManager, ref ModuleData);
        }

        private void ButtonColorChangeEvent(object parameter)
        {
            Brush brush = parameter as Brush;
            if (brush.GetType().Equals(typeof(SolidColorBrush)))
            {
                Color color = ((SolidColorBrush)parameter).Color;
                if (color.Equals(Colors.Red))
                {
                    ModuleData.Color = ButtonColor.Red;
                }
                else if (color.Equals(Colors.Blue))
                {
                    ModuleData.Color = ButtonColor.Blue;
                }
                else if (color.Equals(Colors.Yellow))
                {
                    ModuleData.Color = ButtonColor.Yellow;
                }
                else if (color.Equals(Colors.White))
                {
                    ModuleData.Color = ButtonColor.White;
                }
            }
            ModuleData.Strip = ButtonColor.Disabled;
            StripColor = App.CheckeredBrush;
            OnPropertyChanged(nameof(StripColor));
            LargeButton.SetButtonColor(ModuleData.Color);
            ModuleSolver.SolveButton(BombManager, ref ModuleData);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var content = ((ListBoxItem)ButtonTextList.SelectedItem).Content;
            if (content != null)
            {
                content = content.ToString();
                if (content.Equals("Abort"))
                {
                    ModuleData.Text = ButtonText.Abort;
                }
                else if (content.Equals("Detonate"))
                {
                    ModuleData.Text = ButtonText.Detonate;
                }
                else if (content.Equals("Hold"))
                {
                    ModuleData.Text = ButtonText.Hold;
                }
                else if (content.Equals("Press"))
                {
                    ModuleData.Text = ButtonText.Press;
                }
                ModuleData.Strip = ButtonColor.Disabled;
                StripColor = App.CheckeredBrush;
                OnPropertyChanged(nameof(StripColor));
                LargeButton.SetButtonText(ModuleData.Text);
                ModuleSolver.SolveButton(BombManager, ref ModuleData);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public struct ColorChangeEventArgs
    {
        public SimpleWireColors OldColor;
        public SimpleWireColors NewColor;
    }
}
