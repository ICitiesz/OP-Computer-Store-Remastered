namespace opcs.App.Common.Exception;

public class OpcsException(string message) : System.Exception
{
    public override string Message { get; } = message;

    public int ResponseStatusCode { get; set; } = StatusCodes.Status500InternalServerError;
}