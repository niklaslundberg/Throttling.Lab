namespace Jayway.Throttling
{
    public class SingleAccountInputModel
    {
        public SingleAccountInputModel(string account, int cost, int intervalInSeconds, long creditsPerIntervalValue)
        {
            Account = account;
            Cost = cost;
            IntervalInSeconds = intervalInSeconds;
            CreditsPerIntervalValue = creditsPerIntervalValue;
        }

        public string Account { get; private set; }
        public int Cost { get; private set; }
        public int IntervalInSeconds { get; private set; }
        public long CreditsPerIntervalValue { get; private set; }
    }
}