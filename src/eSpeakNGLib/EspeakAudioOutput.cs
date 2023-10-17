namespace eSpeakNGLib
{
    public enum EspeakAudioOutput
    {
        /// <summary>
        /// PLAYBACK mode: plays the audio data, supplies events to the calling program 
        /// </summary>
        AUDIO_OUTPUT_PLAYBACK,
        /// <summary>
        /// RETRIEVAL mode: supplies audio data and events to the calling program  
        /// </summary>
        AUDIO_OUTPUT_RETRIEVAL,
        /// <summary>
        /// SYNCHRONOUS mode: as RETRIEVAL but doesn't return until synthesis is completed  
        /// </summary>
        AUDIO_OUTPUT_SYNCHRONOUS,
        /// <summary>
        /// Synchronous playback  
        /// </summary>
        AUDIO_OUTPUT_SYNCH_PLAYBACK
    }
}
