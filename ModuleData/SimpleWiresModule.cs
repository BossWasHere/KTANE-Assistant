using KTANEAssistant.API;
using KTANEAssistant.Pages.Modules;
using System.Linq;

namespace KTANEAssistant.ModuleData
{
    public class SimpleWiresModule : ModuleModel<int>, INativeModuleModel
    {
        // Wire codes: 'r' = red, 'w' = white, 'b' = blue, 'k' = black, 'y' = yellow, 'n' = none
        public SimpleWireColors[] Wires = new SimpleWireColors[6];

        public int GetConnectedWires()
        {
            for (int i = 0; i < Wires.Length; i++)
            {
                if (Wires[i].Equals(SimpleWireColors.Off))
                {
                    return i;
                }
            }
            return 6;
        }

        public int FrequencyOf(SimpleWireColors color)
        {
            return Wires.Count(x => x.Equals(color));
        }

        public int FindLastOf(SimpleWireColors color, int upTo)
        {
            for (int i = upTo; i > 0; i--)
            {
                if (Wires[i - 1].Equals(color))
                {
                    return i;
                }
            }
            return -1;
        }

        public override string FriendlyName => "Simple Wires";
        public string PageDescriptor => "p. 5";
        public override DependencyFlag[] Dependencies => new DependencyFlag[] { DependencyFlag.SerialNumber };

        public override ModulePage CreateModulePage()
        {
            return new SimpleWiresPage();
        }
    }

    public enum SimpleWireColors
    {
        Off, Red, White, Blue, Black, Yellow
    }
}
