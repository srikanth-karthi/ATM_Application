namespace ATMMiniproject.Exceptions
{
    [Serializable]
    public class NoSuchIteminDbException : Exception
    {
        readonly string msg;
        public NoSuchIteminDbException(string message) : base(message)
        {
            msg = message;
        }
        public override string Message => msg;
    }
}
