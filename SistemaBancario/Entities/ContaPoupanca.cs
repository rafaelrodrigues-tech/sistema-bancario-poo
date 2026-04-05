using System;
using System.Globalization;

namespace SistemaBancario.Entities
{
    class ContaPoupanca : Conta
    {
        private const double _taxaRendimento = 0.003;
        public ContaPoupanca(int numero, string titular, double saldo): base(numero,titular,saldo)
        {
        }
        public void AplicarRendimento()
        {
            Saldo = Saldo + ( Saldo * _taxaRendimento);
        }
        public override void Saque(double valor)
        {
            Saldo -= valor;
        }

    }

}
