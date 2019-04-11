using System;
using System.Collections.Generic;

namespace InitiativeTracker
{
    public enum CreateNode
    {
        Name,
        INI, Speed, AC, HP, Temp,
        ColoringType, Remove, Coloring_BaseText, Coloring_BaseBG, Coloring_ActiveText, Coloring_ActiveBG, 
        Score_Str, Score_Dex, Score_Con, Score_Int, Score_Wis, Score_Cha,
        Attack_Name, Attack_Atk, Attack_Mod, Attack_Dmg,
        Condition, 
        Note,
        Description,
        Create
    }
    public static class CreateNodeExt
    {
        public static CreateNode MoveDown(this CreateNode node) {
            switch (node) {
                case CreateNode.Name:
                    return CreateNode.INI;
                case CreateNode.INI:
                    return CreateNode.Speed;
                case CreateNode.Speed:
                    return CreateNode.AC;
                case CreateNode.AC:
                    return CreateNode.HP;
                case CreateNode.HP:
                case CreateNode.Temp:
                    return CreateNode.ColoringType;
                case CreateNode.ColoringType:
                case CreateNode.Remove:
                    return CreateNode.Coloring_BaseText;
                case CreateNode.Coloring_BaseText:
                case CreateNode.Coloring_BaseBG:
                case CreateNode.Coloring_ActiveText:
                case CreateNode.Coloring_ActiveBG:
                    return CreateNode.Score_Str;
                case CreateNode.Score_Str:
                case CreateNode.Score_Dex:
                case CreateNode.Score_Con:
                case CreateNode.Score_Int:
                case CreateNode.Score_Wis:
                case CreateNode.Score_Cha:
                    return CreateNode.Attack_Name;
                case CreateNode.Attack_Name:
                    return CreateNode.Attack_Atk;
                case CreateNode.Attack_Atk:
                case CreateNode.Attack_Mod:
                case CreateNode.Attack_Dmg:
                    return CreateNode.Condition;
                case CreateNode.Condition:
                    return CreateNode.Note;
                case CreateNode.Note:
                    return CreateNode.Description;
                case CreateNode.Description:
                case CreateNode.Create:
                    return CreateNode.Create;
                default:
                    return default;
            }
        }
        public static CreateNode MoveUp(this CreateNode node) {
            switch (node) {
                case CreateNode.Name:
                case CreateNode.INI:
                    return CreateNode.Name;
                case CreateNode.Speed:
                    return CreateNode.INI;
                case CreateNode.AC:
                    return CreateNode.Speed;
                case CreateNode.HP:
                case CreateNode.Temp:
                    return CreateNode.AC;
                case CreateNode.ColoringType:
                case CreateNode.Remove:
                    return CreateNode.HP;
                case CreateNode.Coloring_BaseText:
                case CreateNode.Coloring_BaseBG:
                case CreateNode.Coloring_ActiveText:
                case CreateNode.Coloring_ActiveBG:
                    return CreateNode.ColoringType;
                case CreateNode.Score_Str:
                case CreateNode.Score_Dex:
                case CreateNode.Score_Con:
                case CreateNode.Score_Int:
                case CreateNode.Score_Wis:
                case CreateNode.Score_Cha:
                    return CreateNode.Coloring_BaseText;
                case CreateNode.Attack_Name:
                    return CreateNode.Score_Str;
                case CreateNode.Attack_Atk:
                case CreateNode.Attack_Mod:
                case CreateNode.Attack_Dmg:
                    return CreateNode.Attack_Name;
                case CreateNode.Condition:
                    return CreateNode.Attack_Atk;
                case CreateNode.Note:
                    return CreateNode.Condition;
                case CreateNode.Description:
                    return CreateNode.Note;
                case CreateNode.Create:
                    return CreateNode.Description;
                default:
                    return default;
            }
        }
        public static CreateNode MoveLeft(this CreateNode node) {
            switch (node) {
                case CreateNode.Name:
                    return CreateNode.Name;
                case CreateNode.INI:
                    return CreateNode.INI;
                case CreateNode.Speed:
                    return CreateNode.Speed;
                case CreateNode.AC:
                    return CreateNode.AC;
                case CreateNode.HP:
                case CreateNode.Temp:
                    return CreateNode.HP;
                case CreateNode.ColoringType:
                case CreateNode.Remove:
                    return CreateNode.ColoringType;
                case CreateNode.Coloring_BaseText:
                case CreateNode.Coloring_BaseBG:
                    return CreateNode.Coloring_BaseText;
                case CreateNode.Coloring_ActiveText:
                    return CreateNode.Coloring_BaseBG;
                case CreateNode.Coloring_ActiveBG:
                    return CreateNode.Coloring_ActiveText;
                case CreateNode.Score_Str:
                case CreateNode.Score_Dex:
                    return CreateNode.Score_Str;
                case CreateNode.Score_Con:
                    return CreateNode.Score_Dex;
                case CreateNode.Score_Int:
                    return CreateNode.Score_Con;
                case CreateNode.Score_Wis:
                    return CreateNode.Score_Int;
                case CreateNode.Score_Cha:
                    return CreateNode.Score_Wis;
                case CreateNode.Attack_Name:
                    return CreateNode.Attack_Name;
                case CreateNode.Attack_Atk:
                case CreateNode.Attack_Mod:
                    return CreateNode.Attack_Atk;
                case CreateNode.Attack_Dmg:
                    return CreateNode.Attack_Mod;
                case CreateNode.Condition:
                    return CreateNode.Condition;
                case CreateNode.Note:
                    return CreateNode.Note;
                case CreateNode.Description:
                    return CreateNode.Description;
                case CreateNode.Create:
                    return CreateNode.Create;
                default:
                    return default;
            }
        }
        public static CreateNode MoveRight(this CreateNode node) {
            switch (node) {
                case CreateNode.Name:
                    return CreateNode.Name;
                case CreateNode.INI:
                    return CreateNode.INI;
                case CreateNode.Speed:
                    return CreateNode.Speed;
                case CreateNode.AC:
                    return CreateNode.AC;
                case CreateNode.HP:
                case CreateNode.Temp:
                    return CreateNode.Temp;
                case CreateNode.ColoringType:
                case CreateNode.Remove:
                    return CreateNode.Remove;
                case CreateNode.Coloring_BaseText:
                    return CreateNode.Coloring_BaseBG;
                case CreateNode.Coloring_BaseBG:
                    return CreateNode.Coloring_ActiveText;
                case CreateNode.Coloring_ActiveText:
                case CreateNode.Coloring_ActiveBG:
                    return CreateNode.Coloring_ActiveBG;
                case CreateNode.Score_Str:
                    return CreateNode.Score_Dex;
                case CreateNode.Score_Dex:
                    return CreateNode.Score_Con;
                case CreateNode.Score_Con:
                    return CreateNode.Score_Int;
                case CreateNode.Score_Int:
                    return CreateNode.Score_Wis;
                case CreateNode.Score_Wis:
                case CreateNode.Score_Cha:
                    return CreateNode.Score_Cha;
                case CreateNode.Attack_Name:
                    return CreateNode.Attack_Name;
                case CreateNode.Attack_Atk:
                    return CreateNode.Attack_Mod;
                case CreateNode.Attack_Mod:
                case CreateNode.Attack_Dmg:
                    return CreateNode.Attack_Dmg;
                case CreateNode.Condition:
                    return CreateNode.Condition;
                case CreateNode.Note:
                    return CreateNode.Note;
                case CreateNode.Description:
                    return CreateNode.Description;
                case CreateNode.Create:
                    return CreateNode.Create;
                default:
                    return default;
            }
        }
    }

