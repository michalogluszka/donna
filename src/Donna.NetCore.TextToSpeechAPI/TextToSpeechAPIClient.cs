using System;

using Donna.Services.NetCore.Core;

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
