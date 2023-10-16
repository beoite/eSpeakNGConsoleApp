namespace eSpeakNGLib
{
    /// typedef struct {
    ///     const char* name;           // a given name for this voice. UTF8 string.
    ///     const char* languages;      // list of pairs of (byte) priority + (string) language (and dialect qualifier)
    ///     const char* identifier;     // the filename for this voice within espeak-ng-data/voices
    ///     unsigned char gender;       // 0=none 1=male, 2=female,
    ///     unsigned char age;          // 0=not specified, or age in years
    ///     unsigned char variant;      // only used when passed as a parameter to espeak_SetVoiceByProperties
    ///     unsigned char xx1;          // for internal use
    ///     int score;                  // for internal use
    ///     void* spare;                // for internal use
    /// }

    /// <summary>
    /// Note: The espeak_VOICE structure is used for two purposes:
    /// 1.  To return the details of the available voices.
    /// 2.  As a parameter to  espeak_SetVoiceByProperties() in order to specify selection criteria.
    /// In(1), the "languages" field consists of a list of(UTF8) language names for which this voice
    /// may be used, each language name in the list is terminated by a zero byte and is also preceded by
    /// a single byte which gives a "priority" number.The list of languages is terminated by an
    /// additional zero byte.
    /// A language name consists of a language code, optionally followed by one or more qualifier (dialect)
    /// names separated by hyphens (eg. "en-uk").  A voice might, for example, have languages "en-uk" and
    /// "en".  Even without "en" listed, voice would still be selected for the "en" language (because
    /// "en-uk" is related) but at a lower priority.
    /// The priority byte indicates how the voice is preferred for the language. A low number indicates a
    /// more preferred voice, a higher number indicates a less preferred voice.
    /// In(2), the "languages" field consists simply of a single (UTF8) language name, with no preceding
    /// priority byte.
    /// espeak_VOICE;
    /// </summary>

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    public struct EspeakVoice
    {
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string Name;

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string Languages;

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string Identifier;


        /// <summary>
        /// 0=none 1=male, 2=female,
        /// </summary>
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte Gender;

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte Age;

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte Variant;

        public override string ToString()
        {
            System.String gender = Gender.ToString();
            gender = gender.Replace("1", "Male");
            gender = gender.Replace("2", "Female");
            return "Name:" + Name + " Languages:" + Languages + "  Identifier:" + Identifier + " Gender:" + gender + " Age:" + Age + " Variant:" + Variant;
        }
    }

    /// <summary>
    /// An array of pointers
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    public struct ListVoicesPointerArray
    {
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = Config.ListVoices)]
        public System.IntPtr[] PointerArray;
    }

}