    public enum ControlInvalid
    {
        Default, ForceValid, ForceInvalid
    }
    public abstract class InputField
    {
        public abstract void Add(char c);
        public abstract void Remove();
        public abstract void Clear();
        
        public abstract int AddPrinting(Screen screen, int left, int top, ControlInvalid control);
    }
    public class StringField : InputField
    {
        private List<char> value = new List<char>();
        private string valueString = string.Empty;

        public override void Add(char c) {
            ObjectParser.FormatChar(c, ref value);
            Update();
        }
        public override void Remove() {
            if(value.Count != 0) {
                value.RemoveAt(value.Count - 1);
                Update();
            }
        }
        public override void Clear() {
            if (value.Count != 0) {
                value.Clear();
                Update();
            }
        }
        private void Update() {
            valueString = string.Concat(value);
        }

        public bool IsEmpty() {
            return value.Count == 0;
        }
        public string GetValue() {
            return valueString;
        }

        // TODO: With scrolling text for really long input
        public override int AddPrinting(Screen screen, int left, int top, ControlInvalid control) {
            screen.AddFormattedLine(valueString, ConsoleColor.White, ConsoleColor.Black, left, int.MaxValue, top);
            int width = valueString.Length;
            if (control == ControlInvalid.ForceInvalid) {
                string warnString = " !";
                screen.AddFormattedLine(warnString, ConsoleColor.Red, ConsoleColor.Black, left + width, int.MaxValue, top);
                width += warnString.Length;
            }
            return width;
        }
    }
    
