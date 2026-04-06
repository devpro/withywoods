namespace Withywoods.Twohire.Abstractions.Models;

public class ResponseModel<T>
{
    public bool Status { get; set; }

    public object? Error { get; set; }

    public T? Data { get; set; }
}
