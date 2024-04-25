namespace JobsFinder.Communication.Responses;
public class ResponseRegistredUserJson
{
    public string Name { get; set; } = string.Empty;
    public ResponseTokensJson Tokens { get; set; } = default!;
}
