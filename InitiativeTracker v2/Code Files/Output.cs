using System;
using System.Collections.Generic;

namespace InitiativeTracker
{
    public struct SpecialCharacters
    {
        public char vDouble;
        public char hSingle;
        public char hDouble;
        public char cSingle;
        public char cDouble;

        public char rightArrow;
        public char leftArrow;
        public char heath;
        public char armor;
        public void Setup(bool useSpecial)
        {
            if (useSpecial) {
                vDouble = '║';
                hSingle = '─';
                hDouble = '═';
                cSingle = '╟';
                cDouble = '╠';

                rightArrow = '►';
                leftArrow = '◄';
                heath = '♥';
                armor = '▼';
            }
            else {
                vDouble = '*';
                hSingle = '*';
                hDouble = '*';
                cSingle = '*';
                cDouble = '*';

                rightArrow = '-';
                leftArrow = '-';
                heath = 'H';
                armor = 'A';
            }
        }
    }

    public enum Mode : byte
    {
        Info, Select, Create
    }
    public partial class OutputData
    {
        public Mode mode;

        public void Setup() {
            mode = Mode.Info;
            Info_Setup();
            Select_Setup();
            Create_Setup();
        }
    }

    public static partial class Output
    {
        // === Loading ===
        public static Screen GetFatalErrorLoadingScreen(List<string> errors) {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            Screen screen = Screen.Default(width, height);
            int top = 0;
            top += screen.AddFormattedLines(new List<string> { "> Encountered Errors while loading Settings:" } ,
                ConsoleColor.White, ConsoleColor.Black, 0, width, top);
            top += screen.AddFormattedLines(errors ,
                ConsoleColor.Red, ConsoleColor.Black, 0, width, top);
            top += screen.AddFormattedLines(new List<string> { "> Press any Key to Exit App" },
                ConsoleColor.White, ConsoleColor.Black, 0, width, top);

            return screen;
        }
        public static Screen GetSuccessfulLoadingScreen(List<string> errors, bool isStartUp) {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            Screen screen = Screen.Default(width, height);
            int top = 0;
            if (isStartUp) {
                top += screen.AddFormattedLines(new List<string> {
                    "> Settings loaded.",
                    "> Colorings loaded.",
                    "> Actors loaded."
                }, ConsoleColor.White, ConsoleColor.Black, 0, width, top);
            }
            else {
                top += screen.AddFormattedLines(new List<string> {
                    "> Colorings loaded.",
                    "> Actors loaded."
                }, ConsoleColor.White, ConsoleColor.Black, 0, width, top);
            }
            if(errors.Count == 0) {
                screen.AddFormattedLines(new List<string> {
                    "> Press any Key to continue." },
                ConsoleColor.White, ConsoleColor.Black, 0, width, top);
            }
            else {
                top += screen.AddFormattedLines(new List<string> {
                    "> Encountered the following nonfatal Error(s) while loading:" },
                    ConsoleColor.White, ConsoleColor.Black, 0, width, top);
                top += screen.AddFormattedLines(errors, 
                    ConsoleColor.Red, ConsoleColor.Black, 0, width, top);
                top += screen.AddFormattedLines(new List<string> { "> Press any Key to continue." },
                    ConsoleColor.White, ConsoleColor.Black, 0, width, top);
            }
            return screen;
        }

        public static Screen GetScreen() {
            switch (Program.outputData.mode) {
                case Mode.Info:
                    return Info_GetScreen();
                case Mode.Select:
                    return GetScreen_Select();
                case Mode.Create:
                    return GetScreen_Create();
                default:
                    throw new ArgumentOutOfRangeException(nameof(Program.outputData.mode));
            }
        }
        
