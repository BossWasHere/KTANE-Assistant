using KTANEAssistant.API;
using KTANEAssistant.Pages.Modules;

namespace KTANEAssistant.ModuleData
{
    public class MemoryModule : ModuleModel<MemoryPush>, INativeModuleModel
    {
        public int Stage = 1;
        public int CurrentDisplay;
        public MemoryPush[] History = new MemoryPush[4];

        public override string FriendlyName => "Memory";
        public string PageDescriptor => "p. 11";

        public override ModulePage CreateModulePage()
        {
            return new MemoryPage();
        }
    }

    public struct MemoryPush
    {
        public bool PositionInsteadOfValue;
        public int Target;
        public int Position;
    }
}