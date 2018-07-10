using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Donna.Core.TTS.Client
{
    public class TTSRequest
    {
        public TTSRequest()
        {            
            Headers = new List<KeyValuePair<string, string>>(); 
        }

        public List<KeyValuePair<string, string>> Headers { get; }

        public HttpRequestMessage RequestMessage { get; set; }
    }
}
