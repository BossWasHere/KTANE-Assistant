using KTANEAssistant.Pages.Modules;

using KTANEAssistant.API;

namespace KTANEAssistant.ModuleData
{
    public class KnobsModule : ModuleModel<Direction>, INativeModuleModel
    {
        public static readonly bool[,] Configurations = new bool[,]
        {
            { false, false, true, false, true, true, true, true, true, true, false, true },
            { true, false, true, false, true, false, false, true, true, false, true, true },
            { false, true, true, false, false, true, true, true, true, true, false, true },
            { true, false, true, false, true, false, false, true, false, false, false, true },
            { false, false, false, false, true, false, true, false, false, true, true, true },
            { false, false, false, false, true, false, false, false, false, true, true, false },
            { true, false, true, true, true, true, true, true, true, false, true, false },
            { true, false, true, true, false, false, true, true, true, false, true, false }
        };

        public bool[] LitLights;
        public bool UnknownState = false;

        public override string FriendlyName => "Knobs";
        public string PageDescriptor => "p. 20";

        public override ModulePage CreateModulePage()
        {
            return new KnobsPage();
        }
    }
}