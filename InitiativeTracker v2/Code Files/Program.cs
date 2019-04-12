using System;
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

        public const int SleepDelta = 10;

        static void Main(string[] args) {
            Console.Title = "InitiativeTracker";
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.TreatControlCAsInput = true;
            Console.CancelKeyPress += CancelKeyPressHandler;
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

            // This is pretty much just for testing
            data.AddActor_Silent(data.loadedActors[0].CreateInstance(data.loadedColorings, out string error1));
            data.AddActor_Silent(data.loadedActors[0].CreateInstance(data.loadedColorings, out string error2));

            screenWriter.Write(Output.GetScreen());
            // Program Loop
            while (true) {
                if (Console.KeyAvailable) {
                    InputParser.Parse(Console.ReadKey(true));
                    screenWriter.Write(Output.GetScreen());
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
