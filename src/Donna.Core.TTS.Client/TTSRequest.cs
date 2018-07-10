﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Donna.Core.TTS.Client
{
    public class TTSRequest
    {
        public TTSRequest()
        {            
            Headers = new List<KeyValuePair<string, string>>(); 
        }

        /// <summary>
        /// Gets or sets the request URI.
        /// </summary>
        public Uri RequestUri { get; set; }


        public List<KeyValuePair<string, string>> Headers { get; }


        /// <summary>
        /// Gets or sets the locale.
        /// </summary>
        public String Locale { get; set; }

        /// <summary>
        /// Gets or sets the type of the voice; male/female.
        /// </summary>
        public Gender VoiceType { get; set; }

        /// <summary>
        /// Gets or sets the name of the voice.
        /// </summary>
        public string VoiceName { get; set; }

        /// <summary>
        /// Authorization Token.
        /// </summary>
        public string AuthorizationToken { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

    }
}
