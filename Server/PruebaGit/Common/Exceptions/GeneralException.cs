using System.Net;
using PruebaRaddarStudios.Common.Exceptions.Configuration;

namespace PruebaRaddarStudios.Common.Exceptions;

public class GeneralException(string message) : CustomException(message, HttpStatusCode.BadRequest)
{
}
