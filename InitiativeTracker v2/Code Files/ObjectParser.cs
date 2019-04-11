using System;
using System.Collections.Generic;

namespace InitiativeTracker
{
    public enum TokenType : byte
    {
        Integer, Bracket, Dice, Add, Subtract, Multiply, Divide
    }
    public class Token
    {
        public TokenType type;
        public int value;

        public Token(TokenType type, int value = 0) {
            this.type = type;
            this.value = value;
        }

        public Token Copy() {
            return new Token(type, value);
        }
    }

    public static class ObjectParser
    {
        // === Formatting ===
        /// <summary>
        /// Returns Number of Spaces at the Start of Text
        /// </summary>
        public static int GetSpaceCount(this string text) {
            for (int index = 0; index < text.Length; ++index) {
                if(text[index] != ' ') {
                    return index;
                }
            }
            return text.Length;
        }
        public static string ToSignedString(this int value) {
            if (value >= 0) {
                return "+" + value.ToString();
            }
            else {
                return value.ToString();
            }
        }

        public static void FormatExpressionChar(char c, Action<char> AddChar) {
            switch (c) {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '+':
                case '-':
                case '*':
                case '/':
                case 'd':
                case 'D':
                case '(':
                case ')':
                    AddChar(c);
                    return;
                default:
                    // Ignore Character
                    return;
            }
        }
        public static void FormatExpressionChar(char c, ref List<char> list) {
            FormatExpressionChar(c, list.Add);
        }
        public static void FormatChar(char c, Action<char> AddChar) {
            if (c == '\t') {
                // Convert Tab Key
                for (int count = 0; count < Program.settings.tabWidth; ++count) {
                    AddChar(' ');
                }
            }
            else if (('\u0000' <= c && c <= '\u001f') || ('\u007f' <= c && c <= '\u009f')) {
                // Ignore other Control Characters
            }
            else {
                // Add to Line
                AddChar(c);
            }
        }
        public static void FormatChar(char c, ref List<char> list) {
            FormatChar(c, list.Add);
        }
        public static string FormatLine(string text) {
            List<char> current = new List<char>();
            // Set uniforn newline Character
            for (int index = 0; index < text.Length; ++index) {
                FormatChar(text[index], ref current);
            }
            return string.Concat(current);
        }
        private const string ReduceMode = "~";
        /// <summary>
        /// Note: This only parses Description
        /// </summary>
        public static List<string> FormatText(string text) {
            List<string> lines = new List<string>();
            // Set uniforn newline Character
            text = text.Replace("\n\r", "\n").Replace('\r', '\n');

            // Setup
            int start = 0;
            for (int index = 0; index < text.Length; ++index) {
                if (text[index] == '\n') {
                    // Finish Line
                    lines.Add(FormatLine(text.Substring(start, index - start)));
                    start = index + 1;
                }
                else {
                    // Do nothing
                }
            }
            // Finish last Line
            lines.Add(FormatLine(text.Substring(start)));

            // Check for Reduced Mode
            if(lines[0] == ReduceMode) {
                // Ignore first & last Line
                lines.RemoveAt(0);
                if(lines.Count > 0) {
                    lines.RemoveAt(lines.Count - 1);
                }
                // Find reduce Amount
                int width = 0;
                for(int index = 0; index < lines.Count; ++index) {
                    width = Math.Max(lines[index].GetSpaceCount(), width);
                }
                // Remove Spaces
                for(int index = 0; index < lines.Count; ++index) {
                    lines[index] = lines[index].Substring(width);
                }
            }
            return lines;
        }

        // === Calculation ===
        /// <summary>
        /// Assumes type and amount are positive and small enough
        /// </summary>
        public static int Roll(int amount, int type) {
            if (type < 2) {
                return type * amount;
            }
            else {
                int sum = 0;
                for (int index = 0; index < amount; ++index) {
                    sum += Program.random.Next(1, type + 1);
                }
                return sum;
            }
        }
        public static int Divide(int numenator, int denominator) {
            if(denominator == 0) {
                throw new DivideByZeroException(nameof(denominator));
            }
            else if(denominator > 0) {
                if(numenator >= 0) {
                    return numenator / denominator;
                }
                else {
                    return (numenator - denominator + 1) / denominator;
                }
            }
            else {
                if(denominator < 0) {
                    return numenator / denominator;
                }
                else {
                    return (numenator - denominator - 1) / denominator;
                }
            }
        }        
        
