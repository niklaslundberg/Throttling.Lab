namespace Jayway.Throttling
{
    /// <summary>
    /// Responsible for keeping track of the number of credits left for each customer
    /// within a time interval.
    /// </summary>
    public class Interval
    {
        public int Seconds { get; private set; }
        public long Credits { get; private set; }

        public Interval(int seconds, long credits)
        {
            this.Seconds = seconds;
            this.Credits = credits;
        }

    }
}
