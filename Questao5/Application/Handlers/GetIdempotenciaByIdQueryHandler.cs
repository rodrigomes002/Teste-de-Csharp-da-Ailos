using MediatR;
using Questao5.Application.Queries;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories;

namespace Questao5.Application.Handlers;

public class GetIdempotenciaByIdQueryHandler : IRequestHandler<GetIdempotenciaByIdQuery, Idempotencia?>
{
    private readonly IIdempotenciaRepository _idempotenciaRepository;

    public GetIdempotenciaByIdQueryHandler(IIdempotenciaRepository idempotenciaRepository)
    {
        _idempotenciaRepository = idempotenciaRepository;
    }

    public async Task<Idempotencia?> Handle(GetIdempotenciaByIdQuery request, CancellationToken cancellationToken)
    {
        return (await _idempotenciaRepository.GetIdempotenciaByIdAsync(request.ChaveIdempotencia));
    }
}
