using KTANEAssistant.API;
using KTANEAssistant.Pages.Modules;

namespace KTANEAssistant.ModuleData
{
    public class WhoIsModule : ModuleModel<string>, INativeModuleModel
    {
        public override string FriendlyName => "Who's On First";
        public string PageDescriptor => "p. 9-10";

        public override ModulePage CreateModulePage()
        {
            return new WhoIsPage();
        }
    }
}