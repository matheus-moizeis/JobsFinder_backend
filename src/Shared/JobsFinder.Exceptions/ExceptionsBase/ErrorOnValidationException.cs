namespace JobsFinder.Exceptions.ExceptionsBase;
public class ErrorOnValidationException : JobsFinderException
{
    public IList<string> ErrorMessages { get; set; }

    public ErrorOnValidationException(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}