    public class ExpressionField : InputField
    {
        private List<char> expr = new List<char>();
        private string exprString = string.Empty;
        private List<Token> exprTokens = null;
        private bool exprValid = false;

        public override void Add(char c) {
            ObjectParser.FormatExpressionChar(c, ref expr);
        }
        public override void Remove() {
            if (expr.Count != 0) {
                expr.RemoveAt(expr.Count - 1);
                Update();
            }
        }
        public override void Clear() {
            if (expr.Count != 0) {
                expr.Clear();
                Update();
            }
        }
        private void Update() {
            exprString = string.Concat(expr);
            var tokens = ObjectParser.GetTokens(exprString);
            if (tokens.HasValue) {
                exprTokens = tokens.Value;
                exprValid = ObjectParser.ValidateTokens(tokens.Value);
            }
            else {
                exprTokens = null;
                exprValid = false;
            }
        }

        public bool IsEmpty() {
            return expr.Count == 0;
        }
        public bool HasValue() {
            return exprValid;
        }
        public int Evaluate() {
            return ObjectParser.EvaluateTokens(exprTokens).Value;
        }

        public override int AddPrinting(Screen screen, int left, int top, ControlInvalid control) {
            screen.AddFormattedLine(exprString, ConsoleColor.White, ConsoleColor.Black, left, int.MaxValue, top);
            int width = exprString.Length;
            if (control == ControlInvalid.ForceInvalid || (!exprValid && control == ControlInvalid.Default) ) {
                string warnString = " !";
                screen.AddFormattedLine(warnString, ConsoleColor.Red, ConsoleColor.Black, left + width, int.MaxValue, top);
                width += warnString.Length;
            }
            return width;
        }
    }

    public class BooleanField : InputField
    {
        private List<char> expr = new List<char>();
        private string exprString = string.Empty;
        private Option<bool> exprValue;
        private bool defaultValue;

        public void SetDefault(bool defaultValue) {
            this.defaultValue = defaultValue;
        }

        public override void Add(char c) {
            ObjectParser.FormatChar(c, ref expr);
            Update();
        }
        public override void Remove() {
            if (expr.Count != 0) {
                expr.RemoveAt(expr.Count - 1);
                Update();
            }
        }
        public override void Clear() {
            if (expr.Count != 0) {
                expr.Clear();
                Update();
            }
        }
        private void Update() {
            exprString = string.Concat(expr);
            exprValue = ObjectParser.GetBoolean(exprString);
        }


        public bool IsEmpty() {
            return expr.Count == 0;
        }
        public bool HasValue() {
            return exprValue.HasValue;
        }
        public bool GetValue() {
            return expr.Count == 0 ? defaultValue : exprValue.Value;
        }
        
