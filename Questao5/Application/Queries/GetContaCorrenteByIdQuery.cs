using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Queries;

public class GetContaCorrenteByIdQuery : IRequest<ContaCorrente?>
{
    public string IdContaCorrente { get; set; }

    public GetContaCorrenteByIdQuery(string idContaCorrente)
    {
        IdContaCorrente = idContaCorrente;
    }
}