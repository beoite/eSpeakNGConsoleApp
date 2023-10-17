namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            //eSpeakNGLib.Espeak.Initialize();
            Initialize();

            if (args.Length == 0)
            {
                while (System.Console.ReadLine() != null)
                {
                    SetVoice();
                    Nato();
                }
            }
            else
            {
                eSpeakNGLib.Espeak.Synth(args[0]);
            }
        }

        private static void Initialize()
        {
            eSpeakNGLib.Espeak.SetExecutionDirectory();
            System.Console.WriteLine(nameof(eSpeakNGLib.Espeak.ExecutionDirectory) + " " + eSpeakNGLib.Espeak.ExecutionDirectory);

            eSpeakNGLib.Espeak.AddRegistryEntry();
            System.Console.WriteLine(nameof(eSpeakNGLib.Espeak.AddRegistryEntry) + " " + eSpeakNGLib.Espeak.Result);

            eSpeakNGLib.Espeak.SampleRateHz = eSpeakNGLib.EspeakAPI.espeak_Initialize((int)eSpeakNGLib.Espeak.AudioOutput, eSpeakNGLib.Espeak.BufLength, eSpeakNGLib.Espeak.ExecutionDirectory, eSpeakNGLib.Espeak.Options);
            System.Console.WriteLine(nameof(eSpeakNGLib.Espeak.SampleRateHz) + " " + eSpeakNGLib.Espeak.SampleRateHz);

            eSpeakNGLib.Espeak.BuildListVoices();
            System.Console.WriteLine(nameof(eSpeakNGLib.Espeak.ListVoicesPtr) + " " + eSpeakNGLib.Espeak.ListVoicesPtr);
            System.Console.WriteLine(nameof(eSpeakNGLib.Espeak.ListVoices) + " " + eSpeakNGLib.Espeak.ListVoices.Count);
        }

        private static void PrintListVoices()
        {
            for (int i = 0; i < eSpeakNGLib.Espeak.ListVoices.Count; i++)
            {
                eSpeakNGLib.EspeakVoice voice = eSpeakNGLib.Espeak.ListVoices[i];
                System.Console.WriteLine(i + " " + voice.ToString());
            }
        }

        private static void SetVoice()
        {
            int id = eSpeakNGLib.Config.RandomEnglish();
            string name = eSpeakNGLib.Espeak.ListVoices[id].Name;
            byte gender = (byte)System.Random.Shared.Next(1, 3);
            byte age = (byte)System.Random.Shared.Next(1, 100);
            eSpeakNGLib.Espeak.SetVoiceByProperties(name, gender, age);
            System.Console.WriteLine(nameof(eSpeakNGLib.Espeak.EspeakVoiceInput) + " " + eSpeakNGLib.Espeak.EspeakVoiceInput);
            System.Console.WriteLine(nameof(eSpeakNGLib.Espeak.SetVoiceByProperties) + " " + eSpeakNGLib.Espeak.Result);
            eSpeakNGLib.Espeak.GetCurrentVoice();
            System.Console.WriteLine(nameof(eSpeakNGLib.Espeak.GetCurrentVoice) + " " + eSpeakNGLib.Espeak.EspeakVoiceOutput);
        }

        private static void Nato()
        {
            eSpeakNGLib.NATO nato = new eSpeakNGLib.NATO();
            System.Reflection.FieldInfo[] fields = nato.GetType().GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                System.Reflection.FieldInfo fieldInfo = fields[i];
                eSpeakNGLib.Espeak.Synth(fieldInfo.Name);
                System.Console.WriteLine(nameof(eSpeakNGLib.Espeak.Synth) + " " + eSpeakNGLib.Espeak.Result);
            }
        }
    }
}