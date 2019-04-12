using System;
using System.Collections.Generic;
using System.Text;

namespace InitiativeTracker
{
    public enum Info_OpMode : byte
    {
        LoseHealth, GainHealth, GainTemp, Condition, Note, Remove
    }
    public static class Info_OpModeExt
    {
        private static readonly string[] opModeNames = new string[] {
            "Lose_Health", "Gain_Health", "Gain_Temp", "Add_Condition", "Add_Note", "Remove" };

        public static string GetName(this Info_OpMode opMode) {
            return opModeNames[(int)opMode];
        }
    }

    partial class OutputData
    {
        private int info_active = 0;
        private int info_selected = 0;
        private bool info_activeIsSelected = true;
        public void Info_SetActive(int value, int selectedID) {
            info_active = value;
            if (info_activeIsSelected) {
                info_selected = value;
                if (Program.data.idList.Count > 0 && Program.data.idList[info_selected] != selectedID) {
                    Info_RollAttack();
                }
            }
        }
        public int Info_GetActive() {
            return info_active;
        }
        public void Info_SetSelected(int value, int selectedID) {
            if (!info_activeIsSelected) {
                info_selected = value;
                if (Program.data.idList.Count > 0 && Program.data.idList[info_selected] != selectedID) {
                    Info_RollAttack();
                }
            }
        }
        public int Info_GetSelected() {
            return info_selected;
        }
        public void Info_ChangetActiveIsSelected(int selectedID) {
            if (info_activeIsSelected) {
                info_activeIsSelected = false;
            }
            else {
                info_activeIsSelected = true;
                info_selected = info_active;
                if (Program.data.idList.Count > 0 && Program.data.idList[info_selected] != selectedID) {
                    Info_RollAttack();
                }
            }
        }
        public bool Info_GetActiveIsSelected() {
            return info_activeIsSelected;
        }

        public List<Tuple<string, string>> info_attackValues = new List<Tuple<string, string>>();
        public void Info_RollAttack() {
            info_attackValues.Clear();
            if (Program.data.idList.Count > 0) {
                var attacks = Program.data.GetActor(Program.data.idList[Program.outputData.info_selected]).attacks;
                for (int index = 0; index < attacks.Count; ++index) {
                    var attack = ObjectParser.EvaluateTokens(attacks[index].attack);
                    var damage = ObjectParser.EvaluateTokens(attacks[index].damage);
                    info_attackValues.Add(new Tuple<string, string>(
                        attack.HasValue ? attack.Value.ToString() : "?",
                        damage.HasValue ? damage.Value.ToString() : "?"));
                }
            }
        }

        public bool info_details = false;

        public Info_OpMode info_opMode = Info_OpMode.LoseHealth;

        private List<char> info_argument = new List<char>();
        private string info_argumentString = string.Empty;
        public string Info_GetArgument() {
            return info_argumentString;
        }
        public void Info_Argument_Add(char c) {
            info_argument.Add(c);
            info_argumentString = string.Concat(info_argument);
            Info_UpdateArgumentInfo();
        }
        public void Info_Argument_Remove() {
            if(info_argument.Count != 0) {
                info_argument.RemoveAt(info_argument.Count - 1);
                info_argumentString = string.Concat(info_argument);
            }
            Info_UpdateArgumentInfo();
        }
        public void Info_Argument_Clear() {
            if(info_argument.Count != 0) {
                info_argument.Clear();
                info_argumentString = string.Empty;
            }
            Info_UpdateArgumentInfo();
        }

