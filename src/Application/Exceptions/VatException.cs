namespace Engage.Application.Exceptions
{
    public class VatException : Exception
    {
        public VatException(string message) : base(
            $"The Vat amount can't be calculated. \n  {message} "
            )
        {

        }

        public VatException(string message, Exception innerException) :
            base($"The Vat amount can't be calculated. \n {message}", innerException)
        {
        }       
    }
}
