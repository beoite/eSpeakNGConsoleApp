namespace ConsoleApp
{
    public static class Espeak
    {
        /// <summary>
        /// where to look for libespeak-ng.dll, espeak-ng-data
        /// </summary>
        public static string ExecutionDirectory = System.String.Empty;

        /// <summary>
        /// sample rate in Hz, or -1 (EE_INTERNAL_ERROR).
        /// </summary>
        public static int SampleRateHz = -1;

        /// <summary>
        /// <br>
        /// PLAYBACK mode: plays the audio data, supplies events to the calling program 
        /// AUDIO_OUTPUT_PLAYBACK,
        /// </br><br>
        /// RETRIEVAL mode: supplies audio data and events to the calling program  
        /// AUDIO_OUTPUT_RETRIEVAL,
        /// </br><br>
        /// SYNCHRONOUS mode: as RETRIEVAL but doesn't return until synthesis is completed  
        /// AUDIO_OUTPUT_SYNCHRONOUS,
        /// </br><br>
        /// Synchronous playback  
        /// AUDIO_OUTPUT_SYNCH_PLAYBACK
        /// </br>
        /// </summary>
        public static Espeak_AUDIO_OUTPUT AudioOutput = Espeak_AUDIO_OUTPUT.AUDIO_OUTPUT_PLAYBACK;

        /// <summary>
        /// The length in mS of sound buffers passed to the SynthCallback function.
        /// Value=0 gives a default of 60mS.
        /// This parameter is only used for AUDIO_OUTPUT_RETRIEVAL and AUDIO_OUTPUT_SYNCHRONOUS modes.
        /// </summary>
        public static int BufLength = 1000;

        /// <summary>
        /// bit 0:  1 = allow espeakEVENT_PHONEME events.
        /// bit 1:  1 = espeakEVENT_PHONEME events give IPA phoneme names, not eSpeak phoneme names
        /// bit 15: 1 = don't exit if espeak_data is not found (used for --help) 
        /// </summary>
        public static int Options = 0b00000000_00000000;

        /// <summary>
        /// This must be either NULL, or point to an integer variable to
        /// which eSpeak writes a message identifier number.
        /// eSpeak includes this number in espeak_EVENT messages which are the result of
        /// this call of espeak_Synth().
        /// </summary>
        public static System.IntPtr UniqueIdentifier = new System.IntPtr();

        /// <summary>
        /// a pointer (or NULL) which will be passed to the callback function in
        /// espeak_EVENT messages.
        /// </summary>
        public static System.IntPtr UserData = new System.IntPtr();

        public static System.Collections.Generic.List<EspeakVoice> ListVoices = new System.Collections.Generic.List<EspeakVoice>();

        public static void Initialize()
        {
            // get executing path
            string Location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            ExecutionDirectory = System.IO.Path.GetDirectoryName(Location) + @"\";
            System.Console.WriteLine("ExecutionDirectory : " + ExecutionDirectory);

            // add registry entry to path
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("Software\\Wow6432Node\\eSpeak NG");
                key.SetValue("Path", ExecutionDirectory);
                key.Close();
            }

            // espeak_Initialize, Returns: sample rate in Hz, or -1 (EE_INTERNAL_ERROR).
            SampleRateHz = EspeakAPI.espeak_Initialize((int)AudioOutput, BufLength, ExecutionDirectory, Options);
            System.Console.WriteLine(nameof(EspeakAPI.espeak_Initialize) + " " + nameof(SampleRateHz) + " " + SampleRateHz);

            // espeak_ListVoices - array of espeak_VOICE pointers
            System.IntPtr listVoices = EspeakAPI.espeak_ListVoices(new System.IntPtr());
            System.Console.WriteLine(nameof(listVoices) + " " + listVoices);

            ListVoicesPointerArray arrlistVoices = (ListVoicesPointerArray)System.Runtime.InteropServices.Marshal.PtrToStructure(listVoices, typeof(ListVoicesPointerArray));
            System.Console.WriteLine(nameof(arrlistVoices) + " " + arrlistVoices.PointerArray.Length);

            for (int i = 0; i < arrlistVoices.PointerArray.Length; i++)
            {
                System.IntPtr voicePtr = arrlistVoices.PointerArray[i];
                EspeakVoice voice = (EspeakVoice)System.Runtime.InteropServices.Marshal.PtrToStructure(voicePtr, typeof(EspeakVoice));
                ListVoices.Add(voice);
            }

            //for (int i = 0; i < ListVoices.Count; i++)
            //{
            //    EspeakVoice voice = ListVoices[i];
            //    System.Console.WriteLine(i + " " + voice.Name);
            //}

            GetVoice();

            string name = ListVoices[Config.VoiceID].Name;
            SetVoiceByName(name);
            GetVoice();

            SetVoiceByProperties();
            GetVoice();
        }

        private static void SetVoiceByName(string name)
        {
            Espeak_ERROR setVoiceByName = (Espeak_ERROR)EspeakAPI.espeak_SetVoiceByName(name);
            System.Console.WriteLine(nameof(setVoiceByName) + " " + setVoiceByName);
        }

        private static void SetVoiceByProperties()
        {
            string Name = ListVoices[Config.VoiceID].Name;
            string Languages = System.String.Empty;
            string Identifier = System.String.Empty;

            EspeakVoice EspeakVoiceIn = new EspeakVoice();
            EspeakVoiceIn = new EspeakVoice();
            EspeakVoiceIn.Name = Name;
            EspeakVoiceIn.Languages = Languages;
            EspeakVoiceIn.Identifier = Identifier;
            EspeakVoiceIn.Gender = (uint)System.Math.Round(System.Random.Shared.NextDouble() + 1, 0);
            EspeakVoiceIn.Age = (uint)System.Math.Round(System.Random.Shared.NextDouble() * 100, 0);
            EspeakVoiceIn.Variant = 0;
            System.Console.WriteLine(nameof(EspeakVoiceIn) + " " + EspeakVoiceIn.ToString());

            //int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(EspeakVoice));
            //EspeakVoiceInPtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(size);
            //System.Runtime.InteropServices.Marshal.StructureToPtr(EspeakVoiceIn, EspeakVoiceInPtr, true);

            //Espeak_ERROR SetVoiceByProperties = (Espeak_ERROR)EspeakAPI.espeak_SetVoiceByProperties(EspeakVoiceInPtr);
            //System.Console.WriteLine(nameof(SetVoiceByProperties) + " " + SetVoiceByProperties.ToString());
        }

        public static void Speak(string tts)
        {
            string zeroTerminator = "\0";
            string text = tts + zeroTerminator;

            // Equal to (or greatrer than) the size of the text data, in bytes.  
            int utf8ByteCount = System.Text.ASCIIEncoding.UTF8.GetByteCount(text);
            //int sysCharSize = text.Length * System.Runtime.InteropServices.Marshal.SystemDefaultCharSize;
            //System.Console.WriteLine(nameof(utf8ByteCount) + " " + utf8ByteCount + " " + nameof(sysCharSize) + " " + sysCharSize);

            int Synth = EspeakAPI.espeak_Synth(text, utf8ByteCount, 0, 0, 0, 0, UniqueIdentifier, UserData);
            System.Console.WriteLine(nameof(EspeakAPI.espeak_Synth) + " " + Synth + " " + text);
        }

        private static void GetVoice()
        {
            System.IntPtr getCurrentVoice = EspeakAPI.espeak_GetCurrentVoice();
            EspeakVoice currentVoice = (EspeakVoice)System.Runtime.InteropServices.Marshal.PtrToStructure(getCurrentVoice, typeof(EspeakVoice));
            System.Console.WriteLine(nameof(currentVoice) + " " + currentVoice.ToString());
        }
    }
}
