namespace Questao5.Application.Commands.Requests;

public class MovimentoRequest
{
    public string IdContaCorrente { get; set; } = string.Empty;
    public string ChaveIdempotencia { get; set; } = string.Empty;
    public double Valor { get; set; }
    public string TipoMovimento { get; set; } = string.Empty;
}
