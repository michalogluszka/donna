using System;

using Donna.NetCore.Core;

namespace Donna.NetCore.TextToSpeechAPI
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
