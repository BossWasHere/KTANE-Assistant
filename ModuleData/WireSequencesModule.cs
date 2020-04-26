using KTANEAssistant.API;
using KTANEAssistant.Pages.Modules;

namespace KTANEAssistant.ModuleData
{
    public class WireSequencesModule : ModuleModel<string>, INativeModuleModel
    {
        public override string FriendlyName => "Wire Sequences";
        public string PageDescriptor => "p. 14";

        public override ModulePage CreateModulePage()
        {
            return new WireSequencesPage();
        }
    }
}