using KTANEAssistant.API;
using KTANEAssistant.Pages.Modules;

namespace KTANEAssistant.ModuleData
{
    public class ComplicatedWiresModule : ModuleModel<bool>, INativeModuleModel
    {
        public bool RedColoring, BlueColoring, Star, LED;
        
        public override string FriendlyName => "Complicated Wires";
        public override DependencyFlag[] Dependencies => new DependencyFlag[] { DependencyFlag.SerialNumber, DependencyFlag.Batteries, DependencyFlag.ParallelPort };
        public string PageDescriptor => "p. 13";

        public override ModulePage CreateModulePage()
        {
            return new ComplicatedWiresPage();
        }
    }
}