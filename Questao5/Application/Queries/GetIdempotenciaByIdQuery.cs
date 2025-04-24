using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Queries;

public class GetIdempotenciaByIdQuery : IRequest<Idempotencia?>
{
    public string ChaveIdempotencia { get; set; }

    public GetIdempotenciaByIdQuery(string idChaveIdempotencia)
    {
        ChaveIdempotencia = idChaveIdempotencia;
    }
}
