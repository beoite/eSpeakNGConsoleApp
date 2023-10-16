namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("");
            //Espeak.Initialize(espeak_AUDIO_OUTPUT.AUDIO_OUTPUT_SYNCH_PLAYBACK, Phrases.Alphabet);
            //Espeak.Speak("Zen 2 is a computer processor microarchitecture by AMD. It is the successor of AMD's Zen and Zen+ microarchitectures, and is fabricated on the 7nm MOSFET node from TSMC.");

            Espeak.Initialize();

            Nato();
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