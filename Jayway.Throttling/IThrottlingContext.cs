namespace Jayway.Throttling
{

    public interface IThrottlingContext
    {
        IThrottlingService GetThrottlingService();

        void Close();
    }
}