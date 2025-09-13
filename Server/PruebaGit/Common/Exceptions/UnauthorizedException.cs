using System.Net;
using GitEjercicioSENA.Common.Exceptions.Configuration;

namespace GitEjercicioSENA.Common.Exceptions;

public class UnauthorizedException() : CustomException("Credenciales Incorrectas.", HttpStatusCode.Unauthorized)
{
}