        public override int AddPrinting(Screen screen, int left, int top, ControlInvalid control) {
            int width;
            if (exprString.Length == 0) {
                screen.AddFormattedLine(defaultValue.ToString(), ConsoleColor.White, ConsoleColor.Black, left, int.MaxValue, top);
                width = exprString.Length;
            }
            else {
                screen.AddFormattedLine(exprString, ConsoleColor.White, ConsoleColor.Black, left, int.MaxValue, top);
                if (exprValue.HasValue) {
                    // Add Continuation
                    string continuationString = exprValue.Value.ToString().Substring(exprString.Length);
                    screen.AddFormattedLine(continuationString, ConsoleColor.DarkGray, ConsoleColor.Black, left + exprString.Length, int.MaxValue, top);
                    width = exprValue.Value.ToString().Length;
                }
                else {
                    width = exprString.Length;
                }
            }
            // Add Warning
            if ((!exprValue.HasValue && control == ControlInvalid.Default) || control == ControlInvalid.ForceInvalid) {
                string warnString = " !";
                screen.AddFormattedLine(warnString, ConsoleColor.Red, ConsoleColor.Black, left + width, int.MaxValue, top);
                width += warnString.Length;
            }
            return width;
        }
    }

    public class ConsoleColorField : InputField
    {
        private List<char> expr = new List<char>();
        private string exprString = string.Empty;
        private Option<ConsoleColor> exprValue;

        public override void Add(char c) {
            ObjectParser.FormatChar(c, ref expr);
            Update();
        }
        public override void Remove() {
            if (expr.Count != 0) {
                expr.RemoveAt(expr.Count - 1);
                Update();
            }
        }
        public override void Clear() {
            if (expr.Count != 0) {
                expr.Clear();
                Update();
            }
        }
        private void Update() {
            exprString = string.Concat(expr);
            exprValue = exprString.GetConsoleColor();
        }

        public bool IsEmpty() {
            return expr.Count == 0;
        }
        public bool HasValue() {
            return exprValue.HasValue;
        }
        public ConsoleColor GetValue() {
            return exprValue.Value;
        }

        public override int AddPrinting(Screen screen, int left, int top, ControlInvalid control) {
            int width;
            
            screen.AddFormattedLine(exprString, ConsoleColor.White, ConsoleColor.Black, left, int.MaxValue, top);
            if (exprValue.HasValue) {
                // Add Continuation
                string continuationString = exprValue.Value.GetName().Substring(exprString.Length);
                screen.AddFormattedLine(continuationString, ConsoleColor.DarkGray, ConsoleColor.Black, left + exprString.Length, int.MaxValue, top);
                width = exprValue.Value.ToString().Length;
            }
            else {
                width = exprString.Length;
            }
            // Add Warning
            if ((!exprValue.HasValue && control == ControlInvalid.Default) || control == ControlInvalid.ForceInvalid) {
                string warnString = " !";
                screen.AddFormattedLine(warnString, ConsoleColor.Red, ConsoleColor.Black, left + width, int.MaxValue, top);
                width += warnString.Length;
            }
            return width;
        }
    }

    public class ConditionField : InputField
    {
        private List<char> expr = new List<char>();
        private string exprString = string.Empty;
        private Option<Condition> exprValue;

        public override void Add(char c) {
            ObjectParser.FormatChar(c, ref expr);
            Update();
        }
        public override void Remove() {
            if (expr.Count != 0) {
                expr.RemoveAt(expr.Count - 1);
                Update();
            }
        }
        public override void Clear() {
            if (expr.Count != 0) {
                expr.Clear();
                Update();
            }
        }
        private void Update() {
            exprString = string.Concat(expr);
            exprValue = exprString.GetCondition();
        }
        
        public bool HasValue() {
            return exprValue.HasValue;
        }
        public Condition GetValue() {
            return exprValue.Value;
        }

