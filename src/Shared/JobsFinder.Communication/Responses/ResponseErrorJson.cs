namespace JobsFinder.Communication.Responses;
public class ResponseErrorJson
{
    public IList<string> Errors { get; set; } = new List<string>();

    public ResponseErrorJson(IList<string> errors) => Errors = errors;

    public bool TokenIsExpired { get; set; }

    public ResponseErrorJson(string error)
    {
        Errors = new List<string>
        {
            error
        };
    }
}
