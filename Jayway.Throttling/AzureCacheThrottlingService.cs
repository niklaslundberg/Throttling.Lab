using System;
using System.Diagnostics;
using Microsoft.ApplicationServer.Caching;

namespace Jayway.Throttling
{
    public class AzureCacheThrottlingService : IThrottlingService
    {
        readonly ICostCalculator _costCalculator;
        readonly DataCache _dataCache;

        public AzureCacheThrottlingService(DataCache dataCache, ICostCalculator costCalculator)
        {
            _dataCache = dataCache;
            _costCalculator = costCalculator;
        }

        public bool Allow(string account, long cost, Func<Interval> intervalFactory)
        {
            var interval = intervalFactory();

            try
            {
                var result = _dataCache.GetRemainingCredits(account, cost, interval.Credits,
                    TimeSpan.FromSeconds(interval.Seconds),
                    _costCalculator);

                Debug.WriteLine("Remaining credits: {0}", result);

                return result >= 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return true;
            }
        }
    }
}