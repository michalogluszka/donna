using System;
using Donna.Core.AzureSecurity;

namespace Donna.Services.NetCore.TextToSpeechAPI
{
    public class TextToSpeechAPIClient
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
