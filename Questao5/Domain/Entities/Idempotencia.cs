namespace Questao5.Domain.Entities;

public class Idempotencia
{
    public string ChaveIdempotencia { get; set; }
    public string Requisicao { get; set; }
    public string Resultado { get; set; }
}
