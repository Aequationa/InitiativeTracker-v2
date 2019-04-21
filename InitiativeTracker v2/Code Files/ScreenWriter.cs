using System;
using System.Collections.Generic;

namespace InitiativeTracker
{
    public struct Character
    {
        public char c;
        public ConsoleColor foreground;
        public ConsoleColor background;

        public Character(char c, ConsoleColor foreground, ConsoleColor background) {
            this.c = c;
            this.foreground = foreground;
            this.background = background;
        }
        
        /// <summary>
        /// Note: A Solid is always the Color of its background
        /// </summary>
        public bool IsSolid() {
            return foreground == background || c == ' ';
        }
        public bool IsSolid(ConsoleColor color) {
            return IsSolid() && background == color;
        }
        /// <summary>
        /// Returns if Characters will Produce the same Console Output
        /// </summary>
        public bool IsIndistinguishable(Character other) {
            return background == other.background && ((foreground == other.foreground && c == other.c) || (IsSolid() && other.IsSolid()));
        }
    }

    public class Screen
    {
        private Character[,] data;

        public static Screen MonoColor(int width, int height, ConsoleColor color) {
            Screen screen = new Screen();
            screen.data = new Character[width, height];
            for (int y = 0; y < height; ++y) {
                for (int x = 0; x < width; ++x) {
                    screen.data[x, y].c = ' ';
                    screen.data[x, y].foreground = color;
                    screen.data[x, y].background = color;
                }
            }
            return screen;
        }
        public static Screen Default(int width, int height) {
            return MonoColor(width, height, ConsoleColor.Black);
        }
        
        public Character this[int x, int y] {
            get { return data[x, y]; }
            set { data[x, y] = value; }
        }
        public int Width { get { return data.GetLength(0); } }
        public int Height { get { return data.GetLength(1); } }

        public void SetC(int x, int y, char c) {
            data[x, y].c = c;
        }
        public void SetForeground(int x, int y, ConsoleColor foreground) {
            data[x, y].foreground = foreground;
        }
        public void SetBackground(int x, int y, ConsoleColor background) {
            data[x, y].background = background;
        }
        
        /// <summary>
        /// Adds the Formatted Line
        /// </summary>
        public void AddFormattedLine(string text, ConsoleColor foreground, ConsoleColor background, int left, int right, int top) {
            if(top < 0 || top >= Height) {
                return;
            }
            int min = Math.Max(left, 0);
            int max = Math.Min(right, Width);
            int textMax = Math.Min(max, left + text.Length);

            for(int pos = min; pos < textMax; ++pos) {
                data[pos, top].c = text[pos - left];
                data[pos, top].foreground = foreground;
                data[pos, top].background = background;
            }
        }
        /// <summary>
        /// Adds the Formatted Line, filling with Blanks if nessesary
        /// </summary>
        public void AddFormattedFullLine(string text, ConsoleColor foreground, ConsoleColor background, int left, int right, int top) {
            if (top < 0 || top >= Height) {
                return;
            }
            int min = Math.Max(left, 0);
            int max = Math.Min(right, Width);
            int textMax = Math.Min(max, left + text.Length);

            for (int pos = min; pos < textMax; ++pos) {
                data[pos, top].c = text[pos - left];
                data[pos, top].foreground = foreground;
                data[pos, top].background = background;
            }
            for (int pos = textMax; pos < max; ++pos) {
                data[pos, top].c = ' ';
                data[pos, top].foreground = foreground;
                data[pos, top].background = background;
            }
        }
        /// <summary>
        /// Adds the formatted Lines, returning their height
        /// </summary>
        public int AddFormattedLines(List<string> text, ConsoleColor foreground, ConsoleColor background, int left, int right, int top) {
            int width = Width;
            int height = Height;
            int availableWidth = right - left;
            if (availableWidth <= 0) {
                return 0;
            }

            int y = top;
            for(int lineIndex = 0; lineIndex < text.Count; ++lineIndex) {
                string[] words = text[lineIndex].Split(' ');
                int usedWidth = 0;
                for(int wordIndex = 0; wordIndex < words.Length; ++wordIndex) {
                    if(wordIndex == 0 || usedWidth + 1 + words[wordIndex].Length <= availableWidth) {
                        // Write in same Line
                        if(wordIndex != 0) {
                            // Start with Space
                            int x = left + usedWidth;
                            if (x >= 0 && x < width && y >= 0 && y < height) {
                                data[x, y].c = ' ';
                                data[x, y].foreground = foreground;
                                data[x, y].background = background;
                            }
                            ++usedWidth;
                        }
                        for(int index = 0; index < words[wordIndex].Length; ++index) {
                            int x = left + usedWidth + index;
                            if (x >= 0 && x < width && y >= 0 && y < height) {
                                data[x, y].c = words[wordIndex][index];
                                data[x, y].foreground = foreground;
                                data[x, y].background = background;
                            }
                        }
                        usedWidth += words[wordIndex].Length;
                    }
                    else {
                        // Write in new Line
                        ++y;
                        for (int index = 0; index < words[wordIndex].Length; ++index) {
                            int x = left + index;
                            if (x >= 0 && x < width && y >= 0 && y < height) {
                                data[x, y].c = words[wordIndex][index];
                                data[x, y].foreground = foreground;
                                data[x, y].background = background;
                            }
                        }
                        usedWidth = words[wordIndex].Length;
                    }
                }
                ++y;
            }
            return y - top;
        }
    }