        // === Parse Integer Expression ===
        public static Option<List<Token>> GetTokens(string text) {
            Stack<int> bracketStarts = new Stack<int>();
            List<Token> tokens = new List<Token>();

            int intStart = int.MinValue;
            for (int index = 0; index < text.Length; ++index) {
                switch (text[index]) {
                    case '+':
                        if (intStart >= 0) {
                            tokens.Add(new Token(TokenType.Integer, ObjectParser.GetInt10_Unsafe(text, intStart, index)));
                            intStart = int.MinValue;
                        }
                        tokens.Add(new Token(TokenType.Add));
                        break;
                    case '-':
                        if (intStart >= 0) {
                            tokens.Add(new Token(TokenType.Integer, ObjectParser.GetInt10_Unsafe(text, intStart, index)));
                            intStart = int.MinValue;
                        }
                        tokens.Add(new Token(TokenType.Subtract));
                        break;
                    case '*':
                        if (intStart >= 0) {
                            tokens.Add(new Token(TokenType.Integer, ObjectParser.GetInt10_Unsafe(text, intStart, index)));
                            intStart = int.MinValue;
                        }
                        tokens.Add(new Token(TokenType.Multiply));
                        break;
                    case '/':
                        if (intStart >= 0) {
                            tokens.Add(new Token(TokenType.Integer, ObjectParser.GetInt10_Unsafe(text, intStart, index)));
                            intStart = int.MinValue;
                        }
                        tokens.Add(new Token(TokenType.Divide));
                        break;
                    case 'd':
                    case 'D':
                        if (intStart >= 0) {
                            tokens.Add(new Token(TokenType.Integer, ObjectParser.GetInt10_Unsafe(text, intStart, index)));
                            intStart = int.MinValue;
                        }
                        tokens.Add(new Token(TokenType.Dice));
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        if (intStart < 0) {
                            intStart = index;
                        }
                        break;
                    case '(':
                        if (intStart >= 0) {
                            tokens.Add(new Token(TokenType.Integer, ObjectParser.GetInt10_Unsafe(text, intStart, index)));
                            intStart = int.MinValue;
                        }
                        bracketStarts.Push(tokens.Count);
                        tokens.Add(new Token(TokenType.Bracket));
                        break;
                    case ')':
                        if (intStart >= 0) {
                            tokens.Add(new Token(TokenType.Integer, ObjectParser.GetInt10_Unsafe(text, intStart, index)));
                            intStart = int.MinValue;
                        }
                        if (bracketStarts.Count == 0) {
                            return Option<List<Token>>.Null;
                        }
                        else {
                            tokens[bracketStarts.Peek()].value = tokens.Count;
                            bracketStarts.Pop();
                            break;
                        }
                    default:
                        return Option<List<Token>>.Null;
                }
            }
            if (intStart >= 0) {
                tokens.Add(new Token(TokenType.Integer, ObjectParser.GetInt10_Unsafe(text, intStart, text.Length)));
            }
            if (bracketStarts.Count == 0) {
                return tokens;
            }
            else {
                return Option<List<Token>>.Null;
            }
        }
        
        
        private static bool ValidateTokens(List<Token> tokens, int start, int end) {
            // Check (...) and create a List of simple Tokens without (...)
            List<Token> simpleTokens = new List<Token>();
            for (int index = start; index < end;) {
                if (tokens[index].type == TokenType.Bracket) {
                    if (!ValidateTokens(tokens, index + 1, tokens[index].value)) {
                        return false;
                    }
                    simpleTokens.Add(new Token(TokenType.Integer));
                    index = tokens[index].value;
                }
                else {
                    simpleTokens.Add(tokens[index].Copy());
                    ++index;
                }
            }
            // Check Dice Rolls and simplify List
            for (int index = 0; index < simpleTokens.Count;) {
                if (simpleTokens[index].type == TokenType.Dice) {
                    // we have a Dice
                    bool intBefore = index > 0 && simpleTokens[index - 1].type == TokenType.Integer;
                    bool intAfter = index < simpleTokens.Count - 1 && simpleTokens[index + 1].type == TokenType.Integer;

                    simpleTokens[index].type = TokenType.Integer;
                    if (intAfter) {
                        simpleTokens.RemoveAt(index + 1);
                    }
                    if (intBefore) {
                        simpleTokens.RemoveAt(index - 1);
                    }
                    else {
                        ++index;
                    }
                }
                else {
                    ++index;
                }
            }
            // Check unary Minus operator and simplify List
            for (int index = simpleTokens.Count - 1; index >= 0;) {
                if ((simpleTokens[index].type == TokenType.Subtract || simpleTokens[index].type == TokenType.Add) && (index == 0 || simpleTokens[index - 1].type != TokenType.Integer)) {
                    // we have a unitary Operator
                    if (index == simpleTokens.Count - 1 || simpleTokens[index + 1].type != TokenType.Integer) {
                        return false;
                    }
                    simpleTokens.RemoveAt(index);
                    --index;
                }
                else {
                    --index;
                }
            }
            // Check Binary Operators
            for (int index = 0; index < simpleTokens.Count;) {
                if (simpleTokens[index].type == TokenType.Multiply || simpleTokens[index].type == TokenType.Divide
                    || simpleTokens[index].type == TokenType.Add || simpleTokens[index].type == TokenType.Subtract) {
                    if (index == 0 || simpleTokens[index - 1].type != TokenType.Integer ||
                        index == simpleTokens.Count - 1 || simpleTokens[index + 1].type != TokenType.Integer) {
                        return false;
                    }
                    simpleTokens.RemoveAt(index + 1);
                    simpleTokens.RemoveAt(index);
                }
                else {
                    ++index;
                }
            }
            // Check if we got one Number
            return simpleTokens.Count == 1;
        }
        /// <summary>
        /// Returns whether Tokens can be evaluated
        /// </summary>
        public static bool ValidateTokens(List<Token> tokens) {
            return ValidateTokens(tokens, 0, tokens.Count);
        }

