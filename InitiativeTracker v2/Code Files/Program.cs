﻿using System;
using System.Threading;

namespace InitiativeTracker
{
    class Program
    {
        public static ScreenWriter screenWriter = new ScreenWriter();
        public static Random random = new Random();
        public static Settings settings;
        public static Data data = new Data();
        public static OutputData outputData = new OutputData();
        public static SpecialCharacters specialCharacters = new SpecialCharacters();

        public const int SleepDelta = 10;

        static void Main(string[] args) {
            try {
                Console.Title = "InitiativeTracker";
            }
            catch (Exception) { }
            try {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
            }
            catch (Exception) { }
            try {
                Console.TreatControlCAsInput = true;
            }
            catch (Exception) { }
            try {
                Console.CancelKeyPress += CancelKeyPressHandler;
            }
            catch (Exception) { }
            
            bool success = FileParser.Load(true);
            // Proceed on Button Press
            while (!Console.KeyAvailable) {
                Thread.Sleep(SleepDelta);
            }
            if (!success) {
                Environment.Exit(0);
            }
            Console.ReadKey(true);
            outputData.Setup();

            screenWriter.Write(Output.GetScreen());
            // Program Loop
            while (true) {
                if (Console.KeyAvailable) {
                    InputParser.Parse(Console.ReadKey(true));
                }
                else {
                    Thread.Sleep(SleepDelta);
                }
            }
        }

        private static void CancelKeyPressHandler(object sender, ConsoleCancelEventArgs e) {
            Environment.Exit(0);
        }
    }
}
