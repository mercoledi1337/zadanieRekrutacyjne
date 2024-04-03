namespace RealWorldApp.Core.Exceptions;

public class WrongTagException : CustomException
{
    public WrongTagException()
        : base("Tag is null or wrong")
    {
    }
}