        public override int AddPrinting(Screen screen, int left, int top, ControlInvalid control) {
            int width;
            
            screen.AddFormattedLine(exprString, ConsoleColor.White, ConsoleColor.Black, left, int.MaxValue, top);
            if (exprValue.HasValue) {
                // Add Continuation
                string continuationString = exprValue.Value.GetName().Substring(exprString.Length);
                screen.AddFormattedLine(continuationString, ConsoleColor.DarkGray, ConsoleColor.Black, left + exprString.Length, int.MaxValue, top);
                width = exprValue.Value.ToString().Length;
            }
            else {
                width = exprString.Length;
            }
            // Add Warning
            if ((!exprValue.HasValue && control == ControlInvalid.Default) || control == ControlInvalid.ForceInvalid) {
                string warnString = " !";
                screen.AddFormattedLine(warnString, ConsoleColor.Red, ConsoleColor.Black, left + width, int.MaxValue, top);
                width += warnString.Length;
            }
            return width;
        }
    }



    partial class OutputData
    {
        public CreateNode create_currentNode = CreateNode.Name;

        public StringField create_name = new StringField();
        public ExpressionField create_ini = new ExpressionField();
        public StringField create_speed = new StringField();
        public ExpressionField create_ac = new ExpressionField();
        public ExpressionField create_hp = new ExpressionField();
        public ExpressionField create_temp = new ExpressionField();

        public StringField create_coloring = new StringField();
        public BooleanField create_remove = new BooleanField();
        public ConsoleColorField create_base_text = new ConsoleColorField();
        public ConsoleColorField create_base_bg = new ConsoleColorField();
        public ConsoleColorField create_active_text = new ConsoleColorField();
        public ConsoleColorField create_active_bg = new ConsoleColorField();

        public ExpressionField create_score_str = new ExpressionField();
        public ExpressionField create_score_dex = new ExpressionField();
        public ExpressionField create_score_con = new ExpressionField();
        public ExpressionField create_score_int = new ExpressionField();
        public ExpressionField create_score_wis = new ExpressionField();
        public ExpressionField create_score_cha = new ExpressionField();

        public List<Attack> create_attacks = new List<Attack>();
        public StringField create_attack_name = new StringField();
        public ExpressionField create_attack_atk = new ExpressionField();
        public ExpressionField create_attack_mod = new ExpressionField();
        public ExpressionField create_attack_dmg = new ExpressionField();

        public List<Condition> create_conditions = new List<Condition>();
        public ConditionField create_condition = new ConditionField();
        public StringField create_note = new StringField();
        public List<string> descriptionLines = new List<string>();
        public StringField create_description = new StringField();

        public bool create_check_coloring = false;
        public bool create_check_scores = false;
        public bool create_check_attack = false;
        public bool create_check_all = false;

        public void Create_UpdateChecks() {
            // Update Coloring Check
            bool allColors = create_base_text.HasValue() && create_base_bg.HasValue() && create_active_text.HasValue() && create_active_bg.HasValue();
            bool noColors = create_base_text.IsEmpty() && create_base_bg.IsEmpty() && create_active_text.IsEmpty() && create_active_bg.IsEmpty();

            if (!allColors && !noColors) {
                create_check_coloring = false;
            }
            else if(allColors) {
                create_check_coloring = create_coloring.GetValue().Length == 0;
            }
            else {
                if(create_coloring.IsEmpty()) {
                    create_check_coloring = Program.data.HasColoringType(Program.settings.defaultColoringType);
                }
                else {
                    create_check_coloring = Program.data.HasColoringType(create_coloring.GetValue());
                }
            }

            // Update Scores Check
            bool allScores = create_score_str.HasValue() && create_score_dex.HasValue() && create_score_con.HasValue()
                && create_score_int.HasValue() && create_score_wis.HasValue() && create_score_cha.HasValue();
            bool noScores = create_score_str.IsEmpty() && create_score_dex.IsEmpty() && create_score_con.IsEmpty()
                && create_score_int.IsEmpty() && create_score_wis.IsEmpty() && create_score_cha.IsEmpty();

            create_check_scores = allScores || noScores;

            // Update Attack Check
            bool attack_valid = (create_attack_atk.HasValue() || create_attack_atk.IsEmpty()) && (create_attack_mod.HasValue() || create_attack_mod.IsEmpty());
            create_check_attack = attack_valid && (create_attack_atk.HasValue() ^ create_attack_mod.HasValue());

            // Update Complete Check
            create_check_all = create_ini.HasValue() && (create_ac.HasValue() || create_ac.IsEmpty())
                && create_hp.HasValue() && (create_temp.HasValue() || create_temp.IsEmpty())
                && create_check_coloring && (create_remove.HasValue() || create_remove.IsEmpty())
                && create_check_scores;
        }

