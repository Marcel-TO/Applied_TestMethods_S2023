namespace MockingbirdLibrary.Exceptions
{
    public class OrderAlreadyFilled : Exception
    {
        public OrderAlreadyFilled(string message) : base(message)
        {
        }
    }
}