namespace JobsFinder.Exceptions.ExceptionsBase;
public class InvalidLoginException : JobsFinderException
{
    public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
    {
    }
}
