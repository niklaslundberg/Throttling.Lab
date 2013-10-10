using System;
using Microsoft.ApplicationServer.Caching;

namespace Jayway.Throttling
{
    public static class CacheExtensions
    {
        public static long GetRemainingCredits(this DataCache dataCache, string account, long cost, long initialCredits,
            TimeSpan timeout, ICostCalculator costCalculator)
        {
            if (string.IsNullOrWhiteSpace(account))
            {
                throw new ArgumentNullException("account");
            }
            if (cost <= 0)
            {
                throw new ArgumentOutOfRangeException("cost", "Cost cannot be negative");
            }
            if (initialCredits <= 0)
            {
                throw new ArgumentOutOfRangeException("initialCredits", "Initial credit cannot be negative");
            }
            if (timeout.TotalMilliseconds <= 0)
            {
                throw new ArgumentOutOfRangeException("timeout", "Timeout must be a time interval");
            }

            DataCacheItem cacheItem = dataCache.GetCacheItem(account);


            long remainingCredits;

            if (cacheItem == null)
            {
                remainingCredits = costCalculator.CalculateRemainingCredits(initialCredits, cost);

                if (remainingCredits <= 0)
                {
                    return remainingCredits;
                }

                dataCache.Add(account, remainingCredits, timeout);

                return remainingCredits;
            }

            if (!(cacheItem.Value is long))
            {
                throw new InvalidOperationException(string.Format("The cached value with key {0} is not a valid long",
                    account));
            }

            var cachedValue = (long) cacheItem.Value;

            if (cachedValue <= 0)
            {
                return -1;
            }

            remainingCredits = costCalculator.CalculateRemainingCredits(cachedValue, cost);
            
            var newTimeout = cacheItem.Timeout;

            dataCache.Put(account, remainingCredits, cacheItem.Version, newTimeout);

            return remainingCredits;
        }
    }
}