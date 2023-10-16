namespace ConsoleApp
{
    public static class EspeakAPI
    {
        /*
        ----------------------------------------------------------------------------------------------------------------
        espeak-ng example 1
        ----------------------------------------------------------------------------------------------------------------
         #include <espeak-ng/speak_lib.h>

        espeak_AUDIO_OUTPUT output = AUDIO_OUTPUT_SYNCH_PLAYBACK;
        char* path = NULL;
        void* user_data;
        unsigned int* identifier;

        int main(int argc, char* argv[])
        {
            char voicename[] = { "English" }; // Set voice by its name
            char text[] = { "Hello world!" };
            int buflength = 500, options = 0;
            unsigned int position = 0, position_type = 0, end_position = 0, flags = espeakCHARS_AUTO;
            espeak_Initialize(output, buflength, path, options);
            espeak_SetVoiceByName(voicename);
            printf("Saying  '%s'...\n", text);
            espeak_Synth(text, buflength, position, position_type, end_position, flags, identifier, user_data);
            printf("Done\n");
            return 0;
        }

        ----------------------------------------------------------------------------------------------------------------
        espeak-ng example 2
        ----------------------------------------------------------------------------------------------------------------
        #include <string.h>
        #include <malloc.h>
        #include <espeak-ng/speak_lib.h>

        espeak_AUDIO_OUTPUT output = AUDIO_OUTPUT_SYNCH_PLAYBACK;
        char* path = NULL;
        void* user_data;
        unsigned int* identifier;

        int main(int argc, char* argv[] )
        {
            char text[] = { "Hello world!" };
            int buflength = 500, options = 0;
            unsigned int position = 0, position_type = 0, end_position = 0, flags = espeakCHARS_AUTO;
            espeak_Initialize(output, buflength, path, options);
            espeak_VOICE voice;
            memset(&voice, 0, sizeof(espeak_VOICE)); // Zero out the voice first
            const char* langNativeString = "en"; // Set voice by properties
            voice.languages = langNativeString;
            voice.name = "US";
            voice.variant = 2;
            voice.gender = 2;
            espeak_SetVoiceByProperties(&voice);
            printf("Saying  '%s'...\n", text);
            espeak_Synth(text, buflength, position, position_type, end_position, flags, identifier, user_data);
            printf("Done\n");
            return 0;
        }
        */

        public const string FileName = "libespeak-ng";
        public const string WindowsFileName = FileName + ".dll";
        public const string DllImportPath = WindowsFileName;

        public const System.Runtime.InteropServices.CallingConvention MyCallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;
        public const System.Runtime.InteropServices.CharSet MyCharSet = System.Runtime.InteropServices.CharSet.Auto;
        public const bool MySetLastError = false;
        public const bool MyThrowOnUnmappableChar = true;

        /* espeak_Cancel
        ESPEAK_API espeak_ERROR espeak_Cancel(void);
        */
        [System.Runtime.InteropServices.DllImport(DllImportPath, CallingConvention = MyCallingConvention, CharSet = MyCharSet, SetLastError = MySetLastError, ThrowOnUnmappableChar = MyThrowOnUnmappableChar)]
        public static extern int espeak_Cancel();

        /* espeak_GetCurrentVoice
        ESPEAK_API espeak_VOICE *espeak_GetCurrentVoice(void);
        */
        [System.Runtime.InteropServices.DllImport(DllImportPath, CallingConvention = MyCallingConvention, CharSet = MyCharSet, SetLastError = MySetLastError, ThrowOnUnmappableChar = MyThrowOnUnmappableChar)]
        public static extern System.IntPtr espeak_GetCurrentVoice();

        /* espeak_Initialize
           Must be called before any synthesis functions are called.
           output: the audio data can either be played by eSpeak or passed back by the SynthCallback function.
                typedef enum {
	                // PLAYBACK mode: plays the audio data, supplies events to the calling program 
                    AUDIO_OUTPUT_PLAYBACK,
	                // RETRIEVAL mode: supplies audio data and events to the calling program  
	                AUDIO_OUTPUT_RETRIEVAL,
	                // SYNCHRONOUS mode: as RETRIEVAL but doesn't return until synthesis is completed  
	                AUDIO_OUTPUT_SYNCHRONOUS,
	                // Synchronous playback  
	                AUDIO_OUTPUT_SYNCH_PLAYBACK
                }
                espeak_AUDIO_OUTPUT;

           buflength:  The length in mS of sound buffers passed to the SynthCallback function.
                    Value=0 gives a default of 60mS.
                    This parameter is only used for AUDIO_OUTPUT_RETRIEVAL and AUDIO_OUTPUT_SYNCHRONOUS modes.

           path: The directory which contains the espeak-ng-data directory, or NULL for the default location.

           options: bit 0:  1=allow espeakEVENT_PHONEME events.
                    bit 1:  1= espeakEVENT_PHONEME events give IPA phoneme names, not eSpeak phoneme names
                    bit 15: 1=don't exit if espeak_data is not found (used for --help)

           Returns: sample rate in Hz, or -1 (EE_INTERNAL_ERROR).

        ESPEAK_API int espeak_Initialize(espeak_AUDIO_OUTPUT output, int buflength, const char *path, int options);
        */
        [System.Runtime.InteropServices.DllImport(DllImportPath, CallingConvention = MyCallingConvention, CharSet = MyCharSet, SetLastError = MySetLastError, ThrowOnUnmappableChar = MyThrowOnUnmappableChar)]
        public static extern int espeak_Initialize(int output, int buflength, string path, int options);

        /* espeak_IsPlaying
           Returns 1 if audio is played, 0 otherwise.

        ESPEAK_API int espeak_IsPlaying(void);
        */
        [System.Runtime.InteropServices.DllImport(DllImportPath, CallingConvention = MyCallingConvention, CharSet = MyCharSet, SetLastError = MySetLastError, ThrowOnUnmappableChar = MyThrowOnUnmappableChar)]
        public static extern int espeak_IsPlaying();

        /* espeak_ListVoices
           Reads the voice files from espeak-ng-data/voices and creates an array of espeak_VOICE pointers.
           The list is terminated by a NULL pointer

           If voice_spec is NULL then all voices are listed.
           If voice spec is given, then only the voices which are compatible with the voice_spec
           are listed, and they are listed in preference order.

        ESPEAK_API const espeak_VOICE **espeak_ListVoices(espeak_VOICE *voice_spec);
        */
        [System.Runtime.InteropServices.DllImport(DllImportPath, CallingConvention = MyCallingConvention, CharSet = MyCharSet, SetLastError = MySetLastError, ThrowOnUnmappableChar = MyThrowOnUnmappableChar)]
        public static extern System.IntPtr espeak_ListVoices(System.IntPtr voice_spec);

        /* espeak_SetVoiceByName
           Searches for a voice with a matching "name" field.  Language is not considered.
           "name" is a UTF8 string.

           Return: EE_OK: operation achieved
                   EE_BUFFER_FULL: the command can not be buffered;
                     you may try after a while to call the function again.
               EE_INTERNAL_ERROR.

        ESPEAK_API espeak_ERROR espeak_SetVoiceByName(const char *name);
        */
        [System.Runtime.InteropServices.DllImport(DllImportPath, CallingConvention = MyCallingConvention, CharSet = MyCharSet, SetLastError = MySetLastError, ThrowOnUnmappableChar = MyThrowOnUnmappableChar)]
        public static extern int espeak_SetVoiceByName([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPUTF8Str)] string name);


        /* espeak_SetVoiceByProperties
           An espeak_VOICE structure is used to pass criteria to select a voice.  Any of the following
           fields may be set:

           name     NULL, or a voice name

           languages  NULL, or a single language string (with optional dialect), eg. "en-uk", or "en"

           gender   0=not specified, 1=male, 2=female

           age      0=not specified, or an age in years

           variant  After a list of candidates is produced, scored and sorted, "variant" is used to index
                    that list and choose a voice.
                    variant=0 takes the top voice (i.e. best match). variant=1 takes the next voice, etc

        ESPEAK_API espeak_ERROR espeak_SetVoiceByProperties(espeak_VOICE *voice_spec);
        */
        [System.Runtime.InteropServices.DllImport(DllImportPath, CallingConvention = MyCallingConvention, CharSet = MyCharSet, SetLastError = MySetLastError, ThrowOnUnmappableChar = MyThrowOnUnmappableChar)]
        public static extern int espeak_SetVoiceByProperties(System.IntPtr voice_spec);


        /* espeak_Synth
            Synthesize speech for the specified text.  The speech sound data is passed to the calling
            program in buffers by means of the callback function specified by espeak_SetSynthCallback(). The command is asynchronous: it is internally buffered and returns as soon as possible. 
            If espeak_Initialize was previously called with AUDIO_OUTPUT_PLAYBACK as argument, the sound data are played by eSpeak.

            text: The text to be spoken, terminated by a zero character. It may be either 8-bit characters,
               wide characters (wchar_t), or UTF8 encoding.  Which of these is determined by the "flags"
               parameter.

            size: Equal to (or greatrer than) the size of the text data, in bytes.  This is used in order
               to allocate internal storage space for the text.  This value is not used for
               AUDIO_OUTPUT_SYNCHRONOUS mode.

            position:  The position in the text where speaking starts. Zero indicates speak from the
               start of the text.

            position_type:  Determines whether "position" is a number of characters, words, or sentences.
               Values:

            end_position:  If set, this gives a character position at which speaking will stop.  A value
               of zero indicates no end position.

            flags:  These may be OR'd together:
               Type of character codes, one of:
                  espeakCHARS_UTF8     UTF8 encoding
                  espeakCHARS_8BIT     The 8 bit ISO-8859 character set for the particular language.
                  espeakCHARS_AUTO     8 bit or UTF8  (this is the default)
                  espeakCHARS_WCHAR    Wide characters (wchar_t)
                  espeakCHARS_16BIT    16 bit characters.

               espeakSSML   Elements within < > are treated as SSML elements, or if not recognised are ignored.

               espeakPHONEMES  Text within [[ ]] is treated as phonemes codes (in espeak's Kirshenbaum encoding).

               espeakENDPAUSE  If set then a sentence pause is added at the end of the text.  If not set then
                  this pause is suppressed.

            unique_identifier: This must be either NULL, or point to an integer variable to
                which eSpeak writes a message identifier number.
                eSpeak includes this number in espeak_EVENT messages which are the result of
                this call of espeak_Synth().

            user_data: a pointer (or NULL) which will be passed to the callback function in
                espeak_EVENT messages.

            Return: EE_OK: operation achieved
                    EE_BUFFER_FULL: the command can not be buffered;
                      you may try after a while to call the function again.
                EE_INTERNAL_ERROR.

        ESPEAK_API espeak_ERROR espeak_Synth(const void *text,
	        size_t size,
	        unsigned int position,
	        espeak_POSITION_TYPE position_type,
	        unsigned int end_position,
	        unsigned int flags,
	        unsigned int* unique_identifier,
	        void* user_data);
        */
        [System.Runtime.InteropServices.DllImport(DllImportPath, CallingConvention = MyCallingConvention, CharSet = MyCharSet, SetLastError = MySetLastError, ThrowOnUnmappableChar = MyThrowOnUnmappableChar)]
        public static extern int espeak_Synth([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPUTF8Str)] string text, int size, uint position, int position_type, uint end_position, uint flags, System.IntPtr unique_identifier, System.IntPtr user_data);

    }
}
