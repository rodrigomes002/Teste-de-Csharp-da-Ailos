using MediatR;

namespace Questao5.Application.Commands;

public class CreateMovimentoCommand : IRequest<string>
{
    public string IdContaCorrente { get; set; }
    public string TipoMovimento { get; set; }
    public double Valor { get; set; }

    public CreateMovimentoCommand(string idContaCorrente, string tipoMovimento, double valor)
    {
        IdContaCorrente = idContaCorrente;
        TipoMovimento = tipoMovimento;
        Valor = valor;
    }
}