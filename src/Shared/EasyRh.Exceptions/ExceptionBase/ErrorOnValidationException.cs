namespace EasyRh.Exceptions.ExceptionBase;

public class ErrorOnValidationException : EasyRhException
{
    public IList<string> ErrorMessages { get; set; }

    public ErrorOnValidationException(IList<string> Errors) : base(string.Empty) 
    {
        ErrorMessages = Errors;
    }
}