        public bool info_argument_valid;
        public Option<Condition> info_argument_condition;
        public void Info_UpdateArgumentInfo() {
            Actor actor;
            if(Program.data.idList.Count == 0) {
                actor = null;
            }
            else {
                actor = Program.data.GetActor(Program.data.idList[info_selected]);
            }
            if(actor == null) {
                info_argument_valid = false;
                info_argument_condition = Option<Condition>.Null;
            }
            else {
                switch (info_opMode) {
                    case Info_OpMode.LoseHealth:
                    case Info_OpMode.GainHealth:
                    case Info_OpMode.GainTemp:
                        var tokens = ObjectParser.GetTokens(info_argumentString);
                        info_argument_valid = tokens.HasValue && ObjectParser.ValidateTokens(tokens.Value);
                        info_argument_condition = Option<Condition>.Null;
                        return;
                    case Info_OpMode.Remove:
                        if (info_argumentString.Length == 0) {
                            // Remove Creature
                            info_argument_valid = true;
                            info_argument_condition = Option<Condition>.Null;
                        }
                        else {
                            var condition = info_argumentString.GetCondition();
                            if (condition.HasValue) {
                                // Conditon
                                info_argument_valid = actor.conditions.Contains(condition.Value);
                                info_argument_condition = condition.Value;
                            }
                            else {
                                // Note (indexed by Number)
                                var index = ObjectParser.GetInt10(info_argumentString);
                                info_argument_valid = index.HasValue && actor.HasNote(index.Value);
                                info_argument_condition = Option<Condition>.Null;
                            }
                        }
                        return;
                    case Info_OpMode.Condition: {
                            var condition = info_argumentString.GetCondition();
                            info_argument_valid = condition.HasValue && !actor.conditions.Contains(condition.Value);
                            info_argument_condition = condition;
                        }
                        return;
                    case Info_OpMode.Note:
                        // Parse Integer
                        info_argument_valid = true;
                        return;
                }
            }
        }

        public void Info_Setup() {
            Info_UpdateArgumentInfo();
        }
    }

