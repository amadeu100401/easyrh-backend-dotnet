namespace EasyRh.Exceptions.ExceptionBase;

public class InvalidLoginException : EasyRhException
{
    public InvalidLoginException() : base("Credênciais inválidas") { }
}
