using MediatR;

namespace Questao5.Application.Commands;

public class CreateIdempotenciaCommand : IRequest
{
    public string ChaveIdempotencia { get; set; }
    public string Requisicao { get; set; }
    public string Resultado { get; set; }

    public CreateIdempotenciaCommand(string chaveIdempotencia, string requisicao, string resultado)
    {
        ChaveIdempotencia = chaveIdempotencia;
        Requisicao = requisicao;
        Resultado = resultado;
    }
}