using System;

namespace Jayway.Throttling.Web
{
    public class ThrottledRequest
    {
        readonly long _cost;
        readonly Func<Interval> _newInterval;
        readonly IThrottlingService _throttlingService;


        public ThrottledRequest(IThrottlingService throttlingService, int cost, int intervalInSeconds,
            long creditsPerIntervalValue)
        {
            _throttlingService = throttlingService;
            _cost = cost;
            _newInterval = () => new Interval(intervalInSeconds, creditsPerIntervalValue);
        }

        public bool Perform(String account)
        {
            return _throttlingService.Allow(account, _cost, _newInterval);
        }
    }
}