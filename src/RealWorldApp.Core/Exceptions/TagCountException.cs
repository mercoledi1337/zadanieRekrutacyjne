namespace RealWorldApp.Core.Exceptions;

public class TagCountException : CustomException
{
    public TagCountException()
        : base("Count cant be negative!")
    {
    }
}