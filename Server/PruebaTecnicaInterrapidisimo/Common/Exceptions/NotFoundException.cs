using System.Net;
using PruebaTecnicaInterrapidisimo.Common.Exceptions.Configuration;

namespace PruebaTecnicaInterrapidisimo.Common.Exceptions;

public class NotFoundException(string message) : CustomException(message, HttpStatusCode.NotFound)
{
}
