using System.Net;
using PruebaRaddarStudios.Common.Exceptions.Configuration;

namespace PruebaRaddarStudios.Common.Exceptions;

public class InternalException() : CustomException("Ha ocurrido un error interno, intente mas tarde." , HttpStatusCode.InternalServerError)
{
}