    public class ScreenWriter
    {
        private Screen old;

        public ScreenWriter() {
            old = null;
        }
        
        /// <summary>
        /// Clears Console Black, then Writes all Lines. Leaves the Cursor Invisible at (0, 0)
        /// </summary>
        private void Write_New(Screen screen) {
            try {
                Console.CursorVisible = false;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
            }
            catch (Exception) { }

            if (Program.settings.resizeConsole) {
                try {
                    Console.WindowWidth = screen.Width;
                    Console.WindowHeight = screen.Height;
                }
                catch (Exception) { }
            }
            if (Program.settings.resizeBuffer) {
                try {
                    Console.BufferWidth = screen.Width;
                    Console.BufferHeight = screen.Height;
                }
                catch (Exception) { }
            }

            int width = screen.Width;
            int height = screen.Height;

            for(int y = 0; y < height; ++y) {
                ConsoleColor foreground = ConsoleColor.Black;
                ConsoleColor background = ConsoleColor.Black;
                bool found = false;
                int start = 0;
                int end = 0;

                for (int x = 0; x < width; ++x) {
                    if (!found) {
                        // Check if we need to Start
                        if (screen[x, y].IsSolid(ConsoleColor.Black)) {
                            // Do Nothing
                        }
                        else {
                            // Start
                            foreground = screen[x, y].foreground;
                            background = screen[x, y].background;
                            found = true;
                            start = x;
                            end = x;
                        }
                    }
                    else {
                        // Check if we may continue
                        if (screen[x, y].background == background && (screen[x, y].IsSolid() || (screen[x, y].foreground == foreground))) {
                            // We may continue; check if this Character needs to be written
                            if (!screen[x, y].IsSolid(ConsoleColor.Black)) {
                                end = x;
                            }
                        }
                        else {
                            // Write
                            char[] text = new char[end + 1 - start];
                            for (int index = start; index <= end; ++index) {
                                if (screen[index, y].IsSolid(background)) {
                                    text[index - start] = ' ';
                                }
                                else {
                                    text[index - start] = screen[index, y].c;
                                }
                            }
                            try {
                                Console.SetCursorPosition(start, y);
                                Console.ForegroundColor = foreground;
                                Console.BackgroundColor = background;
                                Console.Write(text);
                            }
                            catch (Exception) { }

                            // Check if we need to Start anew
                            if (screen[x, y].IsSolid(ConsoleColor.Black)) {
                                // No new Start
                                found = false;
                            }
                            else {
                                // New Start
                                foreground = screen[x, y].foreground;
                                background = screen[x, y].background;
                                start = x;
                                end = x;
                            }
                        }
                    }
                }
                // Last Draw
                if (found) {
                    char[] text = new char[end + 1 - start];
                    for (int index = start; index <= end; ++index) {
                        if (screen[index, y].IsSolid(background)) {
                            text[index - start] = ' ';
                        }
                        else {
                            text[index - start] = screen[index, y].c;
                        }
                    }
                    try {
                        Console.SetCursorPosition(start, y);
                        Console.ForegroundColor = foreground;
                        Console.BackgroundColor = background;
                        Console.Write(text);
                    }
                    catch (Exception) { }
                }
            }
            try {
                Console.SetCursorPosition(0, 0);
            }
            catch (Exception) { }
        }
        /// <summary>
        /// Writes only Difference from last Screen. Leaves the Cursor Invisible at (0, 0)
        /// </summary>
        /// <param name="screen"></param>
        private void Write_Overwrite(Screen screen) {
            try {
                Console.CursorVisible = false;
            }
            catch (Exception) { }

            int width = screen.Width;
            int height = screen.Height;

            for(int y = 0; y < height; ++y) {
                ConsoleColor foreground = ConsoleColor.Black;
                ConsoleColor background = ConsoleColor.Black;
                bool found = false;
                int start = 0;
                int end = 0;

                for (int x = 0; x < width; ++x) {
                    if (!found) {
                        // Check if we need to Start
                        if (screen[x, y].IsIndistinguishable(old[x, y])) {
                            // Do Nothing
                        }
                        else {
                            // Start
                            foreground = screen[x, y].foreground;
                            background = screen[x, y].background;
                            found = true;
                            start = x;
                            end = x;
                        }
                    }
                    else {
                        // Check if we may continue
                        if(screen[x, y].background == background && (screen[x, y].IsSolid() || (screen[x, y].foreground == foreground))) {
                            // We may continue; check if this Character needs to be written
                            if (!screen[x, y].IsIndistinguishable(old[x, y])) {
                                end = x;
                            }
                        }
                        else {
                            // Write
                            char[] text = new char[end + 1 - start];
                            for (int index = start; index <= end; ++index) {
                                if (screen[index, y].IsSolid(background)) {
                                    text[index - start] = ' ';
                                }
                                else {
                                    text[index - start] = screen[index, y].c;
                                }
                            }
                            try {
                                Console.SetCursorPosition(start, y);
                                Console.ForegroundColor = foreground;
                                Console.BackgroundColor = background;
                                Console.Write(text);
                            }
                            catch (Exception) { }

                            // Check if we need to Start anew
                            if (screen[x, y].IsIndistinguishable(old[x, y])) {
                                // No new Start
                                found = false;
                            }
                            else {
                                // New Start
                                foreground = screen[x, y].foreground;
                                background = screen[x, y].background;
                                start = x;
                                end = x;
                            }
                        }
                    }
                }
                // Last Draw
                if (found) {
                    char[] text = new char[end + 1 - start];
                    for (int index = start; index <= end; ++index) {
                        if (screen[index, y].IsSolid(background)) {
                            text[index - start] = ' ';
                        }
                        else {
                            text[index - start] = screen[index, y].c;
                        }
                    }
                    try {
                        Console.SetCursorPosition(start, y);
                        Console.ForegroundColor = foreground;
                        Console.BackgroundColor = background;
                        Console.Write(text);
                    }
                    catch (Exception) { }
                }
            }
            try {
                Console.SetCursorPosition(0, 0);
            }
            catch (Exception) { }
        }

