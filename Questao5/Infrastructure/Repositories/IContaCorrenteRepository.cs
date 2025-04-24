using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositories;

public interface IContaCorrenteRepository
{
    Task<ContaCorrente> GetContaCorrenteByIdAsync(string id);
}