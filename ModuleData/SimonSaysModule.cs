using KTANEAssistant.API;
using KTANEAssistant.Pages.Modules;

namespace KTANEAssistant.ModuleData
{
    public class SimonSaysModule : ModuleModel<SimonColors[]>, INativeModuleModel
    {
        public SimonColors[] FlashSequence;

        public override string FriendlyName => "Simon Says";
        public string PageDescriptor => "p. 8";
        public override DependencyFlag[] Dependencies => new DependencyFlag[] { DependencyFlag.SerialNumber, DependencyFlag.Strikes };

        public override ModulePage CreateModulePage()
        {
            return new SimonSaysPage();
        }
    }

    public enum SimonColors
    {
        Red, Blue, Yellow, Green
    }
}