    partial class Output
    {
        private const int InfoLeftBorder = 1;
        private const int InfoLeftIndent = 1;
        private const int InfoRightBorder = 1;
        private const int InfoTopBorder = 1;
        private static void AddActorInfo(this Screen screen, int left, Actor actor) {
            int top = InfoTopBorder;
            // Add Name
            screen.AddFormattedLine("Actor '" + actor.name + "'", ConsoleColor.White, ConsoleColor.Black, left + InfoLeftBorder, int.MaxValue, top);
            ++top;
            // End with double Line
            screen.AddPartialDoubleHLine(left - 1, top);
            ++top;
            // Base Information
            string baseInfo;
            if (actor.armorClass.HasValue) {
                if (actor.speed.HasValue) {
                    baseInfo = "AC " + actor.armorClass.Value.ToString() + "   SPEED " + actor.speed.Value + "   INI " + actor.initiative.ToString();
                }
                else {
                    baseInfo = "AC " + actor.armorClass.Value.ToString() + "   INI " + actor.initiative.ToString();
                }
            }
            else {
                if (actor.speed.HasValue) {
                    baseInfo = "SPEED " + actor.speed.Value + "   INI " + actor.initiative.ToString();
                }
                else {
                    baseInfo = "INI " + actor.initiative.ToString();
                }
            }
            screen.AddFormattedLine(baseInfo, ConsoleColor.White, ConsoleColor.Black, left + InfoLeftBorder, int.MaxValue, top);
            ++top;
            string hpInfo = "HP " + actor.currentHP.ToString() + "   Max " + actor.maximumHP.ToString() + "   Temp " + actor.temporaryHP.ToString();
            screen.AddFormattedLine(hpInfo, ConsoleColor.White, ConsoleColor.Black, left + InfoLeftBorder, int.MaxValue, top);
            ++top;

            // Scores
            if (actor.scores.HasValue) {
                // Start with Line
                screen.AddPartialHLine(left - 1, top);
                ++top;

                string strString = actor.scores.Value.strength.ToString() + "(" + actor.scores.Value.strengthModifier.ToSignedString() + ")";
                string dexString = actor.scores.Value.dexterity.ToString() + "(" + actor.scores.Value.dexterityModifier.ToSignedString() + ")";
                string conString = actor.scores.Value.constitution.ToString() + "(" + actor.scores.Value.constitutionModifier.ToSignedString() + ")";
                string intString = actor.scores.Value.intelligence.ToString() + "(" + actor.scores.Value.intelligenceModifier.ToSignedString() + ")";
                string wisString = actor.scores.Value.wisdom.ToString() + "(" + actor.scores.Value.wisdomModifier.ToSignedString() + ")";
                string chaString = actor.scores.Value.charisma.ToString() + "(" + actor.scores.Value.charismaModifier.ToSignedString() + ")";

                int y = left + InfoLeftBorder;
                screen.AddFormattedLine("STR", ConsoleColor.White, ConsoleColor.Black, y, int.MaxValue, top);
                screen.AddFormattedLine(strString, ConsoleColor.White, ConsoleColor.Black, y, int.MaxValue, top + 1);
                y += strString.Length + 1;
                screen.AddFormattedLine("DEX", ConsoleColor.White, ConsoleColor.Black, y, int.MaxValue, top);
                screen.AddFormattedLine(dexString, ConsoleColor.White, ConsoleColor.Black, y, int.MaxValue, top + 1);
                y += dexString.Length + 1;
                screen.AddFormattedLine("CON", ConsoleColor.White, ConsoleColor.Black, y, int.MaxValue, top);
                screen.AddFormattedLine(conString, ConsoleColor.White, ConsoleColor.Black, y, int.MaxValue, top + 1);
                y += conString.Length + 1;
                screen.AddFormattedLine("INT", ConsoleColor.White, ConsoleColor.Black, y, int.MaxValue, top);
                screen.AddFormattedLine(intString, ConsoleColor.White, ConsoleColor.Black, y, int.MaxValue, top + 1);
                y += intString.Length + 1;
                screen.AddFormattedLine("WIS", ConsoleColor.White, ConsoleColor.Black, y, int.MaxValue, top);
                screen.AddFormattedLine(wisString, ConsoleColor.White, ConsoleColor.Black, y, int.MaxValue, top + 1);
                y += wisString.Length + 1;
                screen.AddFormattedLine("CHA", ConsoleColor.White, ConsoleColor.Black, y, int.MaxValue, top);
                screen.AddFormattedLine(chaString, ConsoleColor.White, ConsoleColor.Black, y, int.MaxValue, top + 1);
                y += chaString.Length + 1;
                top += 2;
            }

            // Attacks
            if (actor.attacks.Count != 0) {
                // Start with Line
                screen.AddPartialHLine(left - 1, top);
                ++top;

                for (int index = 0; index < actor.attacks.Count; ++index) {
                    string attackString = actor.attacks[index].name;
                    if (Program.outputData.info_attackValues.Count != 0) {
                        // Add Attack Values
                        attackString = "ATK " + Program.outputData.info_attackValues[index].Item1 + "   DMG " + Program.outputData.info_attackValues[index].Item2 + "   " + attackString;
                    }
                    screen.AddFormattedLine(attackString, ConsoleColor.White, ConsoleColor.Black, left + InfoLeftBorder, int.MaxValue, top);
                    ++top;
                }
            }

            // End with Double Line
            screen.AddPartialDoubleHLine(left - 1, top);
            ++top;

            // Conditions, Notes, Description
            bool topLineExists = true;
            if(actor.conditions.Count > 0) {
                if (Program.outputData.info_details) {
                    // Format conditon
                    //         text text text text
                    for(int index = 0; index < actor.conditions.Count; ++index) {
                        string conditionText = actor.conditions[index].GetName();
                        screen.AddFormattedLine(conditionText,
                            ConsoleColor.Yellow, ConsoleColor.Black, left + InfoLeftBorder, int.MaxValue, top);
                        ++top;
                        List<string> descriptionText = actor.conditions[index].GetDescription();
                        top += screen.AddFormattedLines(descriptionText,
                            ConsoleColor.White, ConsoleColor.Black, left + InfoLeftBorder + InfoLeftIndent, screen.Width - InfoRightBorder, top);
                    }
                }
                else {
                    StringBuilder conditions = new StringBuilder(actor.conditions[0].GetName());
                    for(int index = 1; index < actor.conditions.Count; ++index) {
                        conditions.Append(", ");
                        conditions.Append(actor.conditions[index].GetName());
                    }
                    top += screen.AddFormattedLines(new List<string> { conditions.ToString() },
                        ConsoleColor.White, ConsoleColor.Black, left + InfoLeftBorder, screen.Width - InfoRightBorder, top);
                }
                topLineExists = false;
            }
            if(actor.notes.Count > 0) {
                if (!topLineExists) {
                    screen.AddPartialHLine(left - 1, top);
                    ++top;
                }
                for(int pos = 0; pos < actor.notes.Count; ++pos) {
                    // Write Note, formatted like: 1) text
                    //                              text
                    string indexText = actor.notes[pos].index.ToString() + ")";
                    string noteText = new string(' ', indexText.Length + 1 - InfoLeftIndent) + actor.notes[pos].value;
                    int textWidth = screen.AddFormattedLines(new List<string> { noteText },
                        ConsoleColor.White, ConsoleColor.Black, left + InfoLeftBorder + InfoLeftIndent, screen.Width - InfoRightBorder, top);
                    screen.AddFormattedLine(indexText, ConsoleColor.White, ConsoleColor.Black, left + InfoLeftBorder, int.MaxValue, top);
                    top += textWidth;
                }
                topLineExists = false;
            }
            // Dont forget them lines!
            if (actor.description.HasValue) {
                if (!topLineExists) {
                    screen.AddPartialHLine(left - 1, top);
                    ++top;
                }
                screen.AddFormattedLines(actor.description.Value, ConsoleColor.White, ConsoleColor.Black, left + InfoLeftBorder, screen.Width, top);

                topLineExists = false;
            }
        }
        
        
        private static void AddOperationInfo(this Screen screen) {
            int left = 0;
            string opString = "OP=" + Program.outputData.info_opMode.GetName() + ",ARG=";
            screen.AddFormattedLine(opString, ConsoleColor.White, ConsoleColor.Black, left, int.MaxValue, screen.Height - 1);
            left += opString.Length;
            string warnString = " !";
            int continuation;
            if (Program.outputData.info_argument_condition.HasValue) {
                continuation = Program.outputData.info_argument_condition.Value.GetName().Length - Program.outputData.Info_GetArgument().Length;
            }
            else {
                continuation = 0;
            }
            int argWidth = screen.Width - 1 - opString.Length - continuation;
            if (!Program.outputData.info_argument_valid) {
                argWidth -= warnString.Length;
            }
            argWidth = Math.Max(argWidth, 0);
            // Get & write ArgString
            string argString;
            if (Program.outputData.Info_GetArgument().Length <= argWidth) {
                argString = Program.outputData.Info_GetArgument();
            }
            else {
                argString = Program.outputData.Info_GetArgument().Substring(Program.outputData.Info_GetArgument().Length - argWidth);
            }
            screen.AddFormattedLine(argString, ConsoleColor.White, ConsoleColor.Black, left, int.MaxValue, screen.Height - 1);
            left += argString.Length;
            // Add Continuation
            if (Program.outputData.info_argument_condition.HasValue) {
                string continuationString = Program.outputData.info_argument_condition.Value.GetName().Substring(Program.outputData.Info_GetArgument().Length);
                screen.AddFormattedLine(continuationString, ConsoleColor.DarkGray, ConsoleColor.Black, left, int.MaxValue, screen.Height - 1);
                left += continuationString.Length;
            }
            // Add Warning
            if (!Program.outputData.info_argument_valid) {
                screen.AddFormattedLine(warnString, ConsoleColor.Red, ConsoleColor.Black, left, int.MaxValue, screen.Height - 1);
            }
            
        }