        private int OperationCount_New(Screen screen) {
            // Note: This Code is a modified Version of the corresponding Push Function
            int count = 0;
            int width = screen.Width;
            int height = screen.Height;

            for (int y = 0; y < height; ++y) {
                ConsoleColor foreground = ConsoleColor.Black;
                ConsoleColor background = ConsoleColor.Black;
                bool found = false;

                for (int x = 0; x < width; ++x) {
                    if (!found) {
                        // Check if we need to Start
                        if (screen[x, y].IsSolid(ConsoleColor.Black)) {
                            // Do Nothing
                        }
                        else {
                            // Start
                            foreground = screen[x, y].foreground;
                            background = screen[x, y].background;
                            found = true;
                        }
                    }
                    else {
                        // Check if we may continue
                        if (screen[x, y].background == background && (screen[x, y].IsSolid() || (screen[x, y].foreground == foreground))) {
                            // We may continue
                        }
                        else {
                            // Write
                            ++count;

                            // Check if we need to Start anew
                            if (screen[x, y].IsSolid(ConsoleColor.Black)) {
                                // No new Start
                                found = false;
                            }
                            else {
                                // New Start
                                foreground = screen[x, y].foreground;
                                background = screen[x, y].background;
                            }
                        }
                    }
                }
                // Last Draw
                if (found) {
                    ++count;
                }
            }
            return count;
        }
        private int OperationCount_Overwrite(Screen screen) {
            // Note: This Code is a modified Version of the corresponding Push Function
            int count = 0;
            int width = screen.Width;
            int height = screen.Height;

            for (int y = 0; y < height; ++y) {
                ConsoleColor foreground = ConsoleColor.Black;
                ConsoleColor background = ConsoleColor.Black;
                bool found = false;

                for (int x = 0; x < width; ++x) {
                    if (!found) {
                        // Check if we need to Start
                        if (screen[x, y].IsIndistinguishable(old[x, y])) {
                            // Do Nothing
                        }
                        else {
                            // Start
                            foreground = screen[x, y].foreground;
                            background = screen[x, y].background;
                            found = true;
                        }
                    }
                    else {
                        // Check if we may continue
                        if (screen[x, y].background == background && (screen[x, y].IsSolid() || (screen[x, y].foreground == foreground))) {
                            // We may continue
                        }
                        else {
                            // Write
                            ++count;

                            // Check if we need to Start anew
                            if (screen[x, y].IsIndistinguishable(old[x, y])) {
                                // No new Start
                                found = false;
                            }
                            else {
                                // New Start
                                foreground = screen[x, y].foreground;
                                background = screen[x, y].background;
                            }
                        }
                    }
                }
                // Last Draw
                if (found) {
                    ++count;
                }
            }
            return count;
        }
        private const int clearCost = 10;

