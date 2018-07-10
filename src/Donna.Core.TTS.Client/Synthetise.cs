using Donna.Core.TTS.Client;
using Donna.Core.TTS.Client.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public Task Speak(CancellationToken cancellationToken, TTSRequestParameters parameters)
        {
            var requestBuilder = new TTSRequestBuilder();

            var ttsRequest = requestBuilder.Build(parameters);

            _client.DefaultRequestHeaders.Clear();

            foreach (var header in ttsRequest.Headers)
            {
                _client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }

            var httpTask = _client.SendAsync(ttsRequest.RequestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            Debug.WriteLine("Response status code: [{0}]", httpTask.Result.StatusCode);

            var saveTask = httpTask.ContinueWith(
                async (responseMessage, token) =>
                {
                    try
                    {
                        if (responseMessage.IsCompleted && responseMessage.Result != null && responseMessage.Result.IsSuccessStatusCode)
                        {
                            var httpStream = await responseMessage.Result.Content.ReadAsStreamAsync().ConfigureAwait(false);
                            AudioAvailable(new GenericEventArgs<Stream>(httpStream));
                        }
                        else
                        {
                            Error(new GenericEventArgs<Exception>(new Exception(String.Format("Service returned {0}", responseMessage.Result.StatusCode))));
                        }
                    }
                    catch (Exception e)
                    {
                        Error(new GenericEventArgs<Exception>(e.GetBaseException()));
                    }
                    finally
                    {
                        responseMessage.Dispose();
                        ttsRequest.Dispose();
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
            this.OnAudioAvailable?.Invoke(this, e);
        }

        /// <summary>
        /// Error handler function
        /// </summary>
        /// <param name="e">The exception</param>
        private void Error(GenericEventArgs<Exception> e)
        {
            this.OnError?.Invoke(this, e);
        }


    }
}