        private static Screen Info_GetScreen() {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            Screen screen = Screen.Default(width, height);
            int separatorPos = GetSeparatorPos(width);

            screen.AddDoubleVLine(separatorPos);
            screen.AddInitiativeList(separatorPos);
            screen.AddOperationInfo();

            if (Program.data.idList.Count != 0) {
                screen.AddActorInfo(separatorPos + 1, Program.data.GetActor(Program.data.idList[Program.outputData.Info_GetSelected()]));
            }
            return screen;
        }
    }

    partial class InputParser
    {
        private const ConsoleKey RollAttack = ConsoleKey.A;
        private const ConsoleKey MoveActive = ConsoleKey.Tab;
        private const ConsoleKey MoveSelectUp = ConsoleKey.UpArrow;
        private const ConsoleKey MoveSelectDown = ConsoleKey.DownArrow;
        private const ConsoleKey ToggleSelect = ConsoleKey.S;
        private const ConsoleKey ToggleDetails = ConsoleKey.I;

        private const ConsoleKey OpDamage = ConsoleKey.D;
        private const ConsoleKey OpHeal = ConsoleKey.H;
        private const ConsoleKey OpTemp = ConsoleKey.T;
        private const ConsoleKey OpRemove = ConsoleKey.R;
        private const ConsoleKey OpCondition = ConsoleKey.C;
        private const ConsoleKey OpNote = ConsoleKey.N;

        private const ConsoleKey GainHealth = ConsoleKey.U;
        private const ConsoleKey LoseHealth = ConsoleKey.L;
        private const int DefAmount = 1;
        private const int ShiftAmount = 5;
        private const int CtrlAmount = 10;
        private const int AltAmount = 20;

