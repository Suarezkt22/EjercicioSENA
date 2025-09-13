using System.Net;
using GitEjercicioSENA.Common.Exceptions.Configuration;

namespace GitEjercicioSENA.Common.Exceptions;

public class GeneralException(string message) : CustomException(message, HttpStatusCode.BadRequest)
{
}