        private static int GetSeparatorPos(int width) {
            return Math.Min(width / 2, 60);
        }
        private const ConsoleColor LineColor = ConsoleColor.Gray;
        private static void AddDoubleVLine(this Screen screen, int x) {
            if (x >= 0 && x < screen.Width) {
                for(int pos = 0; pos < screen.Height; ++pos) {
                    screen.SetC(x, pos, Program.specialCharacters.vDouble);
                    screen.SetForeground(x, pos, LineColor);
                }
            }
        }
        private static void AddPartialDoubleHLine(this Screen screen, int x, int y) {
            if (y >= 0 && y < screen.Height) {
                if (x >= 0 && x < screen.Width) {
                    screen.SetC(x, y, Program.specialCharacters.cDouble);
                }
                for(int pos = Math.Max(0, x + 1); pos < screen.Width; ++pos) {
                    screen.SetC(pos, y, Program.specialCharacters.hDouble);
                    screen.SetForeground(pos, y, LineColor);
                }
            }
        }
        private static void AddPartialHLine(this Screen screen, int x, int y) {
            if (y >= 0 && y < screen.Height) {
                if (x >= 0 && x < screen.Width) {
                    screen.SetC(x, y, Program.specialCharacters.cSingle);
                }
                for(int pos = Math.Max(0, x + 1); pos < screen.Width; ++pos) {
                    screen.SetC(pos, y, Program.specialCharacters.hSingle);
                    screen.SetForeground(pos, y, LineColor);
                }
            }
        }
        private static void AddFullDoubleHLine(this Screen screen, int y) {
            if (y >= 0 && y < screen.Height) {
                for(int x = 0; x < screen.Width; ++x) {
                    screen.SetC(x, y, Program.specialCharacters.hDouble);
                    screen.SetForeground(x, y, LineColor);
                    screen.SetBackground(x, y, ConsoleColor.Black);
                }
            }
        }
        private static void AddFullHLine(this Screen screen, int y) {
            if (y >= 0 && y < screen.Height) {
                for (int x = 0; x < screen.Width; ++x) {
                    screen.SetC(x, y, Program.specialCharacters.hSingle);
                    screen.SetForeground(x, y, LineColor);
                    screen.SetBackground(x, y, ConsoleColor.Black);
                }
            }
        }

        private const int ListLeftBorder = 2;
        private const int ListRightBorder = 2;
        private const int ListTopBorder = 1;
        private const ConsoleColor ArmorColor = ConsoleColor.White;
        private const ConsoleColor HealthColor = ConsoleColor.Red;
        private static void AddInitiativeList(this Screen screen, int right) {
            // Add Name, AC, HP for each, formatted like 'This is the Name ▼ 15 ♥ 10+20/20'
            for(int index = 0; index < Program.data.idList.Count; ++index) {
                var actor = Program.data.GetActor(Program.data.idList[index]);
                var foreground = index == Program.outputData.Info_GetActive() ? actor.active_text : actor.base_text;
                var background = index == Program.outputData.Info_GetActive() ? actor.active_bg : actor.base_bg;

                int width = 0;
                // Get HP
                string hpString;
                if(actor.temporaryHP > 0) {
                    hpString = actor.temporaryHP.ToString() + "+" + actor.currentHP.ToString() + "/" + actor.maximumHP.ToString() + " ";
                }
                else {
                    hpString = actor.currentHP.ToString() + "/" + actor.maximumHP.ToString() + " ";
                }
                width += hpString.Length;
                screen.AddFormattedLine(hpString, foreground, background, right - ListRightBorder - width, int.MaxValue, index + ListTopBorder);
                width += 3;
                screen.AddFormattedLine(" " + Program.specialCharacters.heath + " ", HealthColor, background, right - ListRightBorder - width, int.MaxValue, index + ListTopBorder);
                // Get AC
                if (actor.armorClass.HasValue) {
                    string acString = actor.armorClass.Value.ToString();
                    width += acString.Length;
                    screen.AddFormattedLine(acString, foreground, background, right - ListRightBorder - width, int.MaxValue, index + ListTopBorder);
                    width += 3;
                    screen.AddFormattedLine(" " + Program.specialCharacters.armor + " ", ArmorColor, background, right - ListRightBorder - width, int.MaxValue, index + ListTopBorder);
                }
                // Get Name
                screen.AddFormattedFullLine(" " + actor.name, foreground, background, ListLeftBorder, right - ListRightBorder - width, index + ListTopBorder);

                // Add Selected Indicator
                
            }
            // Add Active & Selected Indicators
            screen.AddFormattedLine(Program.specialCharacters.rightArrow.ToString(), ConsoleColor.White, ConsoleColor.Black, 0, int.MaxValue, Program.outputData.Info_GetActive() + ListTopBorder);
            if (!Program.outputData.Info_GetActiveIsSelected()) {
                screen.AddFormattedLine(Program.specialCharacters.leftArrow.ToString(), ConsoleColor.White, ConsoleColor.Black, ListLeftBorder - 1, int.MaxValue, Program.outputData.Info_GetSelected() + ListTopBorder);
            }
        }
    }
}