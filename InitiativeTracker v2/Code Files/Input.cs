using System;

namespace InitiativeTracker
{
    public static partial class InputParser
    {
        public static void Parse(ConsoleKeyInfo keyInfo) {
            switch (Program.outputData.mode) {
                case Mode.Info:
                    Parse_Info(keyInfo);
                    break;
                case Mode.Select:
                    Parse_Select(keyInfo);
                    break;
                case Mode.Create:
                    Parse_Create(keyInfo);
                    break;
            }
        }

        private const ConsoleKey Confirm = ConsoleKey.Enter;
        private const ConsoleKey Remove = ConsoleKey.Backspace;
        private const ConsoleKey Clear = ConsoleKey.Delete;
        private const ConsoleKey CycleMode = ConsoleKey.M;
        private const ConsoleKey Undo = ConsoleKey.Z;
    }
}