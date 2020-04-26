using KTANEAssistant.API;
using KTANEAssistant.ModuleData;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace KTANEAssistant.Pages.Modules
{
    /// <summary>
    /// Interaction logic for ButtonPage.xaml
    /// </summary>
    public partial class MorseCodePage : ModulePage
    {
        private MorseCodeModule ModuleData;

        public MorseCodePage()
        {
            InitializeComponent();
            ModuleData = new MorseCodeModule();
            ModuleData.SolutionChangedEvent += ModuleData_SolutionChangedEvent;
        }

        private void ModuleData_SolutionChangedEvent(object sender, SolutionChangedEventArgs<HashSet<MorseResponse>> args)
        {
            DefusalPage.MessageOutput.Text = string.Join(",", args.CurrentSolution.Select(x => x.Word).ToArray());
        }

        private void TextInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextField.InputText))
            {
                ModuleData.Input = TextField.InputText;
                ModuleSolver.SolveMorseCode(ref ModuleData);
            }
        }
    }
}
