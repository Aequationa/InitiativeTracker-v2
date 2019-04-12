using System;
using System.Collections.Generic;
using System.Text;

namespace InitiativeTracker
{
    public enum CreateNode : byte
    {
        Clear,
        Name,
        Speed,
        INI, AC,
        HP, Temp,
        Score_Str, Score_Dex, Score_Con, Score_Int, Score_Wis, Score_Cha,
        ColoringType, Remove,
        Coloring_BaseText, Coloring_BaseBG, Coloring_ActiveText, Coloring_ActiveBG, 
        Attack_Name,
        Attack_Atk, Attack_Mod, Attack_Dmg,
        Condition, 
        Note,
        Description,
        Create
    }
    public static class CreateNodeExt
    {
        public static CreateNode MoveDown(this CreateNode node) {
            switch (node) {
                case CreateNode.Clear:
                    return CreateNode.Name;
                case CreateNode.Name:
                    return CreateNode.Speed;
                case CreateNode.Speed:
                    return CreateNode.INI;
                case CreateNode.INI:
                    return CreateNode.HP;
                case CreateNode.AC:
                    return CreateNode.Temp;
                case CreateNode.HP:
                    return CreateNode.Score_Str;
                case CreateNode.Temp:
                    return CreateNode.Score_Dex;
                case CreateNode.Score_Str:
                    return CreateNode.ColoringType;
                case CreateNode.Score_Dex:
                case CreateNode.Score_Con:
                case CreateNode.Score_Int:
                case CreateNode.Score_Wis:
                case CreateNode.Score_Cha:
                    return CreateNode.Remove;
                case CreateNode.ColoringType:
                    return CreateNode.Coloring_BaseText;
                case CreateNode.Remove:
                    return CreateNode.Coloring_BaseBG;
                case CreateNode.Coloring_BaseText:
                    return CreateNode.Attack_Name;
                case CreateNode.Coloring_BaseBG:
                    return CreateNode.Attack_Atk;
                case CreateNode.Coloring_ActiveText:
                    return CreateNode.Attack_Mod;
                case CreateNode.Coloring_ActiveBG:
                    return CreateNode.Attack_Dmg;
                case CreateNode.Attack_Name:
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
                case CreateNode.Clear:
                case CreateNode.Name:
                    return CreateNode.Clear;
                case CreateNode.Speed:
                    return CreateNode.Name;
                case CreateNode.INI:
                case CreateNode.AC:
                    return CreateNode.Speed;
                case CreateNode.HP:
                    return CreateNode.INI;
                case CreateNode.Temp:
                    return CreateNode.AC;
                case CreateNode.Score_Str:
                    return CreateNode.HP;
                case CreateNode.Score_Dex:
                case CreateNode.Score_Con:
                case CreateNode.Score_Int:
                case CreateNode.Score_Wis:
                case CreateNode.Score_Cha:
                    return CreateNode.Temp;
                case CreateNode.ColoringType:
                    return CreateNode.Score_Str;
                case CreateNode.Remove:
                    return CreateNode.Score_Dex;
                case CreateNode.Coloring_BaseText:
                    return CreateNode.ColoringType;
                case CreateNode.Coloring_BaseBG:
                case CreateNode.Coloring_ActiveText:
                case CreateNode.Coloring_ActiveBG:
                    return CreateNode.Remove;
                case CreateNode.Attack_Name:
                    return CreateNode.Coloring_BaseText;
                case CreateNode.Attack_Atk:
                    return CreateNode.Coloring_BaseBG;
                case CreateNode.Attack_Mod:
                    return CreateNode.Coloring_ActiveText;
                case CreateNode.Attack_Dmg:
                    return CreateNode.Coloring_ActiveBG;
                case CreateNode.Condition:
                    return CreateNode.Attack_Name;
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
                case CreateNode.Clear:
                    return CreateNode.Clear;
                case CreateNode.Name:
                    return CreateNode.Name;
                case CreateNode.Speed:
                    return CreateNode.Speed;
                case CreateNode.INI:
                case CreateNode.AC:
                    return CreateNode.INI;
                case CreateNode.HP:
                case CreateNode.Temp:
                    return CreateNode.HP;
                case CreateNode.ColoringType:
                case CreateNode.Remove:
                    return CreateNode.ColoringType;
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
                case CreateNode.Coloring_BaseText:
                case CreateNode.Coloring_BaseBG:
                    return CreateNode.Coloring_BaseText;
                case CreateNode.Coloring_ActiveText:
                    return CreateNode.Coloring_BaseBG;
                case CreateNode.Coloring_ActiveBG:
                    return CreateNode.Coloring_ActiveText;
                case CreateNode.Attack_Name:
                case CreateNode.Attack_Atk:
                    return CreateNode.Attack_Name;
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
                case CreateNode.Clear:
                    return CreateNode.Clear;
                case CreateNode.Name:
                    return CreateNode.Name;
                case CreateNode.Speed:
                    return CreateNode.Speed;
                case CreateNode.INI:
                case CreateNode.AC:
                    return CreateNode.AC;
                case CreateNode.HP:
                case CreateNode.Temp:
                    return CreateNode.Temp;
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
                case CreateNode.Attack_Name:
                    return CreateNode.Attack_Atk;
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
    
    public enum WarnType : byte
    {
        AllowEmpty, ForbidEmpty, ExpectEmpty, ForceWarn
    }
    public abstract class InputField
    {
        public abstract void Add(char c);
        public abstract void Remove();
        public abstract void Clear();
        public abstract bool IsEmpty();

        public abstract int AddPrinting(Screen screen, int left, int top, WarnType warnType);
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

        public override bool IsEmpty() {
            return value.Count == 0;
        }
        public string GetValue() {
            return valueString;
        }

        public override int AddPrinting(Screen screen, int left, int top, WarnType warnType) {
            int availableWidth = screen.Width - left;
            if (top == screen.Height - 1) {
                --availableWidth;
            }
            string warnString = " !";
            bool putWarn;
            switch (warnType) {
                case WarnType.AllowEmpty:
                    putWarn = false;
                    break;
                case WarnType.ForbidEmpty:
                    putWarn = value.Count == 0;
                    break;
                case WarnType.ExpectEmpty:
                    putWarn = value.Count != 0;
                    break;
                case WarnType.ForceWarn:
                default:
                    putWarn = true;
                    break;
            }
            if (putWarn) {
                availableWidth -= warnString.Length;
            }
            if(availableWidth < 0) {
                return 0;
            }
            // Write Line
            string written;
            if(availableWidth >= valueString.Length) {
                written = valueString;
            }
            else {
                written = valueString.Substring(valueString.Length - availableWidth);
            }
            screen.AddFormattedLine(written, ConsoleColor.White, ConsoleColor.Black, left, int.MaxValue, top);
            int width = written.Length;
            // Add Warning
            if (putWarn) {
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

        public override bool IsEmpty() {
            return expr.Count == 0;
        }
        public bool HasValue() {
            return exprValid;
        }
        public List<Token> GetTokens() {
            return exprTokens;
        }

        public override int AddPrinting(Screen screen, int left, int top, WarnType warnType) {
            screen.AddFormattedLine(exprString, ConsoleColor.White, ConsoleColor.Black, left, int.MaxValue, top);
            int width = exprString.Length;
            string warnString = " !";
            bool putWarn;
            switch (warnType) {
                case WarnType.AllowEmpty:
                    putWarn = expr.Count != 0 && !exprValid;
                    break;
                case WarnType.ForbidEmpty:
                    putWarn = !exprValid;
                    break;
                case WarnType.ExpectEmpty:
                    putWarn = expr.Count != 0;
                    break;
                case WarnType.ForceWarn:
                default:
                    putWarn = true;
                    break;
            }
            if (putWarn) {
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


        public override bool IsEmpty() {
            return expr.Count == 0;
        }
        public bool HasValue() {
            return exprValue.HasValue;
        }
        public bool GetValue() {
            return exprValue.Value;
        }
        
        public override int AddPrinting(Screen screen, int left, int top, WarnType warnType) {
            int width = exprString.Length;
            screen.AddFormattedLine(exprString, ConsoleColor.White, ConsoleColor.Black, left, int.MaxValue, top);
            if (exprValue.HasValue) {
                // Add Continuation
                string continuationString = exprValue.Value.ToString().Substring(exprString.Length);
                screen.AddFormattedLine(continuationString, ConsoleColor.DarkGray, ConsoleColor.Black, left + width, int.MaxValue, top);
                width = exprValue.Value.ToString().Length;
            }
            // Add Warning
            bool putWarn;
            switch (warnType) {
                case WarnType.AllowEmpty:
                    putWarn = expr.Count != 0 && !exprValue.HasValue;
                    break;
                case WarnType.ForbidEmpty:
                    putWarn = !exprValue.HasValue;
                    break;
                case WarnType.ExpectEmpty:
                    putWarn = expr.Count != 0;
                    break;
                case WarnType.ForceWarn:
                default:
                    putWarn = true;
                    break;
            }
            if (putWarn) {
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

        public override bool IsEmpty() {
            return expr.Count == 0;
        }
        public bool HasValue() {
            return exprValue.HasValue;
        }
        public ConsoleColor GetValue() {
            return exprValue.Value;
        }

        public override int AddPrinting(Screen screen, int left, int top, WarnType warnType) {
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
            bool putWarn;
            switch (warnType) {
                case WarnType.AllowEmpty:
                    putWarn = expr.Count != 0 && !exprValue.HasValue;
                    break;
                case WarnType.ForbidEmpty:
                    putWarn = !exprValue.HasValue;
                    break;
                case WarnType.ExpectEmpty:
                    putWarn = expr.Count != 0;
                    break;
                case WarnType.ForceWarn:
                default:
                    putWarn = true;
                    break;
            }
            if (putWarn) {
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

        public override bool IsEmpty() {
            return expr.Count == 0;
        }
        public bool HasValue() {
            return exprValue.HasValue;
        }
        public Condition GetValue() {
            return exprValue.Value;
        }

        public override int AddPrinting(Screen screen, int left, int top, WarnType warnType) {
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
            bool putWarn;
            switch (warnType) {
                case WarnType.AllowEmpty:
                    putWarn = expr.Count != 0 && !exprValue.HasValue;
                    break;
                case WarnType.ForbidEmpty:
                    putWarn = !exprValue.HasValue;
                    break;
                case WarnType.ExpectEmpty:
                    putWarn = expr.Count != 0;
                    break;
                case WarnType.ForceWarn:
                default:
                    putWarn = true;
                    break;
            }
            if (putWarn) {
                string warnString = " !";
                screen.AddFormattedLine(warnString, ConsoleColor.Red, ConsoleColor.Black, left + width, int.MaxValue, top);
                width += warnString.Length;
            }
            return width;
        }
    }



    partial class OutputData
    {
        public CreateNode create_activeNode = CreateNode.Name;

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

        public Conditions create_conditions = new Conditions();
        public ConditionField create_condition = new ConditionField();
        public List<string> create_notes = new List<string>();
        public StringField create_note = new StringField();
        public List<string> create_descriptionLines = new List<string>();
        public StringField create_description = new StringField();

        /// <summary>
        /// If 0/4 Coloring Values are entered
        /// </summary>
        public bool create_check_noColors = false;
        /// <summary>
        /// If Coloring Type Exists
        /// </summary>
        public bool create_check_coloringValid = false;
        /// <summary>
        /// If no Scores are entered
        /// </summary>
        public bool create_check_noScores = false;
        /// <summary>
        /// If all everything is Correct
        /// </summary>
        public bool create_check_all = false;

        public string create_createError = null;

        public void Create_UpdateChecks() {
            // Update Coloring Check
            bool allColors = create_base_text.HasValue() && create_base_bg.HasValue() && create_active_text.HasValue() && create_active_bg.HasValue();
            create_check_noColors = create_base_text.IsEmpty() && create_base_bg.IsEmpty() && create_active_text.IsEmpty() && create_active_bg.IsEmpty();

            if (create_coloring.IsEmpty()) {
                create_check_coloringValid = Program.data.HasColoringType(Program.settings.defaultColoringType);
            }
            else {
                create_check_coloringValid = Program.data.HasColoringType(create_coloring.GetValue());
            }
            
            // Update Scores Check
            bool allScores = create_score_str.HasValue() && create_score_dex.HasValue() && create_score_con.HasValue()
                && create_score_int.HasValue() && create_score_wis.HasValue() && create_score_cha.HasValue();
            create_check_noScores = create_score_str.IsEmpty() && create_score_dex.IsEmpty() && create_score_con.IsEmpty()
                && create_score_int.IsEmpty() && create_score_wis.IsEmpty() && create_score_cha.IsEmpty();

            // Update Complete Check
            bool hasColoring = (create_check_noColors && create_check_coloringValid) || (allColors && create_coloring.IsEmpty());

            create_check_all = (create_ini.HasValue() || allScores) && (create_ac.HasValue() || create_ac.IsEmpty())
                && create_hp.HasValue() && (create_temp.HasValue() || create_temp.IsEmpty())
                && hasColoring && (create_remove.HasValue() || create_remove.IsEmpty())
                && (allScores || create_check_noScores);
        }

        public void Create_Setup() {
            Create_UpdateChecks();
        }
        
        public void Create_ClearData() {
            create_name.Clear();
            create_ini.Clear();
            create_speed.Clear();
            create_ac.Clear();
            create_hp.Clear();
            create_temp.Clear();

            create_coloring.Clear();
            create_remove.Clear();
            create_base_text.Clear();
            create_base_bg.Clear();
            create_active_text.Clear();
            create_active_bg.Clear();

            create_score_str.Clear();
            create_score_dex.Clear();
            create_score_con.Clear();
            create_score_int.Clear();
            create_score_wis.Clear();
            create_score_cha.Clear();

            create_attacks.Clear();
            create_attack_name.Clear();
            create_attack_atk.Clear();
            create_attack_mod.Clear();
            create_attack_dmg.Clear();

            create_conditions.Clear();
            create_condition.Clear();
            create_notes.Clear();
            create_note.Clear();
            create_descriptionLines.Clear();
            create_description.Clear();

            Create_UpdateChecks();
        }
        
        public void Create_AddAttack() {
            // Check if Attack Valid
            bool hasAtk = create_attack_atk.HasValue();
            bool hasMod = create_attack_mod.HasValue();
            bool valid = (hasAtk || create_attack_atk.IsEmpty()) && (hasMod || create_attack_mod.IsEmpty())
                && (hasAtk ^ hasMod) && create_attack_dmg.HasValue();
            if (valid) {
                if (hasAtk) {
                    create_attacks.Add(new Attack(create_attack_name.GetValue(), create_attack_atk.GetTokens(), create_attack_dmg.GetTokens()));
                }
                else {
                    List<Token> atk = new List<Token> { new Token(TokenType.Dice), new Token(TokenType.Integer, 20), new Token(TokenType.Add) };
                    atk.AddRange(create_attack_mod.GetTokens());
                    create_attacks.Add(new Attack(create_attack_name.GetValue(), atk, create_attack_dmg.GetTokens()));
                }
            }
        }
        public void Create_RemoveAttack() {
            if(create_attacks.Count > 0) {
                create_attacks.RemoveAt(create_attacks.Count - 1);
            }
        }
        public bool Create_AddCondition() {
            if (create_condition.HasValue()) {
                var condition = create_condition.GetValue();
                if (create_conditions.Add(condition)) {
                    create_condition.Clear();
                    return true;
                }
            }
            return false;
        }
        public void Create_RemoveCondition() {
            create_conditions.Remove();
        }
        public void Create_AddNote() {
            create_notes.Add(create_note.GetValue());
            create_note.Clear();
        }
        public void Create_RemoveNote() {
            if(create_notes.Count > 0) {
                create_notes.RemoveAt(create_notes.Count - 1);
            }
        }
        public void Create_AddDescriptionLine() {
            create_descriptionLines.Add(create_description.GetValue());
            create_description.Clear();
        }
        public void Create_RemoveDescriptionLine() {
            if(create_descriptionLines.Count > 0) {
                create_descriptionLines.RemoveAt(create_descriptionLines.Count - 1);
            }
        }
        
        public Actor Create_CreateActor(out string errorMessage) {
            Actor actor = new Actor();
            // Get Name, Speed
            actor.name = create_name.GetValue();
            actor.speed = create_speed.GetValue();
            // Get AC
            if (!create_ac.IsEmpty()) {
                if (!create_ac.HasValue()) {
                    errorMessage = "[Create] Unable to Compute: AC";
                    return actor;
                }
                var eval_armorClass = create_ac.GetTokens().Evaluate();
                if (!eval_armorClass.HasValue) {
                    errorMessage = "[Create] Unable to Compute: AC";
                    return actor;
                }
                actor.armorClass = eval_armorClass.Value;
            }
            // Get HP Values
            if (create_hp.IsEmpty() || !create_hp.HasValue()) {
                errorMessage = "[Create] Unable to Compute: HP";
                return actor;
            }
            var eval_HP = create_hp.GetTokens().Evaluate();
            if (!eval_HP.HasValue) {
                errorMessage = "[Create] Unable to Compute: HP";
                return actor;
            }
            actor.currentHP = eval_HP.Value;
            actor.maximumHP = eval_HP.Value;

            if (!create_temp.IsEmpty()) {
                if (!create_temp.HasValue()) {
                    errorMessage = "[Create] Unable to Compute: Temp";
                    return actor;
                }
                var eval_temporary = create_temp.GetTokens().Evaluate();
                if (!eval_temporary.HasValue) {
                    errorMessage = "[Create] Unable to Compute: Temp";
                    return actor;
                }
                actor.temporaryHP = eval_temporary.Value;
            }
            // Get Color Values
            bool noColoring = create_base_text.IsEmpty() && create_base_bg.IsEmpty() && create_active_text.IsEmpty() && create_active_bg.IsEmpty();
            bool hasColoring = create_base_text.HasValue() && create_base_bg.HasValue() && create_active_text.HasValue() && create_active_bg.HasValue();
            if (hasColoring) {
                if (!create_coloring.IsEmpty()) {
                    errorMessage = "[Create] Ambiguity: Coloring is defined twice";
                    return actor;
                }
                actor.base_text = create_base_text.GetValue();
                actor.base_bg = create_base_bg.GetValue();
                actor.active_text = create_active_text.GetValue();
                actor.active_bg = create_active_bg.GetValue();
            }
            else {
                if (!noColoring) {
                    errorMessage = "[Create] Unable to Compute some Colors";
                    return actor;
                }
                string targetColoringType;
                if (create_coloring.IsEmpty()) {
                    targetColoringType = Program.settings.defaultColoringType;
                }
                else {
                    targetColoringType = create_coloring.GetValue();
                }

                bool found = false;
                for (int index = 0; index < Program.data.loadedColorings.Count; ++index) {
                    if (Program.data.loadedColorings[index].name == targetColoringType) {
                        actor.base_text = Program.data.loadedColorings[index].base_text;
                        actor.base_bg = Program.data.loadedColorings[index].base_bg;
                        actor.active_text = Program.data.loadedColorings[index].active_text;
                        actor.active_bg = Program.data.loadedColorings[index].active_bg;
                        found = true;
                        break;
                    }
                }
                if (!found) {
                    errorMessage = "[Create] Coloring not found: " + targetColoringType;
                    return actor;
                }
            }
            // Get Remove
            if (create_remove.IsEmpty()) {
                actor.remove = Program.settings.defaultRemove;
            }
            else if (create_remove.HasValue()) {
                actor.remove = create_remove.GetValue();
            }
            else {
                errorMessage = "[Create] Unable to Compute: Remove";
                return actor;
            }
            // Get Scores
            bool noScores = create_score_str.IsEmpty() && create_score_dex.IsEmpty() && create_score_con.IsEmpty()
                && create_score_int.IsEmpty() && create_score_wis.IsEmpty() && create_score_cha.IsEmpty();
            bool allScores = create_score_str.HasValue() && create_score_dex.HasValue() && create_score_con.HasValue()
                && create_score_int.HasValue() && create_score_wis.HasValue() && create_score_cha.HasValue();
            if(!noScores && !allScores) {
                errorMessage = "[Create] Unable to Compute some Scores";
                return actor;
            }
            if (allScores) {
                Scores scores = new Scores();
                var strength = create_score_str.GetTokens().Evaluate();
                if (!strength.HasValue) {
                    errorMessage = "[Create] Unable to Compute: str";
                    return actor;
                }
                scores.strength = strength.Value;
                var dexterity = create_score_dex.GetTokens().Evaluate();
                if (!dexterity.HasValue) {
                    errorMessage = "[Create] Unable to Compute: dex";
                    return actor;
                }
                scores.dexterity = dexterity.Value;
                var constitution = create_score_con.GetTokens().Evaluate();
                if (!constitution.HasValue) {
                    errorMessage = "[Create] Unable to Compute: con";
                    return actor;
                }
                scores.constitution = constitution.Value;
                var intelligence = create_score_int.GetTokens().Evaluate();
                if (!intelligence.HasValue) {
                    errorMessage = "[Create] Unable to Compute: int";
                    return actor;
                }
                scores.intelligence = intelligence.Value;
                var wisdon = create_score_wis.GetTokens().Evaluate();
                if (!wisdon.HasValue) {
                    errorMessage = "[Create] Unable to Compute: wis";
                    return actor;
                }
                scores.wisdom = wisdon.Value;
                var charisma = create_score_cha.GetTokens().Evaluate();
                if (!charisma.HasValue) {
                    errorMessage = "[Create] Unable to Compute: cha";
                    return actor;
                }
                scores.charisma = charisma.Value;

                scores.CalculateModifiers();
                actor.scores = scores;
            }
            // Get Intiative
            if (!create_ini.IsEmpty()) {
                if (!create_ini.HasValue()) {
                    errorMessage = "[Create] Unable to Compute: ini";
                    return actor;
                }
                var eval_initiative = create_ini.GetTokens().Evaluate();
                if (!eval_initiative.HasValue) {
                    errorMessage = "[Create] Unable to Compute: ini";
                    return actor;
                }
                actor.initiative = eval_initiative.Value;
            }
            else {
                if (actor.scores.HasValue) {
                    actor.initiative = ObjectParser.Roll(1, 20) + actor.scores.Value.dexterityModifier;
                }
                else {
                    errorMessage = "[Create] Unable to Determine Value: ini";
                    return actor;
                }
            }
            // Get Attacks, Description, Conditions 
            actor.attacks = new List<Attack>(create_attacks);
            actor.description = new List<string>(create_descriptionLines);
            
            // Get Notes
            for (int index = 0; index < create_notes.Count; ++index) {
                actor.notes.Add(new Note(actor.nextNoteIndex, create_notes[index]));
                ++actor.nextNoteIndex;
            }

            errorMessage = null;
            return actor;
        }
    }

    partial class Output
    {
        private static int AddField(this Screen screen, string argName, InputField argField, bool isActive, int left, int top, WarnType warnType) {
            int x = left;
            // Add Active 
            if (isActive) {
                screen.AddFormattedLine("► ", ConsoleColor.White, ConsoleColor.Black, x, int.MaxValue, top);
                x += 2;
            }
            else {
                screen.AddFormattedLine("  ", ConsoleColor.White, ConsoleColor.Black, x, int.MaxValue, top);
                x += 2;
            }
            // Add Arg Name
            screen.AddFormattedLine(argName, ConsoleColor.White, ConsoleColor.Black, x, int.MaxValue, top);
            x += argName.Length;
            // Add Arg Value
            x += argField.AddPrinting(screen, x, top, warnType);
            return x - left;
        }
        private static int AddButton(this Screen screen, string buttonName, bool isActive, int left, int top, bool putWarn) {
            int x = left;
            // Add Active 
            if (isActive) {
                screen.AddFormattedLine("► ", ConsoleColor.White, ConsoleColor.Black, x, int.MaxValue, top);
                x += 2;
            }
            else {
                screen.AddFormattedLine("  ", ConsoleColor.White, ConsoleColor.Black, x, int.MaxValue, top);
                x += 2;
            }
            // Add Button Name
            screen.AddFormattedLine(buttonName, ConsoleColor.White, ConsoleColor.Black, x, int.MaxValue, top);
            x += buttonName.Length;
            // Add Warn
            if (putWarn) {
                string warnString = " !";
                screen.AddFormattedLine(warnString, ConsoleColor.Red, ConsoleColor.Black, x, int.MaxValue, top);
                x += warnString.Length;
            }
            return x - left;
        }
        private static int AddDefaultValue(this Screen screen, string argName, string defaultValue, bool isActive, int left, int top, bool putWarn) {
            int x = left;
            // Add Active 
            if (isActive) {
                screen.AddFormattedLine("► ", ConsoleColor.White, ConsoleColor.Black, x, int.MaxValue, top);
                x += 2;
            }
            else {
                screen.AddFormattedLine("  ", ConsoleColor.White, ConsoleColor.Black, x, int.MaxValue, top);
                x += 2;
            }
            // Add Arg Name
            screen.AddFormattedLine(argName, ConsoleColor.White, ConsoleColor.Black, x, int.MaxValue, top);
            x += argName.Length;
            // Add Default Value
            screen.AddFormattedLine(defaultValue, ConsoleColor.DarkGray, ConsoleColor.Black, x, int.MaxValue, top);
            x += defaultValue.Length;
            // Add Warn
            if (putWarn) {
                string warnString = " !";
                screen.AddFormattedLine(warnString, ConsoleColor.Red, ConsoleColor.Black, x, int.MaxValue, top);
                x += warnString.Length;
            }
            return x - left;
        }

        private const int CreateTopBorder = 0;
        private const int CreateLeftBorder = 0;
        private const int CreateLeftAttacks = 4;
        private const int CreateLeftConditions = 4;
        private const int CreateLeftNotes = 3;
        private const int CreateLeftDescription = 5;
        private const int CreateSpace = 1;
        private static Screen GetScreen_Create() {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            Screen screen = Screen.Default(width, height);

            // Add Fields
            int top = CreateTopBorder;
            screen.AddButton("[Clear Data]", Program.outputData.create_activeNode == CreateNode.Clear, 
                CreateLeftBorder, top, false);
            ++top;
            screen.AddFullDoubleHLine(top);
            ++top;
            screen.AddField("Name=", Program.outputData.create_name, Program.outputData.create_activeNode == CreateNode.Name, 
                CreateLeftBorder, top, WarnType.AllowEmpty);
            ++top;
            // INI, Speed, AC
            screen.AddField("Speed=", Program.outputData.create_speed, Program.outputData.create_activeNode == CreateNode.Speed, 
                CreateLeftBorder, top, WarnType.AllowEmpty);
            ++top;
            {
                int left = CreateLeftBorder;
                left += screen.AddField("INI=", Program.outputData.create_ini, Program.outputData.create_activeNode == CreateNode.INI,
                    left, top, Program.outputData.create_check_noScores ? WarnType.ForbidEmpty : WarnType.AllowEmpty);
                left += CreateSpace;
                screen.AddField("AC=", Program.outputData.create_ac, Program.outputData.create_activeNode == CreateNode.AC,
                    left, top, WarnType.AllowEmpty);
            }
            ++top;
            // HP
            {
                int left = CreateLeftBorder;
                left += screen.AddField("HP=", Program.outputData.create_hp, Program.outputData.create_activeNode == CreateNode.HP, 
                    left, top, WarnType.ForbidEmpty);
                left += CreateSpace;
                screen.AddField("Temp=", Program.outputData.create_temp, Program.outputData.create_activeNode == CreateNode.Temp, 
                    left, top, WarnType.AllowEmpty);
            }
            ++top;
            // Scores
            {
                int left = CreateLeftBorder;
                left += screen.AddField("STR=", Program.outputData.create_score_str, Program.outputData.create_activeNode == CreateNode.Score_Str,
                    left, top, Program.outputData.create_check_noScores ? WarnType.AllowEmpty : WarnType.ForbidEmpty);
                left += CreateSpace;
                left += screen.AddField("DEX=", Program.outputData.create_score_dex, Program.outputData.create_activeNode == CreateNode.Score_Dex,
                    left, top, Program.outputData.create_check_noScores ? WarnType.AllowEmpty : WarnType.ForbidEmpty);
                left += CreateSpace;
                left += screen.AddField("CON=", Program.outputData.create_score_con, Program.outputData.create_activeNode == CreateNode.Score_Con,
                    left, top, Program.outputData.create_check_noScores ? WarnType.AllowEmpty : WarnType.ForbidEmpty);
                left += CreateSpace;
                left += screen.AddField("INT=", Program.outputData.create_score_int, Program.outputData.create_activeNode == CreateNode.Score_Int,
                    left, top, Program.outputData.create_check_noScores ? WarnType.AllowEmpty : WarnType.ForbidEmpty);
                left += CreateSpace;
                left += screen.AddField("WIS=", Program.outputData.create_score_wis, Program.outputData.create_activeNode == CreateNode.Score_Wis,
                    left, top, Program.outputData.create_check_noScores ? WarnType.AllowEmpty : WarnType.ForbidEmpty);
                left += CreateSpace;
                left += screen.AddField("CHA=", Program.outputData.create_score_cha, Program.outputData.create_activeNode == CreateNode.Score_Cha,
                    left, top, Program.outputData.create_check_noScores ? WarnType.AllowEmpty : WarnType.ForbidEmpty);
            }
            ++top;
            screen.AddFullHLine(top);
            ++top;
            // Coloring
            {
                int left = CreateLeftBorder;
                if(Program.outputData.create_check_noColors && Program.outputData.create_coloring.IsEmpty()) {
                    left += screen.AddDefaultValue("Coloring=", Program.settings.defaultColoringType, Program.outputData.create_activeNode == CreateNode.ColoringType,
                        left, top, !Program.outputData.create_check_coloringValid);
                }
                else {
                    left += screen.AddField("Coloring=", Program.outputData.create_coloring, Program.outputData.create_activeNode == CreateNode.ColoringType,
                        left, top, Program.outputData.create_check_noColors ? (Program.outputData.create_check_coloringValid ? WarnType.AllowEmpty : WarnType.ForceWarn) : WarnType.ExpectEmpty);
                }
                left += CreateSpace;
                if (Program.outputData.create_remove.IsEmpty()) {
                    screen.AddDefaultValue("Remove=", Program.settings.defaultRemove.ToString(), Program.outputData.create_activeNode == CreateNode.Remove, 
                        left, top, false);
                }
                else {
                    screen.AddField("Remove=", Program.outputData.create_remove, Program.outputData.create_activeNode == CreateNode.Remove,
                        left, top, WarnType.AllowEmpty);
                }
            }
            ++top;
            {
                int left = CreateLeftBorder;
                left += screen.AddField("Base_Text=", Program.outputData.create_base_text, Program.outputData.create_activeNode == CreateNode.Coloring_BaseText,
                    left, top, Program.outputData.create_check_noColors ? WarnType.AllowEmpty : WarnType.ForbidEmpty);
                left += CreateSpace;
                left += screen.AddField("Base_BG=", Program.outputData.create_base_bg, Program.outputData.create_activeNode == CreateNode.Coloring_BaseBG,
                    left, top, Program.outputData.create_check_noColors ? WarnType.AllowEmpty : WarnType.ForbidEmpty);
                left += CreateSpace;
                left += screen.AddField("Active_Text=", Program.outputData.create_active_text, Program.outputData.create_activeNode == CreateNode.Coloring_ActiveText,
                    left, top, Program.outputData.create_check_noColors ? WarnType.AllowEmpty : WarnType.ForbidEmpty);
                left += CreateSpace;
                left += screen.AddField("Actie_BG=", Program.outputData.create_active_bg, Program.outputData.create_activeNode == CreateNode.Coloring_ActiveBG,
                    left, top, Program.outputData.create_check_noColors ? WarnType.AllowEmpty : WarnType.ForbidEmpty);
            }
            ++top;
            screen.AddFullDoubleHLine(top);
            ++top;
            // Attacks
            for(int index = 0; index < Program.outputData.create_attacks.Count; ++index) {
                string attackString = Program.outputData.create_attacks[index].name 
                    + " (Atk=" + Program.outputData.create_attacks[index].attack.GetString() 
                    + ",Dmg=" + Program.outputData.create_attacks[index].damage.GetString() + ")";
                screen.AddFormattedLine(attackString, ConsoleColor.White, ConsoleColor.Black, CreateLeftAttacks, int.MaxValue, top);
                ++top;
            }
            // Attack
            {
                int left = CreateLeftBorder;
                left += screen.AddField("Name=", Program.outputData.create_attack_name, Program.outputData.create_activeNode == CreateNode.Attack_Name,
                    left, top, WarnType.AllowEmpty);
                left += CreateSpace;
                left += screen.AddField("Atk=", Program.outputData.create_attack_atk, Program.outputData.create_activeNode == CreateNode.Attack_Atk,
                    left, top, Program.outputData.create_attack_mod.IsEmpty() ? WarnType.ForbidEmpty : WarnType.ExpectEmpty);
                left += CreateSpace;
                left += screen.AddField("Mod=", Program.outputData.create_attack_mod, Program.outputData.create_activeNode == CreateNode.Attack_Mod,
                    left, top, Program.outputData.create_attack_atk.IsEmpty() ? WarnType.ForbidEmpty : WarnType.ExpectEmpty);
                left += CreateSpace;
                left += screen.AddField("Dmg=", Program.outputData.create_attack_dmg, Program.outputData.create_activeNode == CreateNode.Attack_Dmg,
                    left, top, WarnType.ForbidEmpty);
                left += CreateSpace;
            }
            ++top;
            screen.AddFullHLine(top);
            ++top;
            // Conditions
            if (!Program.outputData.create_conditions.IsEmpty()) {
                using (var conditionEnum = Program.outputData.create_conditions.GetEnumerator()) {
                    conditionEnum.MoveNext();
                    StringBuilder conditions = new StringBuilder(conditionEnum.Current.GetName());
                    while (conditionEnum.MoveNext()) {
                        conditions.Append(", ");
                        conditions.Append(conditionEnum.Current.GetName());
                    }
                    top += screen.AddFormattedLines(new List<string> { conditions.ToString() },
                        ConsoleColor.White, ConsoleColor.Black, CreateLeftBorder + CreateLeftConditions, screen.Width, top);
                }
            }
            // Condition
            screen.AddField("Condition=", Program.outputData.create_condition, Program.outputData.create_activeNode == CreateNode.Condition,
                CreateLeftBorder, top, (Program.outputData.create_condition.HasValue() && Program.outputData.create_conditions.Contains(Program.outputData.create_condition.GetValue())) ? WarnType.ForceWarn : WarnType.ForbidEmpty);
            ++top;
            screen.AddFullHLine(top);
            ++top;
            // Notes
            for(int index = 0; index < Program.outputData.create_notes.Count; ++index) {
                top += screen.AddFormattedLines(new List<string> { (index + 1).ToString() + ") " + Program.outputData.create_notes[index] },
                    ConsoleColor.White, ConsoleColor.Black, CreateLeftBorder + CreateLeftNotes, screen.Width, top);
            }
            // Note
            screen.AddField("Note=", Program.outputData.create_note, Program.outputData.create_activeNode == CreateNode.Note,
                CreateLeftBorder, top, WarnType.AllowEmpty);
            ++top;
            screen.AddFullHLine(top);
            ++top;
            // Description
            top += screen.AddFormattedLines(Program.outputData.create_descriptionLines,
                ConsoleColor.White, ConsoleColor.Black, CreateLeftBorder + CreateLeftDescription, screen.Width, top);
            // Description Line
            screen.AddField("Description=", Program.outputData.create_description, Program.outputData.create_activeNode == CreateNode.Description,
                CreateLeftBorder, top, WarnType.AllowEmpty);
            ++top;
            screen.AddFullDoubleHLine(top);
            ++top;

            // Create Button
            screen.AddButton("[Create Actor]", Program.outputData.create_activeNode == CreateNode.Create,
                CreateLeftBorder, top, !Program.outputData.create_check_all);

            return screen;
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
                    Program.outputData.create_activeNode = Program.outputData.create_activeNode.MoveDown();
                    return;
                case MoveNodeUp:
                    Program.outputData.create_activeNode = Program.outputData.create_activeNode.MoveUp();
                    return;
                case MoveNodeLeft:
                    Program.outputData.create_activeNode = Program.outputData.create_activeNode.MoveLeft();
                    return;
                case MoveNodeRight:
                    Program.outputData.create_activeNode = Program.outputData.create_activeNode.MoveRight();
                    return;
            }

            // Check Node Dependant - TODO: add Confirm Checks where needed and Escape Checks as well
            switch (Program.outputData.create_activeNode) {
                case CreateNode.Clear:
                    switch (keyInfo.Key) {
                        case Confirm:
                            Program.outputData.Create_ClearData();
                            break;
                    }
                    break;
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
                    switch (keyInfo.Key) {
                        case Confirm:
                            var actor = Program.outputData.Create_CreateActor(out Program.outputData.create_createError);
                            if(Program.outputData.create_createError == null) {
                                Program.data.AddActor(actor);
                            }
                            break;
                    }
                    break;
                default:
                    return;
            }
            Program.outputData.Create_UpdateChecks();
        }
    }
}