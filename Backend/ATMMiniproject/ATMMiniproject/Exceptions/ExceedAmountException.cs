namespace ATMMiniproject.Exceptions
{
    public class ExceedAmountException : Exception
    {
        public ExceedAmountException(string message) : base(message) { 
        }
    }
}
