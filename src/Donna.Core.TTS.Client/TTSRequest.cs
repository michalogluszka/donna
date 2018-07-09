using System;
using System.Collections.Generic;
using System.Text;

namespace Donna.Core.TTS.Client
{
    public class TTSRequest
    {
        private List<KeyValuePair<string, string>> _headers;

        public TTSRequest()
        {            
            _headers = new List<KeyValuePair<string, string>>(); 
        }

        /// <summary>
        /// Gets or sets the request URI.
        /// </summary>
        public Uri RequestUri { get; set; }


        public List<KeyValuePair<string, string>> Headers
        {
            get
            {
                return _headers;
            }
            
        }


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
