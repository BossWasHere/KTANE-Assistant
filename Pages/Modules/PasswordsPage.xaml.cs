using KTANEAssistant.API;
using KTANEAssistant.ModuleData;
using System.Collections.Generic;
using System.Windows.Controls;

namespace KTANEAssistant.Pages.Modules
{
    /// <summary>
    /// Interaction logic for PasswordsPage.xaml
    /// </summary>
    public partial class PasswordsPage : ModulePage
    {
        private PasswordModule ModuleData;

        public PasswordsPage()
        {
            InitializeComponent();
            ModuleData = new PasswordModule();
            ModuleData.SolutionChangedEvent += ModuleData_SolutionChangedEvent;
        }

        private void ModuleData_SolutionChangedEvent(object sender, SolutionChangedEventArgs<HashSet<string>> args)
        {
            DefusalPage.MessageOutput.Text = string.Join(",", args.CurrentSolution);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ModuleData.FirstColumn = InputA.Text;
            ModuleData.SecondColumn = InputB.Text;
            ModuleData.ThirdColumn = InputC.Text;
            ModuleData.FourthColumn = InputD.Text;
            ModuleData.FifthColumn = InputE.Text;
            ModuleSolver.SolvePassword(ref ModuleData);
        }
    }
}
