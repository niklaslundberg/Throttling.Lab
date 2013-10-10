namespace Jayway.Throttling
{
    public interface ICostCalculator
    {
        long CalculateRemainingCredits(long initialValue, long cost);
    }
}