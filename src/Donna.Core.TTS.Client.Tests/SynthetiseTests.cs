using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading;
using Donna.Core.AzureSecurity;
using Donna.Core.AzureSecurity.Providers;
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

            string requestUri = "https://westus.tts.speech.microsoft.com/cognitiveservices/v1";

            var ssmlBuilder = new SsmlBuilder();

            var cortana = new Synthesize(ssmlBuilder);

            cortana.OnAudioAvailable += PlayAudio;
            cortana.OnError += ErrorHandler;

            // Reuse Synthesize object to minimize latency
            cortana.Speak(CancellationToken.None, new InputOptions()
            {
                RequestUri = new Uri(requestUri),
                // Text to be spoken.
                Text = "Good Evening!",
                VoiceType = Gender.Female,
                // Refer to the documentation for complete list of supported locales.
                Locale = "en-US",
                // You can also customize the output voice. Refer to the documentation to view the different
                // voices that the TTS service can output.
                // VoiceName = "Microsoft Server Speech Text to Speech Voice (en-US, Jessa24KRUS)",
                VoiceName = "Microsoft Server Speech Text to Speech Voice (en-US, Guy24KRUS)",
                // VoiceName = "Microsoft Server Speech Text to Speech Voice (en-US, ZiraRUS)",

                // Service can return audio in different output format.
                OutputFormat = AudioOutputFormat.Riff24Khz16BitMonoPcm,
                AuthorizationToken = "Bearer " + accessToken,
            }).Wait();          


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
