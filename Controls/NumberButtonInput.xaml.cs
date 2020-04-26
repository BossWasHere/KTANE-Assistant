using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace KTANEAssistant.Controls
{
    /// <summary>
    /// Interaction logic for NumberButton.xaml
    /// </summary>
    public partial class NumberButtonInput : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty MinDependency = DependencyProperty.Register("Min", typeof(int), typeof(NumberButtonInput), new FrameworkPropertyMetadata(int.MinValue) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
        public static readonly DependencyProperty MaxDependency = DependencyProperty.Register("Max", typeof(int), typeof(NumberButtonInput), new FrameworkPropertyMetadata(int.MaxValue) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
        public static readonly DependencyProperty ValueDependency = DependencyProperty.Register("Value", typeof(int), typeof(NumberButtonInput), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

        public int Min
        {
            get
            {
                return (int)GetValue(MinDependency);
            }
            set
            {
                if (Max < value)
                {
                    throw new ArgumentOutOfRangeException(nameof(Min), "The minimum value given is greater than the maximum");
                }
                SetValue(MinDependency, value);
                OnPropertyChanged(nameof(MinDependency));
            }
        }
        public int Max
        {
            get
            {
                return (int)GetValue(MaxDependency);
            }
            set
            {
                if (Min > value)
                {
                    throw new ArgumentOutOfRangeException(nameof(Max), "The maximum value given is smaller than the minimum");
                }
                SetValue(MaxDependency, value);
                OnPropertyChanged(nameof(MaxDependency));
            }
        }
        public int Value
        {
            get
            {
                return (int)GetValue(ValueDependency);
            }
            set
            {
                if (value > Max)
                {
                    SetValue(ValueDependency, Max);
                }
                else if (value < Min)
                {
                    SetValue(ValueDependency, Min);
                }
                else
                {
                    SetValue(ValueDependency, value);
                }
                OnPropertyChanged(nameof(Value));
            }
        }

        public NumberButtonInput()
        {
            InitializeComponent();
        }

        private void ArrowButton_Incr(object sender, RoutedEventArgs e)
        {
            Value++;
        }

        private void ArrowButton_Decr(object sender, RoutedEventArgs e)
        {
            Value--;
        }

        private void UserControl_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta == 120)
            {
                Value++;
            }
            else if (e.Delta == -120)
            {
                Value--;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
