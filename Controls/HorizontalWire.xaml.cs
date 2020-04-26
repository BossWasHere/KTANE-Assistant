using KTANEAssistant.ModuleData;
using KTANEAssistant.Pages.Modules;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KTANEAssistant.Controls
{
    /// <summary>
    /// Interaction logic for HorizontalWire.xaml
    /// </summary>
    public partial class HorizontalWire : UserControl
    {
        private static readonly LinearGradientBrush OffBrush;

        public static readonly DependencyProperty SelectedBackgroundDependency;
        public static readonly DependencyProperty OffVisibilityDependency;
        public static readonly DependencyProperty ColorChangeEventHandlerDependency;

        public static readonly RoutedEvent ColorChangeEvent;

        public delegate void ColorChangedHandler(object sender, ColorChangeEventArgs args);
        public event ColorChangedHandler ColorChanged;

        private SimpleWireColors LastColor;

        static HorizontalWire()
        {
            GradientStopCollection gradientStops = new GradientStopCollection();
            gradientStops.Add(new GradientStop(Colors.White, 0));
            gradientStops.Add(new GradientStop(Colors.White, 0.5));
            gradientStops.Add(new GradientStop(Colors.Black, 0.5));
            gradientStops.Add(new GradientStop(Colors.Black, 1));
            OffBrush = new LinearGradientBrush(gradientStops, new Point(0, 0), new Point(12, 8))
            {
                SpreadMethod = GradientSpreadMethod.Repeat,
                MappingMode = BrushMappingMode.Absolute
            };

            SelectedBackgroundDependency = DependencyProperty.Register("SelectedBackground", typeof(Brush), typeof(HorizontalWire), new UIPropertyMetadata(OffBrush));
            OffVisibilityDependency = DependencyProperty.Register("OffVisibility", typeof(Visibility), typeof(HorizontalWire));
            ColorChangeEvent = EventManager.RegisterRoutedEvent("ColorChangeEvent", RoutingStrategy.Bubble, typeof(ColorChangedHandler), typeof(HorizontalWire));
        }

        public Brush SelectedBackground
        {
            get
            {
                return (Brush)GetValue(SelectedBackgroundDependency);
            }
            set
            {
                SetValue(SelectedBackgroundDependency, value);
            }
        }

        public Visibility OffVisibility
        {
            get
            {
                return (Visibility)GetValue(OffVisibilityDependency);
            }
            set
            {
                SetValue(OffVisibilityDependency, value);
            }
        }

        public HorizontalWire()
        {
            InitializeComponent();
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            SelectedBackground = Brushes.IndianRed;
            RaiseColorChangeEvent(LastColor, SimpleWireColors.Red);
        }

        private void Blue_Click(object sender, RoutedEventArgs e)
        {
            SelectedBackground = Brushes.DarkSlateBlue;
            RaiseColorChangeEvent(LastColor, SimpleWireColors.Blue);
        }

        private void Yellow_Click(object sender, RoutedEventArgs e)
        {
            SelectedBackground = Brushes.Goldenrod;
            RaiseColorChangeEvent(LastColor, SimpleWireColors.Yellow);
        }

        private void Black_Click(object sender, RoutedEventArgs e)
        {
            SelectedBackground = Brushes.Black;
            RaiseColorChangeEvent(LastColor, SimpleWireColors.Black);
        }

        private void White_Click(object sender, RoutedEventArgs e)
        {
            SelectedBackground = Brushes.White;
            RaiseColorChangeEvent(LastColor, SimpleWireColors.White);
        }

        private void Off_Click(object sender, RoutedEventArgs e)
        {
            SelectedBackground = OffBrush;
            RaiseColorChangeEvent(LastColor, SimpleWireColors.Off);
        }

        protected void RaiseColorChangeEvent(SimpleWireColors oldColor, SimpleWireColors newColor)
        {
            ColorChanged?.Invoke(this, new ColorChangeEventArgs() { OldColor = oldColor, NewColor = newColor });
            LastColor = newColor;
        }
    }
}
