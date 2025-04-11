using System.Net;
using PruebaTecnicaAmaris.Common.Exceptions.Configuration;

namespace PruebaTecnicaAmaris.Common.Exceptions;

public class NotFoundException(string message) : CustomException(message, HttpStatusCode.NotFound)
{
}
