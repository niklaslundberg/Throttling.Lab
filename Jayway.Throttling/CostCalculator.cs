namespace Jayway.Throttling
{
    public class CostCalculator : ICostCalculator
    {
        public long CalculateRemainingCredits(long initialValue, long cost)
        {
            var remainingCredits = initialValue - cost;

            return remainingCredits;
        }
    }
}