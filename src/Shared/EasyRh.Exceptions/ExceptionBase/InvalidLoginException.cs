namespace EasyRh.Exceptions.ExceptionBase;

public class InvalidLoginException : EasyRhException
{
    public InvalidLoginException() : base(ResourceErrorMessage.Invalid_Id) { }
}
