using System.Net;
using PruebaTecnicaInterrapidisimo.Common.Exceptions.Configuration;

namespace PruebaTecnicaInterrapidisimo.Common.Exceptions;

public class InternalException() : CustomException("Ha ocurrido un error interno, intente mas tarde." , HttpStatusCode.InternalServerError)
{
}
