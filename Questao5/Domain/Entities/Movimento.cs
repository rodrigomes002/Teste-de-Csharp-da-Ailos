namespace Questao5.Domain.Entities;

public class Movimento
{
    public string IdMovimento { get; set; }
    public string IdContaCorrente { get; set; }
    public string DataMovimento { get; set; }
    public string TipoMovimento { get; set; }
    public double Valor { get; set; }
}
