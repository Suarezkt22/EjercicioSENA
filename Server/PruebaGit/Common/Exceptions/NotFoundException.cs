using System.Net;
using GitEjercicioSENA.Common.Exceptions.Configuration;

namespace GitEjercicioSENA.Common.Exceptions;

public class NotFoundException(string message) : CustomException(message, HttpStatusCode.NotFound)
{
}
