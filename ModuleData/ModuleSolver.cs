using KTANEAssistant.API;
using System.Collections.Generic;
using System.Linq;

namespace KTANEAssistant.ModuleData
{
    public static class ModuleSolver
    {
        public static void SolveSimpleWires(BombManager bombData, ref SimpleWiresModule moduleData)
        {
            switch (moduleData.GetConnectedWires())
            {
                case 3:
                    if (moduleData.FrequencyOf(SimpleWireColors.Red) == 0) moduleData.SetSolution(2);
                    else if (moduleData.Wires[2] == SimpleWireColors.White) moduleData.SetSolution(3);
                    else if (moduleData.FrequencyOf(SimpleWireColors.Blue) > 1) moduleData.SetSolution(moduleData.FindLastOf(SimpleWireColors.Blue, 3));
                    else moduleData.SetSolution(3);
                    break;
                case 4:
                    if (moduleData.FrequencyOf(SimpleWireColors.Red) > 0 && bombData.IsLastSerialDigitOdd())
                    {
                        moduleData.SetSolution(moduleData.FindLastOf(SimpleWireColors.Red, 4));
                        moduleData.RaiseDependencyFlag(DependencyFlag.SerialNumber);
                    }
                    else if (moduleData.Wires[3] == SimpleWireColors.Yellow && moduleData.FrequencyOf(SimpleWireColors.Red) == 0) moduleData.SetSolution(1);
                    else if (moduleData.FrequencyOf(SimpleWireColors.Blue) == 1) moduleData.SetSolution(1);
                    else if (moduleData.FrequencyOf(SimpleWireColors.Yellow) > 1) moduleData.SetSolution(4);
                    else moduleData.SetSolution(2);
                    break;
                case 5:
                    if (moduleData.Wires[4] == SimpleWireColors.Black && bombData.IsLastSerialDigitOdd())
                    {
                        moduleData.SetSolution(4);
                        moduleData.RaiseDependencyFlag(DependencyFlag.SerialNumber);
                    }
                    else if (moduleData.FrequencyOf(SimpleWireColors.Red) == 1 && moduleData.FrequencyOf(SimpleWireColors.Yellow) > 1) moduleData.SetSolution(1);
                    else if (moduleData.FrequencyOf(SimpleWireColors.Black) == 0) moduleData.SetSolution(2);
                    else moduleData.SetSolution(1);
                    break;
                case 6:
                    if (moduleData.FrequencyOf(SimpleWireColors.Yellow) == 0 && bombData.IsLastSerialDigitOdd())
                    {
                        moduleData.SetSolution(3);
                        moduleData.RaiseDependencyFlag(DependencyFlag.SerialNumber);
                    }
                    else if (moduleData.FrequencyOf(SimpleWireColors.Yellow) == 1 && moduleData.FrequencyOf(SimpleWireColors.White) > 1) moduleData.SetSolution(4);
                    else if (moduleData.FrequencyOf(SimpleWireColors.Red) == 0) moduleData.SetSolution(6);
                    else moduleData.SetSolution(4);
                    break;
                default:
                    moduleData.SetSolution(-1);
                    break;
            }
        }

        public static void SolveButton(BombManager bombData, ref ButtonModule moduleData)
        {
            if (moduleData.Strip == ButtonColor.Disabled)
            {
                if (moduleData.Color == ButtonColor.Blue && moduleData.Text == ButtonText.Abort) moduleData.SetSolution(ButtonPhase.HoldAwait);
                else if (bombData.Batteries > 1 && moduleData.Text == ButtonText.Detonate)
                {
                    moduleData.SetSolution(ButtonPhase.ReleaseImmediately);
                    moduleData.RaiseDependencyFlag(DependencyFlag.Batteries);
                }
                else if (bombData.IsIndicatorLit(Indicators.CAR) && moduleData.Color == ButtonColor.White)
                {
                    moduleData.SetSolution(ButtonPhase.HoldAwait);
                    moduleData.RaiseDependencyFlag(DependencyFlag.IndicatorCAR);
                }
                else if (bombData.Batteries > 2 && bombData.IsIndicatorLit(Indicators.FRK))
                {
                    moduleData.SetSolution(ButtonPhase.ReleaseImmediately);
                    moduleData.RaiseDependencyFlag(DependencyFlag.Batteries);
                    moduleData.RaiseDependencyFlag(DependencyFlag.IndicatorFRK);
                }
                else if (moduleData.Color == ButtonColor.Yellow) moduleData.SetSolution(ButtonPhase.HoldAwait);
                else if (moduleData.Color == ButtonColor.Red && moduleData.Text == ButtonText.Hold) moduleData.SetSolution(ButtonPhase.ReleaseImmediately);
                else moduleData.SetSolution(ButtonPhase.HoldAwait);
            }
            else
            {
                switch (moduleData.Strip)
                {
                    case ButtonColor.Blue:
                        moduleData.SetSolution(ButtonPhase.ReleaseFour);
                        break;
                    case ButtonColor.Yellow:
                        moduleData.SetSolution(ButtonPhase.ReleaseFive);
                        break;
                    default:
                        moduleData.SetSolution(ButtonPhase.ReleaseOne);
                        break;
                }
            }
        }

