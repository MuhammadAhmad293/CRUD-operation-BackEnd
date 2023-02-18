namespace CRUDoperations.Services.CustomExceptions
{
    public class NameRequiredException : Exception
    {
        public NameRequiredException(string message) : base(message) { }
    }
}
