using System.Linq.Expressions;

namespace ConsoleApp
{
    public class Program
    {
        // test
        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            Espeak.Initialize();

            if (args.Length == 0)
            {
                while (System.Console.ReadLine() != null)
                {
                    Loop();
                }
            }
            else
            {
                Espeak.Synth(args[0]);
            }
        }

        private static void Loop()
        {
            int id = Config.RandomEnglish();
            string name = Espeak.ListVoices[id].Name + Espeak.ZeroTerminator;
            byte gender = 2;
            byte age = 0x53;
            Espeak.SetVoiceByProperties(name, gender, age);
            Nato();
        }

        private static void Nato()
        {
            NATO nato = new NATO();
            System.Reflection.FieldInfo[] fields = nato.GetType().GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                System.Reflection.FieldInfo fieldInfo = fields[i];
                Espeak.Synth(fieldInfo.Name);
            }
        }
    }
}