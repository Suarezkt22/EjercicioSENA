using System.Text.Json.Serialization;

namespace GitEjercicioSENA.Common.Wrappers;

public class Response<T>
{
    public Response()
    {
        Succeeded = true;
    }

    public Response(T data)
    {
        Succeeded = true;
        Data = data;
    }

    public Response(string message)
    {
        Succeeded = true;
        Message = message;
    }


    public bool Succeeded { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; private set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T Data { get; private set; } = default!;
}
