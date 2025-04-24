using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Queries;

public class GetMovimentosQuery : IRequest<IEnumerable<Movimento>>
{
    public string IdContaCorrente { get; set; }

    public GetMovimentosQuery(string idContaCorrente)
    {
        IdContaCorrente = idContaCorrente;
    }
}