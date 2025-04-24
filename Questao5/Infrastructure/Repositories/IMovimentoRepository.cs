using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositories;

public interface IMovimentoRepository
{
    Task<string> AddMovimentoAsync(Movimento entity);
    Task<IEnumerable<Movimento>> GetMovimentosAsync(string idContaCorrente);
}