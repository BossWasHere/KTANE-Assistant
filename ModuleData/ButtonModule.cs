using KTANEAssistant.API;
using KTANEAssistant.Pages.Modules;

namespace KTANEAssistant.ModuleData
{
    public class ButtonModule : ModuleModel<ButtonPhase>, INativeModuleModel
    {
        public ButtonText Text;
        public ButtonColor Color;
        public ButtonColor Strip;

        public override string FriendlyName => "The Button";
        public override DependencyFlag[] Dependencies => new DependencyFlag[] { DependencyFlag.Batteries, DependencyFlag.IndicatorCAR, DependencyFlag.IndicatorFRK };
        public string PageDescriptor => "p. 6";

        public override ModulePage CreateModulePage()
        {
            return new ButtonPage();
        }
    }

    public enum ButtonPhase
    {
        ReleaseImmediately, HoldAwait, ReleaseFour, ReleaseOne, ReleaseFive
    }

    public enum ButtonText
    {
        Abort, Detonate, Hold, Press
    }

    public enum ButtonColor
    {
        Red, Blue, Yellow, White, Other, Disabled
    }

}