        private static void Parse_Info(ConsoleKeyInfo keyInfo) {
            // Check Control Combinations
            if((keyInfo.Modifiers & ConsoleModifiers.Control) != 0) {
                // Check Control Keys
                switch (keyInfo.Key) {
                    case Undo:
                    // Undo
                        if (Program.data.changes.Count != 0) {
                            Program.data.changes.Pop().Undo();
                        }
                        return;
                    case CycleMode:
                        // Change Mode
                        Program.outputData.mode = Mode.Select;
                        return;
                    case RollAttack:
                        // Roll Attacks
                        Program.outputData.Info_RollAttack();
                        return;
                    case ToggleDetails:
                        Program.outputData.info_details = !Program.outputData.info_details;
                        return;
                    // Change Op Mode
                    case OpDamage:
                        Program.outputData.info_opMode = Info_OpMode.LoseHealth;
                        Program.outputData.Info_Argument_Clear();
                        return;
                    case OpHeal:
                        Program.outputData.info_opMode = Info_OpMode.GainHealth;
                        Program.outputData.Info_Argument_Clear();
                        return;
                    case OpTemp:
                        Program.outputData.info_opMode = Info_OpMode.GainTemp;
                        Program.outputData.Info_Argument_Clear();
                        return;
                    case OpCondition:
                        Program.outputData.info_opMode = Info_OpMode.Condition;
                        Program.outputData.Info_Argument_Clear();
                        return;
                    case OpNote:
                        Program.outputData.info_opMode = Info_OpMode.Note;
                        Program.outputData.Info_Argument_Clear();
                        return;
                    case OpRemove:
                        Program.outputData.info_opMode = Info_OpMode.Remove;
                        Program.outputData.Info_Argument_Clear();
                        return;
                    case ToggleSelect: 
                        {
                            int selectedID = Program.data.idList.Count == 0 ? -1 : Program.data.idList[Program.outputData.Info_GetSelected()];
                            Program.outputData.Info_ChangetActiveIsSelected(selectedID);
                        }
                        return;
                }
            }

            // Check other
            switch (keyInfo.Key) {
                case MoveActive:
                    // Move Active
                    if (Program.data.idList.Count > 1) {
                        int selectedID = Program.data.idList.Count == 0 ? -1 : Program.data.idList[Program.outputData.Info_GetSelected()];
                        if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0) {
                            // Move Back
                            Program.outputData.Info_SetActive(Program.outputData.Info_GetActive() == 0 ? Program.data.idList.Count - 1 : Program.outputData.Info_GetActive() - 1, selectedID);
                        }
                        else {
                            // Move Forward
                            Program.outputData.Info_SetActive(Program.outputData.Info_GetActive() == Program.data.idList.Count - 1 ? 0 : Program.outputData.Info_GetActive() + 1, selectedID);
                        }
                    }
                    return;
                case MoveSelectUp: 
                    {
                        int selectedID = Program.data.idList.Count == 0 ? -1 : Program.data.idList[Program.outputData.Info_GetSelected()];
                        if(Program.data.idList.Count > 1) {
                            Program.outputData.Info_SetSelected(Program.outputData.Info_GetSelected() == 0 ? Program.data.idList.Count - 1 : Program.outputData.Info_GetSelected() - 1, selectedID);
                        }
                    }
                    return;
                case MoveSelectDown: 
                    {
                        int selectedID = Program.data.idList.Count == 0 ? -1 : Program.data.idList[Program.outputData.Info_GetSelected()];
                        if (Program.data.idList.Count > 1) {
                            Program.outputData.Info_SetSelected(Program.outputData.Info_GetSelected() == Program.data.idList.Count - 1 ? 0 : Program.outputData.Info_GetSelected() + 1, selectedID);
                        }
                    }
                    return;
                    // Modify Argument
                case Remove:
                    Program.outputData.Info_Argument_Remove();
                    return;
                case Clear:
                    Program.outputData.Info_Argument_Clear();
                    return;
            }
            // Parse Mode-Dependant
            switch (Program.outputData.info_opMode) {
                case Info_OpMode.LoseHealth:
                case Info_OpMode.GainHealth:
                case Info_OpMode.GainTemp:
                    Parse_Info_HealthChange(keyInfo);
                    return;
                case Info_OpMode.Condition:
                case Info_OpMode.Note:
                case Info_OpMode.Remove:
                    Parse_Info_Text(keyInfo);
                    return;
            }
        }

