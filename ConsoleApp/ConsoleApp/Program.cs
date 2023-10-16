namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Espeak.Initialize();

            if (args.Length == 0)
            {
                Nato();
            }
            else
            {
                Espeak.Speak(args[0]);
            }
        }

        private static void Nato()
        {
            NATO nato = new NATO();
            System.Reflection.FieldInfo[] fields = nato.GetType().GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                System.Reflection.FieldInfo fieldInfo = fields[i];
                Espeak.Speak(fieldInfo.Name);
                //string? readline = System.Console.ReadLine();
            }
        }
    }
}