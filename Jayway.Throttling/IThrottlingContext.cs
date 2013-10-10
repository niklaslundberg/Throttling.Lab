using System;

namespace Jayway.Throttling
{
    public interface IThrottlingContext : IDisposable
    {
        IThrottlingService GetThrottlingService();
    }
}