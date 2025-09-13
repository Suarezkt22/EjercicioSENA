using System.Net;
using GitEjercicioSENA.Common.Exceptions.Configuration;

namespace GitEjercicioSENA.Common.Exceptions;

public class InternalException() : CustomException("Ha ocurrido un error interno, intente mas tarde." , HttpStatusCode.InternalServerError)
{
}
