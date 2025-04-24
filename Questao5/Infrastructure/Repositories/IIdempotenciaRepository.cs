using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositories;

public interface IIdempotenciaRepository
{
    Task<Idempotencia> GetIdempotenciaByIdAsync(string id);
    Task AddIdempotenciaAsync(Idempotencia entity);
}