namespace CRUDoperations.Services.CustomExceptions
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(string message = "Invalid Request") : base(message) { }
    }
}
