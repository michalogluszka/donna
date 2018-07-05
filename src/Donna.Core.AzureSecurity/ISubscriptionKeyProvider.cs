using System;
using System.Collections.Generic;
using System.Text;

namespace Donna.Core.AzureSecurity
{
    public interface ISubscriptionKeyProvider
    {
        /// <summary>
        /// Provides endpoint and subscription key
        /// </summary>
        /// <returns>endpoint,subscriptionkey</returns>
        string GetSubscriptionKey();
    }
}
