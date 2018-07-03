using System;

using Donna.Core;

namespace Donna.SpeechService
{   public class TextToSpeechAPIClient
    {
        private SpeechServiceAuthentication _authentication;

        public TextToSpeechAPIClient(SpeechServiceAuthentication authentication)
        {
            _authentication = authentication;                        
        }        

        public void TestCall()
        {
            
        }
    }
}
