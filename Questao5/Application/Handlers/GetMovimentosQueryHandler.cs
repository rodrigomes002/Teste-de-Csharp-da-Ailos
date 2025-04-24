using MediatR;
using Questao5.Application.Queries;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories;

namespace Questao5.Application.Handlers;

public class GetContaCorrenteByIdQueryHandler : IRequestHandler<GetContaCorrenteByIdQuery, ContaCorrente?>
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;

    public GetContaCorrenteByIdQueryHandler(IContaCorrenteRepository contaCorrenteRepository)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
    }

    public async Task<ContaCorrente?> Handle(GetContaCorrenteByIdQuery request, CancellationToken cancellationToken)
    {
        return (await _contaCorrenteRepository.GetContaCorrenteByIdAsync(request.IdContaCorrente));
    }
}