        public void Create_Setup() {
            create_remove.SetDefault(Program.settings.defaultRemove);
            Create_UpdateChecks();
        }

        // TODO
        public void Create_AddAttack() {
        }
        public void Create_RemoveAttack() {

        }
        public void Create_AddCondition() {

        }
        public void Create_RemoveCondition() {

        }
        public void Create_AddNote() {

        }
        public void Create_RemoveNote() {

        }
        public void Create_AddDescriptionLine() {

        }
        public void Create_RemoveDescriptionLine() {

        }
    }

    partial class Output
    {
        private static Screen GetScreen_Create() {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            return Screen.MonoColor(width, height, ConsoleColor.Yellow);
        }
    }

    partial class InputParser
    {
        public const ConsoleKey MoveNodeUp = ConsoleKey.UpArrow;
        public const ConsoleKey MoveNodeDown = ConsoleKey.DownArrow;
        public const ConsoleKey MoveNodeLeft = ConsoleKey.LeftArrow;
        public const ConsoleKey MoveNodeRight = ConsoleKey.RightArrow;
        public const ConsoleKey RevertAdd = ConsoleKey.Escape;
        
        private static void Parse_Create(ConsoleKeyInfo keyInfo) {
            // Check Control Combinations
            if((keyInfo.Modifiers & ConsoleModifiers.Control) != 0) {
                switch (keyInfo.Key) {
                    case CycleMode:
                        // Cycle Mode
                        Program.outputData.mode = Mode.Info;
                        return;
                }
            }
            // Check other Keys
            switch (keyInfo.Key) {
                case MoveNodeDown:
                    Program.outputData.create_currentNode = Program.outputData.create_currentNode.MoveDown();
                    return;
                case MoveNodeUp:
                    Program.outputData.create_currentNode = Program.outputData.create_currentNode.MoveUp();
                    return;
                case MoveNodeLeft:
                    Program.outputData.create_currentNode = Program.outputData.create_currentNode.MoveLeft();
                    return;
                case MoveNodeRight:
                    Program.outputData.create_currentNode = Program.outputData.create_currentNode.MoveRight();
                    return;
            }

            // Check Node Dependant - TODO: add Confirm Checks where needed and Escape Checks as well
            switch (Program.outputData.create_currentNode) {
                case CreateNode.Name:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_name.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_name.Remove();
                            break;
                        default:
                            Program.outputData.create_name.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.INI:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_ini.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_ini.Remove();
                            break;
                        default:
                            Program.outputData.create_ini.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Speed:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_speed.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_speed.Remove();
                            break;
                        default:
                            Program.outputData.create_speed.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.AC:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_ac.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_ac.Remove();
                            break;
                        default:
                            Program.outputData.create_ac.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.HP:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_hp.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_hp.Remove();
                            break;
                        default:
                            Program.outputData.create_hp.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Temp:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_temp.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_temp.Remove();
                            break;
                        default:
                            Program.outputData.create_temp.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.ColoringType:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_coloring.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_coloring.Remove();
                            break;
                        default:
                            Program.outputData.create_coloring.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Remove:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_remove.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_remove.Remove();
                            break;
                        default:
                            Program.outputData.create_remove.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Coloring_BaseText:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_base_text.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_base_text.Remove();
                            break;
                        default:
                            Program.outputData.create_base_text.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Coloring_BaseBG:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_base_bg.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_base_bg.Remove();
                            break;
                        default:
                            Program.outputData.create_base_bg.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Coloring_ActiveText:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_active_text.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_active_text.Remove();
                            break;
                        default:
                            Program.outputData.create_active_text.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Coloring_ActiveBG:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_active_bg.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_active_bg.Remove();
                            break;
                        default:
                            Program.outputData.create_active_bg.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Score_Str:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_score_str.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_score_str.Remove();
                            break;
                        default:
                            Program.outputData.create_score_str.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Score_Dex:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_score_dex.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_score_dex.Remove();
                            break;
                        default:
                            Program.outputData.create_score_dex.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Score_Con:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_score_con.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_score_con.Remove();
                            break;
                        default:
                            Program.outputData.create_score_con.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Score_Int:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_score_int.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_score_int.Remove();
                            break;
                        default:
                            Program.outputData.create_score_int.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Score_Wis:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_score_wis.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_score_wis.Remove();
                            break;
                        default:
                            Program.outputData.create_score_wis.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Score_Cha:
                    switch (keyInfo.Key) {
                        case Clear:
                            Program.outputData.create_score_cha.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_score_cha.Remove();
                            break;
                        default:
                            Program.outputData.create_score_cha.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Attack_Name:
                    switch (keyInfo.Key) {
                        case RevertAdd:
                            Program.outputData.Create_RemoveAttack();
                            break;
                        case Confirm:
                            Program.outputData.Create_AddAttack();
                            break;
                        case Clear:
                            Program.outputData.create_attack_name.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_attack_name.Remove();
                            break;
                        default:
                            Program.outputData.create_attack_name.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Attack_Atk:
                    switch (keyInfo.Key) {
                        case RevertAdd:
                            Program.outputData.Create_RemoveAttack();
                            break;
                        case Confirm:
                            Program.outputData.Create_AddAttack();
                            break;
                        case Clear:
                            Program.outputData.create_attack_atk.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_attack_atk.Remove();
                            break;
                        default:
                            Program.outputData.create_attack_atk.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Attack_Mod:
                    switch (keyInfo.Key) {
                        case RevertAdd:
                            Program.outputData.Create_RemoveAttack();
                            break;
                        case Confirm:
                            Program.outputData.Create_AddAttack();
                            break;
                        case Clear:
                            Program.outputData.create_attack_mod.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_attack_mod.Remove();
                            break;
                        default:
                            Program.outputData.create_attack_mod.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Attack_Dmg:
                    switch (keyInfo.Key) {
                        case RevertAdd:
                            Program.outputData.Create_RemoveAttack();
                            break;
                        case Confirm:
                            Program.outputData.Create_AddAttack();
                            break;
                        case Clear:
                            Program.outputData.create_attack_dmg.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_attack_dmg.Remove();
                            break;
                        default:
                            Program.outputData.create_attack_dmg.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Condition:
                    switch (keyInfo.Key) {
                        case RevertAdd:
                            Program.outputData.Create_RemoveCondition();
                            break;
                        case Confirm:
                            Program.outputData.Create_AddCondition();
                            break;
                        case Clear:
                            Program.outputData.create_condition.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_condition.Remove();
                            break;
                        default:
                            Program.outputData.create_condition.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Note:
                    switch (keyInfo.Key) {
                        case RevertAdd:
                            Program.outputData.Create_RemoveNote();
                            break;
                        case Confirm:
                            Program.outputData.Create_AddNote();
                            break;
                        case Clear:
                            Program.outputData.create_note.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_note.Remove();
                            break;
                        default:
                            Program.outputData.create_note.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Description:
                    switch (keyInfo.Key) {
                        case RevertAdd:
                            Program.outputData.Create_RemoveDescriptionLine();
                            break;
                        case Confirm:
                            Program.outputData.Create_AddDescriptionLine();
                            break;
                        case Clear:
                            Program.outputData.create_description.Clear();
                            break;
                        case Remove:
                            Program.outputData.create_description.Remove();
                            break;
                        default:
                            Program.outputData.create_description.Add(keyInfo.KeyChar);
                            break;
                    }
                    break;
                case CreateNode.Create:
                    break;
                default:
                    return;
            }
            Program.outputData.Create_UpdateChecks();
        }
    }
}