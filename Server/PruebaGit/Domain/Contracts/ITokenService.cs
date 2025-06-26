using PruebaRaddarStudios.Domain.DTOs;
using PruebaRaddarStudios.Domain.Entities;

namespace PruebaRaddarStudios.Domain.Contracts;

public interface ITokenService
{
    string Generate(UserDataDto userData);
}