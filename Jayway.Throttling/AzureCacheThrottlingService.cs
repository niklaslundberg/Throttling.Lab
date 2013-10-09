using System;
using Microsoft.ApplicationServer.Caching;

namespace Jayway.Throttling
{
    public class AzureCacheThrottlingService : IThrottlingService
    {
        private readonly DataCache _dataCache;

        public AzureCacheThrottlingService(DataCache dataCache)
        {
            _dataCache = dataCache;
        }

        public bool Allow(string account, long cost, Func<Interval> intervalFactory)
        {
            var interval = intervalFactory();
            var result = _dataCache.Decrement(account, cost, interval.Credits - cost);

            return result > 0;
        }
    }
}