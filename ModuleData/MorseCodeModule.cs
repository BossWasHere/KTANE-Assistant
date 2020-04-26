using KTANEAssistant.API;
using KTANEAssistant.Pages.Modules;
using System.Collections.Generic;
using System.Linq;

namespace KTANEAssistant.ModuleData
{
    public class MorseCodeModule : ModuleModel<HashSet<MorseResponse>>, INativeModuleModel
    {
        public static readonly Dictionary<string, char> Morse = new Dictionary<string, char> {
            {".-", 'a'}, {"-...", 'b'}, {"-.-.", 'c'}, {"-..", 'd'}, {".", 'e'},
            {"..-.", 'f'}, {"--.", 'g'}, {"....", 'h'}, {"..", 'i'}, {".---", 'j'},
            {"-.-", 'k'}, {".-..", 'l'}, {"--", 'm'}, {"-.", 'n'}, {"---", 'o'},
            {".--.", 'p'}, {"--.-", 'q'}, {".-.", 'r'}, {"...", 's'}, {"-", 't'},
            {"..-", 'u'}, {"...-", 'v'}, {".--", 'w'}, {"-..-", 'x'}, {"-.--", 'y'},
            {"--..", 'z'}, {".----", '1'}, {"..---", '2'}, {"...--", '3'}, {"....-", '4'},
            {".....", '5'}, {"-....", '6'}, {"--...", '7'}, {"---..", '8'}, {"----.", '9'},
            {"-----", '0'}
        };
        public static readonly Dictionary<char, string> ReverseMorse = Morse.ToDictionary(x => x.Value, x => x.Key);

        public static readonly string[] Words = new string[] { "shell", "halls", "slick", "trick", "boxes", "leaks", "strobe", "bistro", "flick", "bombs", "break", "brick", "steak", "sting", "vector", "beats" };
        public static readonly string[] MorseWords = Words.Select(x => Morseify(x)).ToArray();
        public static readonly string[] Responses = new string[] { "3.505", "3.515", "3.522", "3.532", "3.535", "3.542", "3.545", "3.552", "3.555", "3.565", "3.572", "3.575", "3.582", "3.592", "3.595", "3.600" };

        public string Input;

        public static string Morseify(string input)
        {
            return string.Join(string.Empty, input.Select(x =>
            {
                ReverseMorse.TryGetValue(x, out string y);
                return y;
            }));
        }

        public override string FriendlyName => "Morse Code";
        public string PageDescriptor => "p. 12";

        public override ModulePage CreateModulePage()
        {
            return new MorseCodePage();
        }
    }

    public struct MorseResponse
    {
        public string Word;
        public string Response;
    }
}