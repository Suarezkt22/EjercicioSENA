using System.Net;
using PruebaRaddarStudios.Common.Exceptions.Configuration;

namespace PruebaRaddarStudios.Common.Exceptions;

public class UnauthorizedException() : CustomException("Credenciales Incorrectas.", HttpStatusCode.Unauthorized)
{
}