        /// <summary>
        /// For LoseHealth, GainHealth, GainTemp
        /// </summary>
        private static void Parse_Info_HealthChange(ConsoleKeyInfo keyInfo) {
            switch (keyInfo.Key) {
                case Confirm:
                    // Evaluate Tokens
                    var tokens = ObjectParser.GetTokens(Program.outputData.Info_GetArgument());
                    if (tokens.HasValue) {
                        var result = ObjectParser.EvaluateTokens(tokens.Value);
                        if (result.HasValue) {
                            switch (Program.outputData.info_opMode) {
                                case Info_OpMode.LoseHealth:
                                    Program.data.LoseHealth(Program.data.idList[Program.outputData.Info_GetSelected()], result.Value);
                                    return;
                                case Info_OpMode.GainHealth:
                                    Program.data.LoseHealth(Program.data.idList[Program.outputData.Info_GetSelected()], result.Value);
                                    return;
                                case Info_OpMode.GainTemp:
                                    Program.data.GainHealth(Program.data.idList[Program.outputData.Info_GetSelected()], result.Value);
                                    return;
                                default:
                                    throw new ArgumentOutOfRangeException(nameof(Program.outputData.info_opMode));
                            }
                        }
                    }
                    break;
                case GainHealth:
                    // Gain Health
                    if ((keyInfo.Modifiers & ConsoleModifiers.Alt) != 0) {
                        Program.data.GainHealth(Program.data.idList[Program.outputData.Info_GetSelected()], AltAmount);
                    }
                    else if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0) {
                        Program.data.GainHealth(Program.data.idList[Program.outputData.Info_GetSelected()], CtrlAmount);
                    }
                    else if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0) {
                        Program.data.GainHealth(Program.data.idList[Program.outputData.Info_GetSelected()], ShiftAmount);
                    }
                    else {
                        Program.data.GainHealth(Program.data.idList[Program.outputData.Info_GetSelected()], DefAmount);
                    }
                    break;
                case LoseHealth:
                    // Lose Health
                    if ((keyInfo.Modifiers & ConsoleModifiers.Alt) != 0) {
                        Program.data.LoseHealth(Program.data.idList[Program.outputData.Info_GetSelected()], AltAmount);
                    }
                    else if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0) {
                        Program.data.LoseHealth(Program.data.idList[Program.outputData.Info_GetSelected()], CtrlAmount);
                    }
                    else if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0) {
                        Program.data.LoseHealth(Program.data.idList[Program.outputData.Info_GetSelected()], ShiftAmount);
                    }
                    else {
                        Program.data.LoseHealth(Program.data.idList[Program.outputData.Info_GetSelected()], DefAmount);
                    }
                    break;
            }
            // Add Character to Argument
            ObjectParser.FormatExpressionChar(keyInfo.KeyChar, Program.outputData.Info_Argument_Add);
        }
        /// <summary>
        /// For Condition, Note, Remove
        /// </summary>
        private static void Parse_Info_Text(ConsoleKeyInfo keyInfo) {
            // Look for Confirm Key
            if(Program.data.idList.Count == 0) {
                return;
            }

            switch (keyInfo.Key) {
                case Confirm:
                    var actor = Program.data.GetActor(Program.data.idList[Program.outputData.Info_GetSelected()]);
                    switch (Program.outputData.info_opMode) {
                        case Info_OpMode.Condition:
                            if(actor != null) {
                                // Add Condition
                                var condition = Program.outputData.Info_GetArgument().GetCondition();
                                if (condition.HasValue) {
                                    if (actor.AddCondition(condition.Value)) {
                                        Program.outputData.Info_Argument_Clear();
                                    }
                                }
                            }
                            return;
                        case Info_OpMode.Note:
                            if(actor != null) {
                                // Add Note
                                actor.AddNote(Program.outputData.Info_GetArgument());
                            }
                            return;
                        case Info_OpMode.Remove:
                            if(actor != null) {
                                if(Program.outputData.Info_GetArgument().Length == 0) {
                                    // Remove Actor
                                    Program.data.RemoveActor(actor.id);
                                    Program.outputData.Info_UpdateArgumentInfo();
                                }
                                else {
                                    var condition = Program.outputData.Info_GetArgument().GetCondition();
                                    if (condition.HasValue) {
                                        // Remove Condition
                                        actor.RemoveCondition(condition.Value);
                                        Program.outputData.Info_UpdateArgumentInfo();
                                    }
                                    else {
                                        // Remove Note
                                        var index = ObjectParser.GetInt10(Program.outputData.Info_GetArgument());
                                        if (index.HasValue) {
                                            actor.RemoveNote(index.Value);
                                            Program.outputData.Info_UpdateArgumentInfo();
                                        }
                                    }
                                }
                            }
                            return;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(Program.outputData.info_opMode));
                    }
            }
            // Parse Char as Input
            ObjectParser.FormatChar(keyInfo.KeyChar, Program.outputData.Info_Argument_Add);
        }
    }
}