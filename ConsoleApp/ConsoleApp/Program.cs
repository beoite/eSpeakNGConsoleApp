namespace ConsoleApp
{
    public class Program
    {
        // test
        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            eSpeakNGLib.Espeak.Initialize();

            if (args.Length == 0)
            {
                while (System.Console.ReadLine() != null)
                {
                    Loop();
                }
            }
            else
            {
                eSpeakNGLib.Espeak.Synth(args[0]);
            }
        }

        private static void Loop()
        {
            int id = eSpeakNGLib.Config.RandomEnglish();
            string name = eSpeakNGLib.Espeak.ListVoices[id].Name + eSpeakNGLib.Espeak.ZeroTerminator;
            byte gender = 2;
            byte age = 0x53;
            eSpeakNGLib.Espeak.SetVoiceByProperties(name, gender, age);
            Nato();
        }

        private static void Nato()
        {
            eSpeakNGLib.NATO nato = new eSpeakNGLib.NATO();
            System.Reflection.FieldInfo[] fields = nato.GetType().GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                System.Reflection.FieldInfo fieldInfo = fields[i];
                eSpeakNGLib.Espeak.Synth(fieldInfo.Name);
            }
        }
    }
}