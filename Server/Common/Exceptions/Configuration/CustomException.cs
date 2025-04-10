using System.Net;

namespace PruebaTecnicaAmaris.Common.Exceptions.Configuration;

public class CustomException(string message, HttpStatusCode code) : Exception(message)
{
    public int ErrorCode { get; } = (int)code;
}
