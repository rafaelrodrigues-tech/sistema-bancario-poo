using System;
using System.Globalization;
using POO.Entities.Services;
using POO.Entities.Exceptions;

namespace POO.Entities
{
    class Conta : Banco
    {
        public int Numero { get; set; }
        public string Titular { get; set; }
        public double Saldo { get; protected set; }

        public Conta(int numero, string titular, double saldo)
        {
            if (numero < 0)
            {
                throw new DomainException("Número da conta inválido");
            }
            if (String.IsNullOrEmpty(titular))
            {
                throw new DomainException("O nome do titular é obrigatório");
            }
            if (saldo < 0.0)
            {
                throw new DomainException("O saldo inicial não pode ser negativo");
            }
            Numero = numero;
            Titular = titular;
            Saldo = saldo;

        }
        public void Deposito(double valor)
        {
            if (valor <= 0.0)
            {
                throw new DomainException("O valor do depósito deve ser maior que zero");
            }

            Saldo += valor;
          
        }
        public virtual void Saque(double valor)
        {
            if (Saldo >= valor)
            {
                Saldo -= valor;
            }
            else
            {
                throw new DomainException("Saldo Insuficiente para realizar o saque");
            }
        }
        public override string ToString()
        {
            return "Numero: " + Numero + " |Titular: " + Titular + " |Saldo: R$ " + Saldo.ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}
