namespace Questao5.Application.Commands.Requests;

public class IdempotenciaRequest
{
    public string ChaveIdempotencia { get; set; } = string.Empty;
    public string Requisicao { get; set; } = string.Empty;
    public string Resultado { get; set; } = string.Empty;
}