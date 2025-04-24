using Questao1;

namespace Questoes.Test;

public class Questao1Tests
{
    [Fact]
    public void ContaBancariaTest1()
    {
        var conta = new ContaBancaria(5447, "Milton Gonçalves", 350.00);
        conta.Deposito(200);
        conta.Saque(199);

        Assert.Equal(347.50, conta.Saldo);
    }

    [Fact]
    public void ContaBancariaTest2()
    {
        var conta = new ContaBancaria(5139, "Elza Soares");
        conta.Deposito(300);
        conta.Saque(298);

        Assert.Equal(-1.50, conta.Saldo);
    }    
}