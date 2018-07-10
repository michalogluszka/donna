using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading;
using Donna.Core.Authorization;
using Donna.Core.Authorization.Providers;
using Donna.Core.TTS.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Donna.Core.TTS.Client.Tests
{
    [TestClass]
    public class SynthetiseTests
    {
        [TestMethod]
        public void SynthetiseHelloTest()
        {
            Uri issueTokenUri = new Uri("https://westus.api.cognitive.microsoft.com/sts/v1.0/issuetoken");

            ISubscriptionKeyProvider provider = new SubscriptionKeyEnviromentVariableProvider("SpeechServiceSubscriptionKey");
            var auth = new AzureAuthToken(provider, issueTokenUri);

            string accessToken = auth.GetAccessToken();

            Debug.WriteLine(accessToken);

            var requestUri = new Uri("https://westus.tts.speech.microsoft.com/cognitiveservices/v1");

            var ssmlBuilder = new SsmlBuilder();

            var cortana = new Synthesize(ssmlBuilder);

            cortana.OnAudioAvailable += PlayAudio;
            cortana.OnError += ErrorHandler;

            var requestParams = new TTSRequestParameters()
            {
                AuthorizationToken = accessToken,
                RequestUri = requestUri,
                OutputFormat = AudioOutputFormat.Riff24Khz16BitMonoPcm,
                Locale = "en-US",
                Ssml = ssmlBuilder.GenerateSsml("en-US", Gender.Male, "Microsoft Server Speech Text to Speech Voice (en-US, Jessa24KRUS)", "Hello. You are so awesome!"),
                VoiceName = "Microsoft Server Speech Text to Speech Voice (en-US, Jessa24KRUS)",
                // request.VoiceName = "Microsoft Server Speech Text to Speech Voice (en-US, Guy24KRUS)";
                // request.VoiceName = "Microsoft Server Speech Text to Speech Voice (en-US, ZiraRUS)";
                VoiceType = Gender.Female
            };

            // Reuse Synthesize object to minimize latency
            cortana.Speak(CancellationToken.None, requestParams).Wait();          


        }

        private static void PlayAudio(object sender, GenericEventArgs<Stream> args)
        {
            Console.WriteLine(args.EventData);

            // For SoundPlayer to be able to play the wav file, it has to be encoded in PCM.
            // Use output audio format AudioOutputFormat.Riff16Khz16BitMonoPcm to do that.
            SoundPlayer player = new SoundPlayer(args.EventData);
            player.PlaySync();
            args.EventData.Dispose();
        }

        /// <summary>
        /// Handler an error when a TTS request failed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GenericEventArgs{Exception}"/> instance containing the event data.</param>
        private static void ErrorHandler(object sender, GenericEventArgs<Exception> e)
        {
            Console.WriteLine("Unable to complete the TTS request: [{0}]", e.ToString());
            Assert.Fail("Failed to connect to server");
        }
    }
}
