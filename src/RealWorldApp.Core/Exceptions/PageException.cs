namespace RealWorldApp.Core.Exceptions;

public class PageException : CustomException
{
    public PageException()
        : base("This page is empty :)")
    {
    }
}