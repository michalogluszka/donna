using System;
using System.Collections.Generic;
using System.Text;

namespace Donna.Core.AzureSecurity.Providers
{
    public class SpeechServiceVariableProvider : ISpeechServiceProvider
    {
        private string _endpointVariableName;

        private string _subscriptionKeyVariableName;

        public SpeechServiceVariableProvider(string endpointVariableName, string subscriptionKeyVariableName)
        {
            _endpointVariableName = endpointVariableName;
            _subscriptionKeyVariableName = subscriptionKeyVariableName;
        }

        public SpeechServiceDefinition GetDefinition()
        {
            string address = Environment.GetEnvironmentVariable(_endpointVariableName, EnvironmentVariableTarget.User);

            if (String.IsNullOrEmpty(address))
                throw new InvalidOperationException("Missing environment variable:" + _endpointVariableName);

            Uri endpoint = new Uri(address);

            string subscriptionKey = Environment.GetEnvironmentVariable(_subscriptionKeyVariableName, EnvironmentVariableTarget.User);

            if (String.IsNullOrEmpty(subscriptionKey))
                throw new InvalidOperationException("Missing environment variable:" + _subscriptionKeyVariableName);


            return new SpeechServiceDefinition()
            {
                Endopoint = endpoint,
                SubscriptionKey = subscriptionKey
            };
        }
    }
}
