using System.Net;
using PruebaTecnicaAmaris.Common.Exceptions.Configuration;

namespace PruebaTecnicaAmaris.Common.Exceptions;

public class InternalException() : CustomException("Ha ocurrido un error interno, intente mas tarde." , HttpStatusCode.InternalServerError)
{
}
