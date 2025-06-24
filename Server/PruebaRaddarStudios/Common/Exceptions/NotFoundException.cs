using System.Net;
using PruebaRaddarStudios.Common.Exceptions.Configuration;

namespace PruebaRaddarStudios.Common.Exceptions;

public class NotFoundException(string message) : CustomException(message, HttpStatusCode.NotFound)
{
}
