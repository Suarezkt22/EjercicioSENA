using System.Text.Json.Serialization;

namespace PruebaTecnicaAmaris.Common.Wrappers;

public class Response<T>
{
    public Response()
    {
    }

    public Response(T data)
    {
        Succeeded = true;
        Data = data;
    }

    public bool Succeeded { get; private set; }

    public T Data { get; private set; } = default!;
}