        public static void SolveKeypads(ref KeypadModule moduleData)
        {
            HashSet<int> otherOptions = new HashSet<int>();
            Reorder[] order = new Reorder[4];
            int setTarget = moduleData.Selected.Count(x => x != -1);
            int i, j, k, l = 0;
            var Layouts = KeypadModule.Layouts;
            for (i = 0; i < Layouts.GetLength(0); i++)
            {
                for (j = 0; j < Layouts.GetLength(1); j++)
                {
                    //Search for potential direct matches
                    k = Layouts[i, j];
                    if (k == moduleData.Selected[l])
                    {
                        order[l] = new Reorder { Position = j, Value = k };
                        l++;
                        //Break out on a full match
                        if (l > 3)
                        {
                            moduleData.SetSolution(order.OrderBy(x => x.Position).Select(x => x.Value).ToArray());
                            return;
                        }
                        //Loop current set again for next entry
                        j = -1;
                    }
                }
                //Add to potential list if every matching element accounted for
                if (l == setTarget)
                {
                    for (j = 0; j < Layouts.GetLength(1); j++)
                    {
                        otherOptions.Add(Layouts[i, j]);
                    }
                }
                l = 0;
            }
            moduleData.OtherOptions = otherOptions;
            moduleData.SetSolution(null);
        }

        public static void SolveSimonSays(BombManager bombData, ref SimonSaysModule moduleData)
        {
            int len = moduleData.FlashSequence.Length;
            SimonColors[] returnSequence = new SimonColors[len];
            for (int i = 0; i < len; i++)
            {
                returnSequence[i] = GetSimonColor(moduleData.FlashSequence[i], bombData.Strikes, bombData.SerialContainsVowel());
            }
            moduleData.SetSolution(returnSequence);
            moduleData.RaiseDependencyFlag(DependencyFlag.SerialNumber);
            moduleData.RaiseDependencyFlag(DependencyFlag.Strikes);
        }

        public static SimonColors GetSimonColor(SimonColors flash, int strikes, bool vowel)
        {
            switch (flash)
            {
                case SimonColors.Red:
                    {
                        switch (strikes)
                        {
                            case 0:
                                return SimonColors.Blue;
                            case 1:
                                return vowel ? SimonColors.Yellow : SimonColors.Red;
                            default:
                                return vowel ? SimonColors.Green : SimonColors.Yellow;
                        }
                    }
                case SimonColors.Blue:
                    {
                        switch (strikes)
                        {
                            case 0:
                                return vowel ? SimonColors.Red : SimonColors.Yellow;
                            case 1:
                                return vowel ? SimonColors.Green : SimonColors.Blue;
                            default:
                                return vowel ? SimonColors.Red : SimonColors.Green;
                        }
                    }
                case SimonColors.Green:
                    {
                        switch (strikes)
                        {
                            case 0:
                                return vowel ? SimonColors.Yellow : SimonColors.Green;
                            case 1:
                                return vowel ? SimonColors.Blue : SimonColors.Yellow;
                            default:
                                return vowel ? SimonColors.Yellow : SimonColors.Blue;
                        }
                    }
                default:
                    {
                        switch (strikes)
                        {
                            case 0:
                                return vowel ? SimonColors.Green : SimonColors.Red;
                            case 1:
                                return vowel ? SimonColors.Red : SimonColors.Green;
                            default:
                                return vowel ? SimonColors.Blue : SimonColors.Red;
                        }
                    }
            }
        }

