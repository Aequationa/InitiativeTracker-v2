using System;

namespace InitiativeTracker
{
    public static partial class InputParser
    {
        public static void Parse(ConsoleKeyInfo keyInfo) {
            // Force Refresh
            if(keyInfo.Key == ForceRefresh) {
                Program.screenWriter.Write(Output.GetScreen(), true);
            }

            switch (Program.outputData.mode) {
                case Mode.Info:
                    Parse_Info(keyInfo);
                    Program.screenWriter.Write(Output.GetScreen());
                    break;
                case Mode.Select:
                    Parse_Select(keyInfo);
                    Program.screenWriter.Write(Output.GetScreen());
                    break;
                case Mode.Create:
                    Parse_Create(keyInfo);
                    Program.screenWriter.Write(Output.GetScreen());
                    break;
            }
        }
        private const ConsoleKey ForceRefresh = ConsoleKey.F5;
        private const ConsoleKey Confirm = ConsoleKey.Enter;
        private const ConsoleKey Remove = ConsoleKey.Backspace;
        private const ConsoleKey Clear = ConsoleKey.Delete;
        private const ConsoleKey CycleMode = ConsoleKey.M;
        private const ConsoleKey Undo = ConsoleKey.Z;
    }
}