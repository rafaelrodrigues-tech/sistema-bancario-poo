using System;
using System.Globalization;
using SistemaBancario.Entities.Exceptions;

namespace SistemaBancario.Entities
{
    class ContaCorrente:Conta 
    {
        public double Limite { get; set; }

        public ContaCorrente(int numero, string titular, double saldo,double limite):base(numero,titular,saldo)
        {
            if (limite < 0.0)
            {
                throw new DomainException("Erro: Limite não pode ser menor que ZERO!");
            }

            Limite = limite;
        
        }
        public override void Saque(double valor)
        {
            double limiteDisponivel = Saldo + Limite;
            if (valor <= limiteDisponivel)
            {
                Saldo -= valor;
            }
            else
            {
                throw new DomainException("Saldo Insuficiente para realizar o saque");
            }

        }
    }
}