        public static void SolveWhoFirst(ref WhoIsModule moduleData)
        {
            // implementation t.b.d.
        }

        public static void SolveMemory(ref MemoryModule moduleData)
        {
            MemoryPush mp = new MemoryPush() { PositionInsteadOfValue = false };
            int stage = moduleData.Stage;
            int display = moduleData.CurrentDisplay;
            switch (stage)
            {
                case 1:
                    mp.Position = display == 1 ? 2 : display;
                    mp.PositionInsteadOfValue = true;
                    break;
                case 2:
                    switch (display)
                    {
                        case 1:
                            mp.Target = 4;
                            break;
                        case 3:
                            mp.Position = 1;
                            mp.PositionInsteadOfValue = true;
                            break;
                        default:
                            mp.Position = moduleData.History[0].Position;
                            mp.PositionInsteadOfValue = true;
                            break;
                    }
                    break;
                case 3:
                    switch (display)
                    {
                        case 1:
                            mp.Target = moduleData.History[1].Target;
                            break;
                        case 2:
                            mp.Target = moduleData.History[0].Target;
                            break;
                        case 3:
                            mp.Position = 3;
                            mp.PositionInsteadOfValue = true;
                            break;
                        default:
                            mp.Target = 4;
                            break;
                    }
                    break;
                case 4:
                    switch (display)
                    {
                        case 1:
                            mp.Position = moduleData.History[0].Position;
                            break;
                        case 2:
                            mp.Position = 1;
                            break;
                        default:
                            mp.Position = moduleData.History[1].Position;
                            break;
                    }
                    mp.PositionInsteadOfValue = true;
                    break;
                default:
                    switch (display)
                    {
                        case 1:
                            mp.Target = moduleData.History[0].Target;
                            break;
                        case 2:
                            mp.Target = moduleData.History[1].Target;
                            break;
                        case 3:
                            mp.Target = moduleData.History[3].Target;
                            break;
                        default:
                            mp.Target = moduleData.History[2].Target;
                            break;
                    }
                    break;
            }
            if (stage < 5)
            {
                moduleData.History[stage - 1] = mp;
            }
            moduleData.SetSolution(mp);
        }

        public static void SolveMorseCode(ref MorseCodeModule moduleData)
        {
            var filter = new string(moduleData.Input.Where(x => x.Equals('.') || x.Equals('-')).ToArray());
            HashSet<MorseResponse> morseResponses = new HashSet<MorseResponse>();
            for (int i = 0; i < MorseCodeModule.MorseWords.Length; i++)
            {
                if (MorseCodeModule.MorseWords[i].Contains(filter)) morseResponses.Add(new MorseResponse() { Response = MorseCodeModule.Responses[i], Word = MorseCodeModule.Words[i] });
            }
            /*string[] encoded = moduleData.Input.Split(' ');
            HashSet<MorseResponse> morseResponses = new HashSet<MorseResponse>();
            StringBuilder builder = new StringBuilder();
            int i;
            for (i = 0; i < encoded.Length; i++)
            {
                MorseCodeModule.Morse.TryGetValue(encoded[i], out char val);
                if (char.IsLetterOrDigit(val))
                {
                    builder.Append(val);
                }
            }
            string constructedWord = builder.ToString();
            for (i = 0; i < MorseCodeModule.Words.Length; i++)
            {
                string word = MorseCodeModule.Words[i];
                if (word.StartsWith(constructedWord))
                {
                    morseResponses.Add(new MorseResponse() { Response = MorseCodeModule.Responses[i], Word = word });
                }
            }*/
            moduleData.SetSolution(morseResponses);
        }

