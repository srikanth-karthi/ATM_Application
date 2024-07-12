namespace ATMMiniproject.Exceptions
{
    [Serializable]
    public class ExceedAmountException : Exception
    {
        readonly string msg;
        public ExceedAmountException(string message) : base(message) { 
            msg = message;
        }
        public override string Message => msg;
    }
}
