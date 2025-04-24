using MediatR;
using Questao5.Application.Queries;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories;

namespace Questao5.Application.Handlers;

public class GetMovimentosQueryHandler : IRequestHandler<GetMovimentosQuery, IEnumerable<Movimento>>
{
    private readonly IMovimentoRepository _movimentoRepository;

    public GetMovimentosQueryHandler(IMovimentoRepository movimentoRepository)
    {
        _movimentoRepository = movimentoRepository;
    }

    public async Task<IEnumerable<Movimento>> Handle(GetMovimentosQuery request, CancellationToken cancellationToken)
    {
        return (await _movimentoRepository.GetMovimentosAsync(request.IdContaCorrente));
    }
}
