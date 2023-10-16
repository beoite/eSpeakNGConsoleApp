namespace ConsoleApp
{
    /*  
        0 Afrikaans
        1 afrikaans-mbrola-1
        2 Amharic
        3 Aragonese
        4 arabic-mbrola-1
        5 arabic-mbrola-2
        6 Arabic
        7 Assamese
        8 Azerbaijani
        9 Bashkir
        10 Belarusian
        11 Bulgarian
        12 Bengali
        13 Bishnupriya Manipuri
        14 Bosnian
        15 Catalan
        16 Cherokee
        17 Chinese (Mandarin, latin as English)
        18 Chinese (Mandarin, latin as Pinyin)
        19 czech-mbrola-1
        20 czech-mbrola-2
        21 Czech
        22 Chuvash
        23 Welsh
        24 Danish
        25 German
        26 german-mbrola-1
        27 german-mbrola-2
        28 german-mbrola-3
        29 german-mbrola-4
        30 german-mbrola-6
        31 german-mbrola-5
        32 german-mbrola-7
        33 german-mbrola-8
        34 greek-mbrola-2
        35 Greek
        36 greek-mbrola-1
        37 en-german-1
        38 en-german-2
        39 en-german-3
        40 en-german-4
        41 en-german-5
        42 en-german-6
        43 en-greek
        44 en-romanian
        45 en-dutch
        46 en-french-1
        47 en-french-4
        48 en-hungarian
        49 en-swedish-f
        50 en-afrikaans
        51 en-polish
        52 en-swedish
        53 English (Caribbean)
        54 English (Great Britain)
        55 English (Scotland)
        56 English (Lancaster)
        57 English (West Midlands)
        58 English (Received Pronunciation)
        59 english-mb-en1
        60 English (America)
        61 us-mbrola-1
        62 us-mbrola-2
        63 us-mbrola-3
        64 English (America, New York City)
        65 Esperanto
        66 Spanish (Spain)
        67 Spanish (Latin America)
        68 spanish-mbrola-3
        69 spanish-mbrola-4
        70 spanish-mbrola-1
        71 spanish-mbrola-2
        72 mexican-mbrola-1
        73 mexican-mbrola-2
        74 venezuala-mbrola-1
        75 Estonian
        76 estonian-mbrola-1
        77 Basque
        78 Persian
        79 persian-mb-ir1
        80 Persian (Pinglish)
        81 Finnish
        82 french-mbrola-7
        83 French (Belgium)
        84 french-mbrola-5
        85 fr-canadian-mbrola-1
        86 fr-canadian-mbrola-2
        87 French (Switzerland)
        88 French (France)
        89 french-mbrola-1
        90 french-mbrola-4
        91 french-mbrola-2
        92 french-mbrola-3
        93 french-mbrola-6
        94 Gaelic (Irish)
        95 Gaelic (Scottish)
        96 Guarani
        97 Greek (Ancient)
        98 german-mbrola-6
        99 Gujarati
        100 Hakka Chinese
        101 Hawaiian
        102 Hebrew
        103 hebrew-mbrola-1
        104 hebrew-mbrola-2
        105 hindi-mbrola-1
        106 hindi-mbrola-2
        107 Hindi
        108 Croatian
        109 croatian-mbrola-1
        110 Haitian Creole
        111 Hungarian
        112 hungarian-mbrola-1
        113 Armenian (East Armenia)
        114 Armenian (West Armenia)
        115 Interlingua
        116 Indonesian
        117 indonesian-mbrola-1
        118 Ido
        119 Icelandic
        120 icelandic-mbrola-1
        121 Italian
        122 italian-mbrola-3
        123 italian-mbrola-4
        124 italian-mbrola-1
        125 italian-mbrola-2
        126 japanese-mbrola-1
        127 japanese-mbrola-2
        128 japanese-mbrola-3
        129 Japanese
        130 Lojban
        131 Georgian
        132 Kazakh
        133 Greenlandic
        134 Kannada
        135 Korean
        136 Konkani
        137 Kurdish
        138 Kyrgyz
        139 Latin
        140 latin-mbrola-1
        141 Luxembourgish
        142 Lingua Franca Nova
        143 Lithuanian
        144 lithuanian-mbrola-1
        145 lithuanian-mbrola-2
        146 Latgalian
        147 Latvian
        148 MÄ?ori
        149 maori-mbrola-1
        150 Macedonian
        151 Malayalam
        152 Marathi
        153 malay-mbrola-1
        154 Malay
        155 Maltese
        156 Totontepec Mixe
        157 Myanmar (Burmese)
        158 Norwegian BokmÃ¥l
        159 Nahuatl (Classical)
        160 Nepali
        161 Dutch
        162 dutch-mbrola-2
        163 dutch-mbrola-1
        164 dutch-mbrola-3
        165 Nogai
        166 Oromo
        167 Oriya
        168 Punjabi
        169 Papiamento
        170 Klingon
        171 Polish
        172 polish-mbrola-1
        173 Portuguese (Portugal)
        174 Portuguese (Brazil)
        175 brazil-mbrola-1
        176 brazil-mbrola-2
        177 brazil-mbrola-3
        178 brazil-mbrola-4
        179 portugal-mbrola-1
        180 Pyash
        181 Lang Belta
        182 Quechua
        183 K'iche'
        184 Quenya
        185 Romanian
        186 romanian-mbrola-1
        187 Russian
        188 Russian (Classic)
        189 Russian (Latvia)
        190 Sindhi
        191 Shan (Tai Yai)
        192 Sinhala
        193 Sindarin
        194 Slovak
        195 Slovenian
        196 Lule Saami
        197 Albanian
        198 Serbian
        199 Swedish
        200 swedish-mbrola-1
        201 swedish-mbrola-2
        202 Swahili
        203 Tamil
        204 Telugu
        205 telugu-mbrola-1
        206 Thai
        207 Turkmen
        208 Setswana
        209 Turkish
        210 turkish-mbrola-1
        211 turkish-mbrola-1
        212 Tatar
        213 Uyghur
        214 Ukrainian
        215 Urdu
        216 Uzbek
        217 Vietnamese (Northern)
        218 Vietnamese (Central)
        219 Vietnamese (Southern)
        220 Chinese (Cantonese)
        221 Chinese (Cantonese, latin as Jyutping)
        222 chinese-mb-cn1
    */

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
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint Gender;

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint Age;

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint Variant;

        public override string ToString()
        {
            return "Name:" + Name + " Languages:" + Languages + "  Identifier:" + Identifier + " Gender:" + Gender + " Age:" + Age + " Variant:" + Variant;
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