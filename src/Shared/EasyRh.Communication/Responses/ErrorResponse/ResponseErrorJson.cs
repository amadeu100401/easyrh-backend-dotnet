namespace EasyRh.Communication.Responses.ErrorResponse;

public class ResponseErrorJson
{
    public IList<string> Errors { get; set; }
    public DateTime TimeStamp { get; set; }

    public ResponseErrorJson(IList<string> errors)
    {
        TimeStamp = DateTime.UtcNow;  
        Errors = errors;    
    }

    public ResponseErrorJson(string errors)
    {
        Errors = new List<string>() { errors };
    }
}