        private static Option<int> EvaluateTokens(List<Token> tokens, int start, int end) {
            // Evaluate (...) and create a List of simple Tokens without (...)
            List<Token> simpleTokens = new List<Token>();
            for (int index = start; index < end;) {
                if (tokens[index].type == TokenType.Bracket) {
                    var result = EvaluateTokens(tokens, index + 1, tokens[index].value);
                    if (!result.HasValue) {
                        return Option<int>.Null;
                    }
                    simpleTokens.Add(new Token(TokenType.Integer, result.Value));
                    index = tokens[index].value;
                }
                else {
                    simpleTokens.Add(tokens[index].Copy());
                    ++index;
                }
            }
            // Evaluate Dice Rolls and simplify List
            for (int index = 0; index < simpleTokens.Count;) {
                if (simpleTokens[index].type == TokenType.Dice) {
                    // we have a Dice
                    bool intBefore = index > 0 && simpleTokens[index - 1].type == TokenType.Integer;
                    bool intAfter = index < simpleTokens.Count - 1 && simpleTokens[index + 1].type == TokenType.Integer;

                    int value = Roll(intBefore ? simpleTokens[index - 1].value : 1, intAfter ? simpleTokens[index + 1].value : Program.settings.defaultType);
                    simpleTokens[index].type = TokenType.Integer;
                    simpleTokens[index].value = value;
                    if (intAfter) {
                        simpleTokens.RemoveAt(index + 1);
                    }
                    if (intBefore) {
                        simpleTokens.RemoveAt(index - 1);
                    }
                    else {
                        ++index;
                    }
                }
                else {
                    ++index;
                }
            }
            // Evaluate unary Operators and simplify List
            for (int index = simpleTokens.Count - 1; index >= 0;) {
                if ((simpleTokens[index].type == TokenType.Subtract || simpleTokens[index].type == TokenType.Add) && (index == 0 || simpleTokens[index - 1].type != TokenType.Integer)) {
                    // we have a unitary Operator
                    if (index == simpleTokens.Count - 1 || simpleTokens[index + 1].type != TokenType.Integer) {
                        return Option<int>.Null;
                    }
                    if(simpleTokens[index].type == TokenType.Subtract) {
                        simpleTokens[index + 1].value = -simpleTokens[index + 1].value;
                    }
                    simpleTokens.RemoveAt(index);
                    --index;
                }
                else {
                    --index;
                }
            }
            // Evaluate Muliply and Divide
            for (int index = 0; index < simpleTokens.Count;) {
                if (simpleTokens[index].type == TokenType.Multiply || simpleTokens[index].type == TokenType.Divide) {
                    if (index == 0 || simpleTokens[index - 1].type != TokenType.Integer ||
                        index == simpleTokens.Count - 1 || simpleTokens[index + 1].type != TokenType.Integer) {
                        return Option<int>.Null;
                    }
                    if (simpleTokens[index].type == TokenType.Multiply) {
                        simpleTokens[index - 1].value = simpleTokens[index - 1].value * simpleTokens[index + 1].value;
                    }
                    else {
                        try {
                            simpleTokens[index - 1].value = Divide(simpleTokens[index - 1].value, simpleTokens[index + 1].value);
                        }
                        catch (DivideByZeroException) {
                            return Option<int>.Null;
                        }
                    }
                    simpleTokens.RemoveAt(index + 1);
                    simpleTokens.RemoveAt(index);
                }
                else {
                    ++index;
                }
            }
            // Evaluate Add and Subtract
            for (int index = 0; index < simpleTokens.Count;) {
                if (simpleTokens[index].type == TokenType.Add || simpleTokens[index].type == TokenType.Subtract) {
                    if (index == 0 || simpleTokens[index - 1].type != TokenType.Integer ||
                        index == simpleTokens.Count - 1 || simpleTokens[index + 1].type != TokenType.Integer) {
                        return Option<int>.Null;
                    }
                    if (simpleTokens[index].type == TokenType.Add) {
                        simpleTokens[index - 1].value = simpleTokens[index - 1].value + simpleTokens[index + 1].value;
                    }
                    else {
                        simpleTokens[index - 1].value = simpleTokens[index - 1].value - simpleTokens[index + 1].value;
                    }
                    simpleTokens.RemoveAt(index + 1);
                    simpleTokens.RemoveAt(index);
                }
                else {
                    ++index;
                }
            }
            // Check if we got one Number
            if (simpleTokens.Count != 1) {
                return Option<int>.Null;
            }
            else {
                return simpleTokens[0].value;
            }
        }
        /// <summary>
        /// Returns Evaluation of Tokens
        /// </summary>
        public static Option<int> EvaluateTokens(List<Token> tokens) {
            return EvaluateTokens(tokens, 0, tokens.Count);
        }



