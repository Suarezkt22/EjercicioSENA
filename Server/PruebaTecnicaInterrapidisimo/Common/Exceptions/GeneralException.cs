using System.Net;
using PruebaTecnicaInterrapidisimo.Common.Exceptions.Configuration;

namespace PruebaTecnicaInterrapidisimo.Common.Exceptions;

public class GeneralException(string message) : CustomException(message, HttpStatusCode.BadRequest)
{
}
