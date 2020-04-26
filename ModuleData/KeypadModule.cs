using KTANEAssistant.API;
using KTANEAssistant.Pages.Modules;
using System.Collections.Generic;

namespace KTANEAssistant.ModuleData
{
    public class KeypadModule : ModuleModel<int[]>, INativeModuleModel
    {
        public int[] Selected;

        public static readonly SymbolImage[] Symbols = new SymbolImage[]
        {
            new SymbolImage() { ResourceID = 0, FriendlyName = "Balloon" },         //balloon
            new SymbolImage() { ResourceID = 1, FriendlyName = "AT" },              //at
            new SymbolImage() { ResourceID = 2, FriendlyName = "Lambda" },          //upsidedowny
            new SymbolImage() { ResourceID = 3, FriendlyName = "Squiggly N" },      //squigglyn
            new SymbolImage() { ResourceID = 4, FriendlyName = "Squid Knife" },     //squidknife
            new SymbolImage() { ResourceID = 5, FriendlyName = "H with Hook" },     //nhook
            new SymbolImage() { ResourceID = 6, FriendlyName = "Left Facing C" },   //leftc
            new SymbolImage() { ResourceID = 7, FriendlyName = "Backwards Euro" },  //euro
            new SymbolImage() { ResourceID = 8, FriendlyName = "Cursive" },         //cursive
            new SymbolImage() { ResourceID = 9, FriendlyName = "Hollow Star" },     //hollowstar
            new SymbolImage() { ResourceID = 10, FriendlyName = "Question Mark" },  //questionmark
            new SymbolImage() { ResourceID = 11, FriendlyName = "Copyright" },      //copyright
            new SymbolImage() { ResourceID = 12, FriendlyName = "Pumpkin" },        //pumpkin
            new SymbolImage() { ResourceID = 13, FriendlyName = "Double K" },       //doublek
            new SymbolImage() { ResourceID = 14, FriendlyName = "Melted 3" },       //meltedthree
            new SymbolImage() { ResourceID = 15, FriendlyName = "Six" },            //six
            new SymbolImage() { ResourceID = 16, FriendlyName = "Paragraph" },      //paragraph
            new SymbolImage() { ResourceID = 17, FriendlyName = "bT" },             //bt
            new SymbolImage() { ResourceID = 18, FriendlyName = "Smiley Face" },    //smileyface
            new SymbolImage() { ResourceID = 19, FriendlyName = "Candelabra" },     //pitchfork
            new SymbolImage() { ResourceID = 20, FriendlyName = "Right Facing C" }, //rightc
            new SymbolImage() { ResourceID = 21, FriendlyName = "Dragon" },         //dragon
            new SymbolImage() { ResourceID = 22, FriendlyName = "Filled Star" },    //filledstar
            new SymbolImage() { ResourceID = 23, FriendlyName = "Train Track" },    //tracks
            new SymbolImage() { ResourceID = 24, FriendlyName = "ae" },             //ae
            new SymbolImage() { ResourceID = 25, FriendlyName = "N with Hat" },     //nwithhat
            new SymbolImage() { ResourceID = 26, FriendlyName = "Omega" },          //omega
        };

        public static readonly int[,] Layouts = new int[6,7]
        { { 0, 1, 2, 3, 4, 5, 6 },
        { 7, 0, 6, 8, 9, 5, 10 },
        { 11, 12, 8, 13, 14, 2, 9 },
        { 15, 16, 17, 4, 13, 10, 18 },
        { 19, 18, 17, 20, 16, 21, 22 },
        { 15, 7, 23, 24, 19, 25, 26 } };

        public static string GetSource(int resourceID)
        {
            return string.Concat("pack://application:,,,/KTANE-Assistant;component/Resources/", resourceID, ".png");
        }

        public override string FriendlyName => "Keypads";
        public string PageDescriptor => "p. 7";

        public HashSet<int> OtherOptions;

        public override ModulePage CreateModulePage()
        {
            return new KeypadsPage();
        }
    }

    public struct SymbolImage
    {
        public int ResourceID;
        public string FriendlyName;
    }
}