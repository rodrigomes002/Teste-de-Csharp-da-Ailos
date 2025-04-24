using System;

namespace Questao1
{
    public class ContaBancaria
    {
        public int? Numero { get; private set; }
        public string Titular { get; set; }
        public double Saldo { get; private set; }

        public const double Taxa = 3.50;

        public ContaBancaria(int numero, string titular, double depositoInicial)
        {
            Numero = numero;
            Titular = titular;
            Saldo = depositoInicial;
        }

        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            Titular = titular;
            Saldo = 0;
        }

        public void Deposito(double quantia)
        {
            Saldo += quantia;
        }

        public void Saque(double quantia)
        {
            Saldo -= (quantia + Taxa);
        }

        public string GetDetalhes() => $"Conta: {Numero}, Titular: {Titular}, Saldo: $ {Saldo}";
    }
}
