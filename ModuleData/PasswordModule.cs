using KTANEAssistant.API;
using KTANEAssistant.Pages.Modules;
using System.Collections.Generic;

namespace KTANEAssistant.ModuleData
{
    public class PasswordModule : ModuleModel<HashSet<string>>, INativeModuleModel
    {
        public static readonly HashSet<string> Passwords = new HashSet<string>() { "about", "after", "again", "below", "could", "every", "first", "found", "great", "house",
                                                             "large", "learn", "never", "other", "place", "plant", "point", "right", "small", "sound",
                                                             "spell", "still", "study", "their", "there", "these", "thing", "think", "three", "water",
                                                             "where", "which", "world", "would", "write" };

        public string FirstColumn, SecondColumn, ThirdColumn, FourthColumn, FifthColumn;

        public override string FriendlyName => "Passwords";
        public string PageDescriptor => "p. 16";

        public override ModulePage CreateModulePage()
        {
            return new PasswordsPage();
        }
    }
}