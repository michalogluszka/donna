using Donna.Core.TTS.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Donna.Core.TTS.Client
{
 
    /// <summary>
    /// Sample synthesize request
    /// </summary>
    public class Synthesize
    {

        private HttpClient _client;
        private HttpClientHandler _clientHandler;

        private ISsmlBuilder _ssmlBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="Synthesize"/> class.
        /// </summary>
        public Synthesize(ISsmlBuilder ssmlBuilder)
        {
            var cookieContainer = new CookieContainer();
            _clientHandler = new HttpClientHandler() { CookieContainer = new CookieContainer(), UseProxy = false };
            _client = new HttpClient(_clientHandler);

            _ssmlBuilder = ssmlBuilder;
        }

        ~Synthesize()
        {
            _client.Dispose();
            _clientHandler.Dispose();
        }

        /// <summary>
        /// Called when a TTS request has been completed and audio is available.
        /// </summary>
        public event EventHandler<GenericEventArgs<Stream>> OnAudioAvailable;

        /// <summary>
        /// Called when an error has occured. e.g this could be an HTTP error.
        /// </summary>
        public event EventHandler<GenericEventArgs<Exception>> OnError;

        /// <summary>
        /// Sends the specified text to be spoken to the TTS service and saves the response audio to a file.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        public Task Speak(CancellationToken cancellationToken, TTSRequest ttsRequest)
        {
            _client.DefaultRequestHeaders.Clear();
            foreach (var header in ttsRequest.Headers)
            {
                _client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }

            var genderValue = "";
            switch (ttsRequest.VoiceType)
            {
                case Gender.Male:
                    genderValue = "Male";
                    break;

                case Gender.Female:
                default:
                    genderValue = "Female";
                    break;
            }

            var request = new HttpRequestMessage(HttpMethod.Post, ttsRequest.RequestUri)
            {
                Content = new StringContent(_ssmlBuilder.GenerateSsml(ttsRequest.Locale, genderValue, ttsRequest.VoiceName, ttsRequest.Text))
            };

            var httpTask = _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            Console.WriteLine("Response status code: [{0}]", httpTask.Result.StatusCode);

            var saveTask = httpTask.ContinueWith(
                async (responseMessage, token) =>
                {
                    try
                    {
                        if (responseMessage.IsCompleted && responseMessage.Result != null && responseMessage.Result.IsSuccessStatusCode)
                        {
                            var httpStream = await responseMessage.Result.Content.ReadAsStreamAsync().ConfigureAwait(false);
                            this.AudioAvailable(new GenericEventArgs<Stream>(httpStream));
                        }
                        else
                        {
                            this.Error(new GenericEventArgs<Exception>(new Exception(String.Format("Service returned {0}", responseMessage.Result.StatusCode))));
                        }
                    }
                    catch (Exception e)
                    {
                        this.Error(new GenericEventArgs<Exception>(e.GetBaseException()));
                    }
                    finally
                    {
                        responseMessage.Dispose();
                        request.Dispose();
                    }
                },
                TaskContinuationOptions.AttachedToParent,
                cancellationToken);

            return saveTask;
        }

        /// <summary>
        /// Called when a TTS requst has been successfully completed and audio is available.
        /// </summary>
        private void AudioAvailable(GenericEventArgs<Stream> e)
        {
            EventHandler<GenericEventArgs<Stream>> handler = this.OnAudioAvailable;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Error handler function
        /// </summary>
        /// <param name="e">The exception</param>
        private void Error(GenericEventArgs<Exception> e)
        {
            EventHandler<GenericEventArgs<Exception>> handler = this.OnError;
            if (handler != null)
            {
                handler(this, e);
            }
        }


    }
}