        public void Write(Screen screen, bool forceNew = false) {
            int width = screen.Width;
            int height = screen.Height;
            //Remove bottom right Entry if Visible
            if (width > 0 && height > 0 && !screen[width - 1, height - 1].IsSolid()) {
                screen.SetC(width - 1, height - 1, ' ');
            }

            if (forceNew || old == null || width != old.Width || height != old.Height) {
                Write_New(screen);
            }
            else {
                if(Console.CursorLeft != 0 || Console.CursorTop != 0) {
                    //Cursor has been moved, mark all Entries that we assume to be corrupted
                    try {
                        int cursorX = Console.CursorLeft;
                        int cursorY = Console.CursorTop;

                        for (int y = 0; y < cursorY && y < height; ++y) {
                            for (int x = 0; x < width; ++x) {
                                old.SetC(x, y, '\u0007');
                            }
                        }
                        if (Console.CursorTop < height) {
                            for (int x = 0; x < cursorX; ++x) {
                                old.SetC(x, cursorY, '\u0007');
                            }
                        }
                    }
                    catch (Exception) { }
                }

                if(OperationCount_New(screen) >= OperationCount_Overwrite(screen) + clearCost) {
                    Write_Overwrite(screen);
                }
                else {
                    Write_New(screen);
                }
            }
            old = screen;
        }
    }
}