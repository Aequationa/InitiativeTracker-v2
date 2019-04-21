using System;
using System.Collections.Generic;
using System.Threading;

namespace InitiativeTracker
{
    partial class OutputData
    {
        public int select_active = 0;

        public List<string> select_errors = new List<string>();
        public void Select_Setup() {
            
        }
    }

    partial class Output
    {
        private const int SelectionTopBorder = 1;
        private const int SelectionLeftBorder = 0;
        private const int SelectionLeftIndent = 2;
        private static void AddSelection(Screen screen, int left) {
            int index = 0;
            int top = SelectionTopBorder;
            // Add Groups
            for(int pos = 0; pos < Program.data.loadedGroups.Count; ++pos) {
                string groupString = "Group '" + Program.data.loadedGroups[pos].name + "'";
                if(index == Program.outputData.select_active) {
                    screen.AddFormattedLine(Program.specialCharacters.rightArrow.ToString(), ConsoleColor.White, ConsoleColor.Black, left + SelectionLeftBorder, int.MaxValue, top);
                }
                screen.AddFormattedLine(groupString, ConsoleColor.White, ConsoleColor.Black, left + SelectionLeftBorder + SelectionLeftIndent, int.MaxValue, top);
                ++index;
                ++top;
            }
            if(Program.data.loadedGroups.Count > 0) {
                AddPartialHLine(screen, left - 1, top);
                ++top;
            }
            // Add Actors
            for(int pos = 0; pos < Program.data.loadedActors.Count; ++pos) {
                string actorString = "Actor '" + Program.data.loadedActors[pos].name + "'";
                if (index == Program.outputData.select_active) {
                    screen.AddFormattedLine(Program.specialCharacters.rightArrow.ToString(), ConsoleColor.White, ConsoleColor.Black, left + SelectionLeftBorder, int.MaxValue, top);
                }
                screen.AddFormattedLine(actorString, ConsoleColor.White, ConsoleColor.Black, left + SelectionLeftBorder + SelectionLeftIndent, int.MaxValue, top);
                ++index;
                ++top;
            }
            if (Program.data.loadedActors.Count > 0) {
                AddPartialHLine(screen, left - 1, top);
                ++top;
            }
            // Add Reload
            string reloadString = "Reload Data";
            if (index == Program.outputData.select_active) {
                screen.AddFormattedLine(Program.specialCharacters.rightArrow.ToString(), ConsoleColor.White, ConsoleColor.Black, left + SelectionLeftBorder, int.MaxValue, top);
            }
            screen.AddFormattedLine(reloadString, ConsoleColor.White, ConsoleColor.Black, left + SelectionLeftBorder + SelectionLeftIndent, int.MaxValue, top);
            ++top;

            // Add Errors
            screen.AddFormattedLines(Program.outputData.select_errors, ConsoleColor.White, ConsoleColor.Black, left, screen.Width, top);
        }

        private static Screen GetScreen_Select() {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            Screen screen = Screen.Default(width, height);
            int separatorPos = GetSeparatorPos(width);

            AddDoubleVLine(screen, separatorPos);
            AddInitiativeList(screen, separatorPos);
            AddSelection(screen, separatorPos + 1);
            return screen;
        }
    }

    partial class InputParser
    {
        private const ConsoleKey Select_Next = ConsoleKey.DownArrow;
        private const ConsoleKey Select_Previous = ConsoleKey.UpArrow;
        private static void Parse_Select(ConsoleKeyInfo keyInfo) {
            Program.outputData.select_errors.Clear();

            switch (keyInfo.Key) {
                    // Undo
                case ConsoleKey.Z:
                    if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0 && Program.data.changes.Count != 0) {
                        Program.data.changes.Pop().Undo();
                    }
                    break;
                    // Change Mode
                case CycleMode:
                    Program.outputData.mode = Mode.Create;
                    break;
                case Select_Next:
                    if (Program.outputData.select_active < Program.data.loadedGroups.Count + Program.data.loadedActors.Count) {
                        ++Program.outputData.select_active;
                    }
                    break;
                case Select_Previous:
                    if(Program.outputData.select_active > 0) {
                        --Program.outputData.select_active;
                    }
                    break;
                case Confirm:
                    if(Program.outputData.select_active < Program.data.loadedGroups.Count) {
                        // Add Group
                        Program.data.AddGroup(Program.data.loadedGroups[Program.outputData.select_active].actors, Program.outputData.select_errors);
                    }
                    else if(Program.outputData.select_active < Program.data.loadedGroups.Count + Program.data.loadedActors.Count) {
                        // Add Actor
                        Program.data.AddActor(Program.data.loadedActors[Program.outputData.select_active - Program.data.loadedGroups.Count], Program.outputData.select_errors);
                    }
                    else {
                        Program.outputData.select_active = 0;
                        // Delete Data
                        Program.data.loadedColoringTypes.Clear();
                        Program.data.loadedGroups.Clear();
                        Program.data.loadedActors.Clear();

                        // Reload Data
                        bool success = FileParser.Load(false);

                        while (!Console.KeyAvailable) {
                            Thread.Sleep(Program.SleepDelta);
                        }
                        Console.ReadKey(true);
                        if (success) {
                            Program.screenWriter.Write(Output.GetScreen());
                        }
                        else {
                            Environment.Exit(0);
                        }
                    }
                    break;
            }
        }
    }
}