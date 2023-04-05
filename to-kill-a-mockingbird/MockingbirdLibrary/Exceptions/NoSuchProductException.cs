namespace MockingbirdLibrary.Exceptions
{
    public class NoSuchProductException : Exception
    {
        public NoSuchProductException()
        {
        }

        public NoSuchProductException(string message) : base(message)
        {
        }
    }
}