        // === Parse Console Color ===
        private static readonly string[] colorNames = new string[] {
            "Black", "DarkBlue", "DarkGreen", "DarkCyan", "DarkRed", "DarkMagenta", "DarkYellow", "Gray",
            "DarkGray", "Blue", "Green", "Cyan", "Red", "Magenta", "Yellow", "White"};
        public static string GetName(this ConsoleColor color) {
            return colorNames[(int)color];
        }

        /// <summary>
        /// Returns ConsoleColor for value, if there is one
        /// </summary>
        public static Option<ConsoleColor> GetConsoleColor(this string text) {
            var result = Option<ConsoleColor>.Null;
            for (int index = 0; index < colorNames.Length; ++index) {
                if (colorNames[index].StartsWith(text, StringComparison.InvariantCultureIgnoreCase)) {
                    if (result.HasValue) {
                        return Option<ConsoleColor>.Null;
                    }
                    else {
                        result = (ConsoleColor)index;
                    }
                }
            }
            return result;
        }

        // === Parse Int ===
        /// <summary>
        /// Returns not negative int for value
        /// </summary>
        public static Option<int> GetInt10(string text, int start, int end) {
            if(end == 0) {
                return Option<int>.Null;
            }
            int value = 0;
            for(int index = start; index < end; ++index) {
                if('0' <= text[index] && text[index] <= '9') {
                    value = 10 * value + text[index] - '0';
                }
                else {
                    return Option<int>.Null;
                }
            }
            return value;
        }
        public static Option<int> GetInt10(string text) {
            return GetInt10(text, 0, text.Length);
        }
        /// <summary>
        /// Returns not negative int for value
        /// </summary>
        public static int GetInt10_Unsafe(string text, int start, int end) {
            int value = 0;
            for(int index = start; index < end; ++index) {
                value = 10 * value + text[index] - '0';
            }
            return value;
        }

        // Parse === Bool ===
        public static Option<bool> GetBoolean(string text) {
            bool compTrue = "true".StartsWith(text, StringComparison.InvariantCultureIgnoreCase);
            bool compFalse = "false".StartsWith(text, StringComparison.InvariantCultureIgnoreCase);

            if(compTrue && !compFalse) {
                return true;
            }
            else if(compFalse && !compTrue) {
                return false;
            }
            else {
                return Option<bool>.Null;
            }
        }
    }
}