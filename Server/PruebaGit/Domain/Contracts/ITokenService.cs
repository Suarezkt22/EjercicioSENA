using GitEjercicioSENA.Domain.DTOs;
using GitEjercicioSENA.Domain.Entities;

namespace GitEjercicioSENA.Domain.Contracts;

public interface ITokenService
{
    string Generate(UserDataDto userData);
}