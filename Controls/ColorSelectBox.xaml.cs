using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KTANEAssistant.Controls
{
    /// <summary>
    /// Interaction logic for ColorSelectBox.xaml
    /// </summary>
    public partial class ColorSelectBox : UserControl
    {
        public static readonly DependencyProperty ColorSelectionDependency = DependencyProperty.Register("ColorSelection", typeof(BrushCollection), typeof(ColorSelectBox), new UIPropertyMetadata(new BrushCollection()));
        public static readonly DependencyProperty SelectionCommandDependency = DependencyProperty.Register("SelectionCommand", typeof(ICommand), typeof(ColorSelectBox));

        public BrushCollection ColorSelection
        {
            get
            {
                return (BrushCollection)GetValue(ColorSelectionDependency);
            }
            set
            {
                SetValue(ColorSelectionDependency, value);
            }
        }

        public ICommand SelectionCommand
        {
            get
            {
                return (ICommand)GetValue(SelectionCommandDependency);
            }
            set
            {
                SetValue(SelectionCommandDependency, value);
            }
        }

        public ColorSelectBox()
        {
            InitializeComponent();
        }
    }

    public class BrushCollection : ObservableCollection<Brush> { }
}
