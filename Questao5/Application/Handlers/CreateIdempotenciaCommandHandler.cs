using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories;

namespace Questao5.Application.Handlers;

public class CreateIdempotenciaCommandHandler : IRequestHandler<CreateIdempotenciaCommand>
{
    private readonly IIdempotenciaRepository _idempotenciaRepository;
    public CreateIdempotenciaCommandHandler(IIdempotenciaRepository idempotenciaRepository)
    {
        _idempotenciaRepository = idempotenciaRepository;
    }

    public async Task<Unit> Handle(CreateIdempotenciaCommand request, CancellationToken cancellationToken)
    {
        var idempotencia = new Idempotencia()
        {
            ChaveIdempotencia = request.ChaveIdempotencia,
            Requisicao = JsonConvert.SerializeObject(request),
            Resultado = request.Resultado
        };

        await _idempotenciaRepository.AddIdempotenciaAsync(idempotencia);

        return Unit.Value;
    }
}