namespace eSpeakNGLib
{
    public static class Espeak
    {
        public static string ZeroTerminator = "\0";

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
        public static System.IntPtr UniqueIdentifier = System.IntPtr.Zero;

        /// <summary>
        /// a pointer (or NULL) which will be passed to the callback function in
        /// espeak_EVENT messages.
        /// </summary>
        public static System.IntPtr UserData = System.IntPtr.Zero;

        public static EspeakVoice EspeakVoiceInput = new EspeakVoice();
        public static System.IntPtr VoiceSpec = System.IntPtr.Zero;

        public static System.IntPtr ListVoicesPtr = System.IntPtr.Zero;
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
            ListVoicesPtr = EspeakAPI.espeak_ListVoices(System.IntPtr.Zero);
            System.Console.WriteLine(nameof(ListVoicesPtr) + " " + ListVoicesPtr);

            object? pointerArray = System.Runtime.InteropServices.Marshal.PtrToStructure(ListVoicesPtr, typeof(ListVoicesPointerArray));
            if (pointerArray != null)
            {
                ListVoicesPointerArray listVoices = (ListVoicesPointerArray)pointerArray;
                System.Console.WriteLine(nameof(listVoices) + " " + listVoices.PointerArray.Length);

                for (int i = 0; i < listVoices.PointerArray.Length; i++)
                {
                    System.IntPtr voicePtr = listVoices.PointerArray[i];
                    object? voiceObj = System.Runtime.InteropServices.Marshal.PtrToStructure(voicePtr, typeof(EspeakVoice));
                    if (voiceObj != null)
                    {
                        EspeakVoice voice = (EspeakVoice)voiceObj;
                        ListVoices.Add(voice);
                    }
                }
            }
        }

        public static void PrintListVoices()
        {
            for (int i = 0; i < ListVoices.Count; i++)
            {
                EspeakVoice voice = ListVoices[i];
                System.Console.WriteLine(i + " " + voice.ToString());
            }
        }

        public static void SetVoiceByName(int id)
        {
            string name = ListVoices[id].Name;
            Espeak_ERROR setVoiceByName = (Espeak_ERROR)EspeakAPI.espeak_SetVoiceByName(name);
            System.Console.WriteLine(nameof(setVoiceByName) + " " + setVoiceByName);
        }

        public static void SetVoiceByProperties(string name, byte gender, byte age)
        {
            EspeakVoiceInput = new EspeakVoice();
            EspeakVoiceInput.Name = name;
            EspeakVoiceInput.Languages = string.Empty;
            EspeakVoiceInput.Identifier = string.Empty;
            EspeakVoiceInput.Gender = gender;
            EspeakVoiceInput.Age = age;
            EspeakVoiceInput.Variant = 0;
            System.Console.WriteLine(nameof(EspeakVoiceInput) + " " + EspeakVoiceInput.ToString());

            if (VoiceSpec != System.IntPtr.Zero)
            {
                System.Runtime.InteropServices.Marshal.FreeHGlobal(VoiceSpec);
                VoiceSpec = System.IntPtr.Zero;
            }
            int size = System.Runtime.InteropServices.Marshal.SizeOf(EspeakVoiceInput);
            VoiceSpec = System.Runtime.InteropServices.Marshal.AllocHGlobal(size);
            System.Runtime.InteropServices.Marshal.StructureToPtr(EspeakVoiceInput, VoiceSpec, false);

            Espeak_ERROR setVoiceByProperties = (Espeak_ERROR)EspeakAPI.espeak_SetVoiceByProperties(VoiceSpec);
            System.Console.WriteLine(nameof(setVoiceByProperties) + " " + setVoiceByProperties.ToString());

            GetCurrentVoice();
        }

        public static void Synth(string tts)
        {
            string text = tts + ZeroTerminator;

            // Equal to (or greatrer than) the size of the text data, in bytes.  
            int utf8ByteCount = System.Text.ASCIIEncoding.UTF8.GetByteCount(text);
            //int sysCharSize = text.Length * System.Runtime.InteropServices.Marshal.SystemDefaultCharSize;
            //System.Console.WriteLine(nameof(utf8ByteCount) + " " + utf8ByteCount + " " + nameof(sysCharSize) + " " + sysCharSize);

            int Synth = EspeakAPI.espeak_Synth(text, utf8ByteCount, 0, 0, 0, 0, UniqueIdentifier, UserData);
            System.Console.WriteLine(nameof(EspeakAPI.espeak_Synth) + " " + Synth + " " + text);
        }

        public static void GetCurrentVoice()
        {
            System.IntPtr getCurrentVoice = EspeakAPI.espeak_GetCurrentVoice();
            EspeakVoice currentVoice = (EspeakVoice)System.Runtime.InteropServices.Marshal.PtrToStructure(getCurrentVoice, typeof(EspeakVoice));
            System.Console.WriteLine(nameof(currentVoice) + " " + currentVoice.ToString());
        }
    }
}
