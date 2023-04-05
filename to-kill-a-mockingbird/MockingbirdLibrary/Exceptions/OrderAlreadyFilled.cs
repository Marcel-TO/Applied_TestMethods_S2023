namespace MockingbirdLibrary.Exceptions
{
    public class OrderAlreadyFilled : Exception
    {
        public OrderAlreadyFilled()
        {
        }

        public OrderAlreadyFilled(string message) : base(message)
        {
        }
    }
}