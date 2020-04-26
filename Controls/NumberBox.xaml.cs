using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace KTANEAssistant.Controls
{
    /// <summary>
    /// Interaction logic for NumberBox.xaml
    /// </summary>
    public partial class NumberBox : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty MinDependency;
        public static readonly DependencyProperty MaxDependency;
        public static readonly DependencyProperty ValueDependency;

        static NumberBox()
        {
            MinDependency = DependencyProperty.Register("Minimum", typeof(int), typeof(NumberBox),
                new FrameworkPropertyMetadata(int.MinValue, new PropertyChangedCallback(DependencyPropertyChanged)) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            MaxDependency = DependencyProperty.Register("Maximum", typeof(int), typeof(NumberBox),
                new FrameworkPropertyMetadata(int.MaxValue, new PropertyChangedCallback(DependencyPropertyChanged)) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            ValueDependency = DependencyProperty.Register("Value", typeof(int), typeof(NumberBox),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(DependencyPropertyChanged)) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
        }

        private static void DependencyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NumberBox).OnPropertyChanged(e.Property.Name);
        }

        public NumberBox()
        {
            InitializeComponent();
            PropertyChanged += NumberBox_PropertyChanged;
        }

        public NumberBox(int initial, int min, int max)
        {
            if (max < min)
            {
                throw new ArgumentOutOfRangeException(nameof(max), "The maximum value given is smaller than the minimum");
            }
            Minimum = min;
            Maximum = max;
            Value = initial;
            InitializeComponent();
            PropertyChanged += NumberBox_PropertyChanged;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StringValue = "" + Value;
            OnPropertyChanged(nameof(StringValue));
        }

        private void NumberBox_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Value)))
            {
                StringValue = Value.ToString();
                OnPropertyChanged(nameof(StringValue));
            }
        }

        public int Minimum
        {
            get
            {
                return (int)GetValue(MinDependency);
            }
            set
            {
                if (Maximum < value)
                {
                    throw new ArgumentOutOfRangeException(nameof(Minimum), "The minimum value given is greater than the maximum");
                }
                SetValue(MinDependency, value);
            }
        }

        public int Maximum
        {
            get
            {
                return (int)GetValue(MaxDependency);
            }
            set
            {
                if (Minimum > value)
                {
                    throw new ArgumentOutOfRangeException(nameof(Maximum), "The maximum value given is smaller than the minimum");
                }
                SetValue(MaxDependency, value);
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
                int newVal = value;
                if (newVal < Minimum)
                {
                    newVal = Minimum;
                }
                else if (newVal > Maximum)
                {
                    newVal = Maximum;
                }
                SetValue(ValueDependency, newVal);
                OnPropertyChanged(nameof(Value));
            }
        }
        public string StringValue { get; set; }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                bool allowed = ValidateInput(text, out int x);
                e.CancelCommand();
                if (allowed)
                {
                    Value = x;
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool allowed = ValidateInput(e.Text, out int x);
            e.Handled = true;
            if (allowed)
            {
                Value = x;
            }
        }

        public bool ValidateInput(string input, out int x)
        {
            if (int.TryParse(input, out x))
            {
                return x <= Maximum && x >= Minimum;
            }
            x = 0;
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
