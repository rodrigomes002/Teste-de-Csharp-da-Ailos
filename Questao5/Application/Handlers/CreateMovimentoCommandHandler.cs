using MediatR;
using Questao5.Application.Commands;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories;

namespace Questao5.Application.Handlers;

public class CreateMovimentoCommandHandler : IRequestHandler<CreateMovimentoCommand, string>
{
    private readonly IMovimentoRepository _movimentoRepository;
    public CreateMovimentoCommandHandler(IMovimentoRepository movimentoRepository)
    {
        _movimentoRepository = movimentoRepository;
    }

    public async Task<string> Handle(CreateMovimentoCommand request, CancellationToken cancellationToken)
    {
        var movimento = new Movimento()
        {
            IdContaCorrente = request.IdContaCorrente,
            TipoMovimento = request.TipoMovimento,
            Valor = request.Valor
        };

        return await _movimentoRepository.AddMovimentoAsync(movimento);
    }
}