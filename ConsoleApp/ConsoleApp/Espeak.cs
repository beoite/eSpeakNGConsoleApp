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

        public static EspeakVoiceIN EspeakVoiceIn = new EspeakVoiceIN();
        public static System.IntPtr EspeakVoiceInPtr = new System.IntPtr();
        public static System.IntPtr EspeakTextPtr = new System.IntPtr();

        // Afrikaans
        // English
        public static string Name = "English";
        // NULL, or a single language string (with optional dialect), eg. "en-uk", or "en"
        public static string Languages = System.String.Empty;
        public static string Identifier = System.String.Empty;

        public static System.IntPtr NamePtr = new System.IntPtr();
        public static System.IntPtr LanguagesPtr = new System.IntPtr();
        public static System.IntPtr IdentifierPtr = new System.IntPtr();

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

            System.Console.WriteLine("Represents the largest possible value of System.IntPtr.         " + System.IntPtr.MaxValue);
            System.Console.WriteLine("Represents the smallest possible value of System.IntPtr.        " + System.IntPtr.MinValue);
            System.Console.WriteLine("Size of a pointer or handle in this process, measured in bytes. " + System.IntPtr.Size);

            // espeak_Initialize, Returns: sample rate in Hz, or -1 (EE_INTERNAL_ERROR).
            SampleRateHz = EspeakAPI.espeak_Initialize((int)AudioOutput, BufLength, ExecutionDirectory, Options);
            System.Console.WriteLine(nameof(EspeakAPI.espeak_Initialize) + " " + nameof(SampleRateHz) + " " + SampleRateHz);

            // espeak_ListVoices
            System.IntPtr ListVoicesPtr = EspeakAPI.espeak_ListVoices(new System.IntPtr());
            System.Console.WriteLine(nameof(ListVoicesPtr) + " " + ListVoicesPtr);

            // pointer to array
            System.IntPtr ListVoicesPtrPtr = (System.IntPtr)System.Runtime.InteropServices.Marshal.PtrToStructure(ListVoicesPtr, typeof(System.IntPtr));
            System.Console.WriteLine(nameof(ListVoicesPtrPtr) + " " + ListVoicesPtrPtr);

            int EspeakVoiceOUTBytesSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(EspeakVoiceOUT));
            System.Console.WriteLine(nameof(EspeakVoiceOUTBytesSize) + " " + EspeakVoiceOUTBytesSize);

            System.Collections.Generic.List<EspeakVoiceOUT> voices = new System.Collections.Generic.List<EspeakVoiceOUT>();
            int index = 0;
            while (index >= 0)
            {
                System.Console.WriteLine("");

                int indexAddress = index * EspeakVoiceOUTBytesSize;
                System.Console.WriteLine(nameof(indexAddress) + " " + indexAddress);

                long longAddress = ListVoicesPtrPtr.ToInt64() + indexAddress;
                System.Console.WriteLine(nameof(longAddress) + " " + longAddress);

                string hexAddress = longAddress.ToString("x");
                System.Console.WriteLine(nameof(hexAddress) + " " + hexAddress);

                System.IntPtr addressPtr = new System.IntPtr(longAddress);
                System.Console.WriteLine(nameof(addressPtr) + " " + addressPtr);

                //EspeakVoiceOUT test = (EspeakVoiceOUT)System.Runtime.InteropServices.Marshal.PtrToStructure(addressPtr, typeof(EspeakVoiceOUT));
                //System.Console.WriteLine(nameof(test) + " " + test);
                //voices.Add(test);

                index++;

                if (index > 10)
                {
                    index = -1;
                }
            }

            System.Console.WriteLine(nameof(voices) + " " + voices.Count);

            //System.IntPtr isNullPtr = ListVoicesPtrPtr.ToPointer() == new System.IntPtr();
            //if (isNullPtr == new System.IntPtr())
            //{
            //    EspeakVoiceOUT test = (EspeakVoiceOUT)System.Runtime.InteropServices.Marshal.PtrToStructure(ListVoicesPtrPtr, typeof(EspeakVoiceOUT));
            //    Console.WriteLine(nameof(test) + " " + test);
            //}
        }

        private static void SetVoiceByName()
        {
            NamePtr = System.Runtime.InteropServices.Marshal.StringToHGlobalUni(Name);
            System.Console.WriteLine(nameof(NamePtr) + " " + NamePtr);

            Espeak_ERROR setVoiceByName = (Espeak_ERROR)EspeakAPI.espeak_SetVoiceByName(NamePtr);
            System.Console.WriteLine(nameof(setVoiceByName) + " " + setVoiceByName);
        }

        private static void SetVoiceByProperties()
        {
            NamePtr = System.Runtime.InteropServices.Marshal.StringToHGlobalUni(Name);
            LanguagesPtr = System.Runtime.InteropServices.Marshal.StringToHGlobalUni(Languages);
            IdentifierPtr = System.Runtime.InteropServices.Marshal.StringToHGlobalUni(Identifier);

            System.Console.WriteLine(nameof(NamePtr) + " " + NamePtr.ToString());
            System.Console.WriteLine(nameof(LanguagesPtr) + " " + LanguagesPtr.ToString());
            System.Console.WriteLine(nameof(IdentifierPtr) + " " + IdentifierPtr.ToString());

            EspeakVoiceIn = new EspeakVoiceIN();
            EspeakVoiceIn.Name = NamePtr;
            EspeakVoiceIn.Languages = LanguagesPtr;
            EspeakVoiceIn.Identifier = IdentifierPtr;
            EspeakVoiceIn.Gender = (uint)System.Math.Round(System.Random.Shared.NextDouble() + 1, 0);
            EspeakVoiceIn.Age = (uint)System.Math.Round(System.Random.Shared.NextDouble() * 100, 0);
            EspeakVoiceIn.Variant = 0;
            System.Console.WriteLine(nameof(EspeakVoiceIn) + " " + EspeakVoiceIn.ToString());

            int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(EspeakVoiceIN));
            EspeakVoiceInPtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(size);
            System.Runtime.InteropServices.Marshal.StructureToPtr(EspeakVoiceIn, EspeakVoiceInPtr, true);

            Espeak_ERROR SetVoiceByProperties = (Espeak_ERROR)EspeakAPI.espeak_SetVoiceByProperties(EspeakVoiceInPtr);
            System.Console.WriteLine(nameof(SetVoiceByProperties) + " " + SetVoiceByProperties.ToString());
        }

        public static void Speak(string tts)
        {

            string zeroTerminator = "\0";
            string text = tts + zeroTerminator;
            // Equal to (or greatrer than) the size of the text data, in bytes.  
            int utf8ByteCount = System.Text.ASCIIEncoding.UTF8.GetByteCount(text);
            System.Console.WriteLine(nameof(utf8ByteCount) + " " + utf8ByteCount);

            EspeakTextPtr = System.Runtime.InteropServices.Marshal.StringToHGlobalUni(text);
            System.Console.WriteLine(nameof(EspeakTextPtr) + " " + EspeakTextPtr);

            int Synth = EspeakAPI.espeak_Synth(EspeakTextPtr, utf8ByteCount, 0, 0, 0, 0, UniqueIdentifier, UserData);
            System.Console.WriteLine(nameof(EspeakAPI.espeak_Synth) + " " + Synth + " " + text);

            //int IsPlaying = espeak_IsPlaying();
            //Console.WriteLine(nameof(IsPlaying) + " : " + IsPlaying);

            //int Cancel = espeak_Cancel();
            //Console.WriteLine(nameof(espeak_Cancel) + " " + Cancel);
        }

        private static void GetVoice()
        {
            //System.UIntPtr GetCurrentVoice = EspeakAPI.espeak_GetCurrentVoice();
            //Espeak_VOICE PtrToStructure = (Espeak_VOICE)System.Runtime.InteropServices.Marshal.PtrToStructure(GetCurrentVoice, typeof(Espeak_VOICE));
            //Console.WriteLine(nameof(PtrToStructure) + " " + PtrToStructure.ToString());

            //if (VOICE.Equals(default(Espeak_VOICE)))
            //{
            //    throw new Exception("eSpeak returned an empty voice object. Did you call one of the ESpeak.SetVoice*() functions?");
            //}
        }
    }
}
