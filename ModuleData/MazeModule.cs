using KTANEAssistant.API;
using KTANEAssistant.Pages.Modules;
using System.Collections.Generic;

namespace KTANEAssistant.ModuleData
{
    public class MazeModule : ModuleModel<List<MazeInstruction>>, INativeModuleModel
    {
        public override string FriendlyName => "Mazes";
        public string PageDescriptor => "p. 15";

        public override ModulePage CreateModulePage()
        {
            return new MazesPage();
        }
    }

    public struct MazeInstruction
    {
        public Direction Direction;
        public int Squares;
    }
}