        public static void SolveComplicatedWires(BombManager bombData, ref ComplicatedWiresModule moduleData)
        {
            bool output;
            if (moduleData.RedColoring)
            {
                if (moduleData.BlueColoring)
                {
                    if (moduleData.Star)
                    {
                        if (moduleData.LED)
                        {
                            output = false;
                        }
                        else
                        {
                            output = bombData.ParallelPort;
                            moduleData.RaiseDependencyFlag(DependencyFlag.ParallelPort);
                        }
                    }
                    else
                    {
                        output = !bombData.IsLastSerialDigitOdd();
                        moduleData.RaiseDependencyFlag(DependencyFlag.SerialNumber);
                    }
                }
                else
                {
                    if (moduleData.LED)
                    {
                        output = bombData.Batteries > 1;
                        moduleData.RaiseDependencyFlag(DependencyFlag.Batteries);
                    }
                    else
                    {
                        if (moduleData.Star)
                        {
                            output = true;
                        }
                        else
                        {
                            output = !bombData.IsLastSerialDigitOdd();
                            moduleData.RaiseDependencyFlag(DependencyFlag.SerialNumber);
                        }
                    }
                }
            }
            else
            {
                if (moduleData.BlueColoring)
                {
                    if (moduleData.LED)
                    {
                        output = bombData.ParallelPort;
                        moduleData.RaiseDependencyFlag(DependencyFlag.ParallelPort);
                    }
                    else
                    {
                        if (moduleData.Star)
                        {
                            output = false;
                        }
                        else
                        {
                            output = !bombData.IsLastSerialDigitOdd();
                            moduleData.RaiseDependencyFlag(DependencyFlag.SerialNumber);
                        }
                    }
                }
                else
                {
                    if (moduleData.Star)
                    {
                        if (moduleData.LED)
                        {
                            output = bombData.Batteries > 1;
                            moduleData.RaiseDependencyFlag(DependencyFlag.Batteries);
                        }
                        else
                        {
                            output = true;
                        }
                    }
                    else
                    {
                        if (moduleData.LED)
                        {
                            output = false;
                        }
                        else
                        {
                            output = true;
                        }
                    }
                }
            }
            moduleData.SetSolution(output);
        }

        public static void SolveWireSequences(ref WireSequencesModule moduleData)
        {
            // implementation t.b.d.
        }

        public static void SolveMaze(ref MazeModule moduleData)
        {
            // implementation t.b.d.
        }

        public static void SolvePassword(ref PasswordModule moduleData)
        {
            HashSet<string> possible = new HashSet<string>(PasswordModule.Passwords);
            FilterPasswordsBy(ref possible, 0, moduleData.FirstColumn);
            FilterPasswordsBy(ref possible, 1, moduleData.SecondColumn);
            FilterPasswordsBy(ref possible, 2, moduleData.ThirdColumn);
            FilterPasswordsBy(ref possible, 3, moduleData.FourthColumn);
            FilterPasswordsBy(ref possible, 4, moduleData.FifthColumn);
            moduleData.SetSolution(possible);
        }

        public static void FilterPasswordsBy(ref HashSet<string> shortlist, int offset, string chars)
        {
            if (!string.IsNullOrWhiteSpace(chars))
            {
                shortlist = FilterPossiblePasswords(chars.ToLower().ToCharArray(), offset, shortlist).ToHashSet();
            }
        }

        public static IEnumerable<string> FilterPossiblePasswords(char[] charSelection, int offset, HashSet<string> shortlist)
        {
            foreach (char c in charSelection)
            {
                foreach (string pwd in shortlist)
                {
                    if (pwd.Substring(offset, 1).Equals(c.ToString()))
                    {
                        yield return pwd;
                    }
                }
            }
        }

        public static void SolveKnobs(ref KnobsModule moduleData)
        {
            bool successful = false;
            if (KnobsModule.Configurations.GetLength(1) != moduleData.LitLights.Length)
            {
                moduleData.UnknownState = true;
                return;
            }
            for (int i = 0; i < KnobsModule.Configurations.Length; i++)
            {
                for (int j = 0; j < moduleData.LitLights.Length; j++)
                {
                    if (KnobsModule.Configurations[i,j] != moduleData.LitLights[j])
                    {
                        successful = false;
                        break;
                    }
                    successful = true;
                }
                if (successful)
                {
                    switch (i)
                    {
                        case 0:
                        case 1:
                            moduleData.SetSolution(Direction.Up);
                            break;
                        case 2:
                        case 3:
                            moduleData.SetSolution(Direction.Down);
                            break;
                        case 4:
                        case 5:
                            moduleData.SetSolution(Direction.Left);
                            break;
                        default:
                            moduleData.SetSolution(Direction.Right);
                            break;
                    }
                    break;
                }
            }
            moduleData.UnknownState = !successful;
        }

        private struct Reorder
        {
            internal int Position;
            internal int Value;
        }
    }
}
