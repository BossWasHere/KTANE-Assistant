using KTANEAssistant.API;
using KTANEAssistant.ModuleData;
using System.Windows;

namespace KTANEAssistant.Pages.Modules
{
    /// <summary>
    /// Interaction logic for SimpleWires.xaml
    /// </summary>
    public partial class SimpleWiresPage : ModulePage
    {
        private SimpleWiresModule ModuleData;

        public SimpleWiresPage()
        {
            ModuleData = new SimpleWiresModule();
            ModuleData.SolutionChangedEvent += ModuleData_SolutionChangedEvent;
            InitializeComponent();
        }

        private void ModuleData_SolutionChangedEvent(object sender, SolutionChangedEventArgs<int> args)
        {
            DefusalPage.SetOutput(string.Format("Cut wire {0}", args.CurrentSolution));
        }

        private void WireChangeEvent(object sender, ColorChangeEventArgs args)
        {
            if (int.TryParse((sender as FrameworkElement).Tag.ToString(), out int x))
            {
                GetSolution(args, x);
            }
        }
        private void GetSolution(ColorChangeEventArgs args, int index)
        {
            if (args.NewColor != args.OldColor)
            {
                ModuleData.Wires[index] = args.NewColor;
                ModuleSolver.SolveSimpleWires(BombManager, ref ModuleData);
            }
        }
    }
}
