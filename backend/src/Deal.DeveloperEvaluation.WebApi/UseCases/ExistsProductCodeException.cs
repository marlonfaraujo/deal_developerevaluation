namespace Deal.DeveloperEvaluation.WebApi.UseCases
{
    public class ExistsProductCodeException : Exception
    {
        public ExistsProductCodeException(string message) : base(message)
        {
        }

        public ExistsProductCodeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
