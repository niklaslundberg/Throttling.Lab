using System;

namespace Jayway.Throttling
{
    public interface IThrottlingService
    {
        bool Allow(string account, long cost, Func<Interval> intervalFactory);
    }
}