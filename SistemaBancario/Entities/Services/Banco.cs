using System;
using System.Collections.Generic;
using SistemaBancario.Entities.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SistemaBancario.Entities.Services
{
    class Banco
    {
        public List<Conta> Contas { get; set; } = new List<Conta>();

        public Conta BuscarConta(int numero)
        {
            return Contas.Find(c => c.Numero == numero);//Para cada Conta(c) verifique(=>) se o Numero da Conta é igual o numero.
            
        }
        public void AdicionarConta(Conta conta)
        {
            if (BuscarConta(conta.Numero) != null)
            {
                throw new DomainException("Já existe uma conta com esse número.");
            }
            Contas.Add(conta);
        }
        public void RemoverConta(int numero)
        {
            Conta conta = BuscarConta(numero);
            if (conta == null)
            {
                throw new DomainException("Conta não encontrada.");
            }
            Contas.Remove(conta);
        }
        public void Transferir(Conta origem, Conta destino, double valor)
        {
            if (origem == null || destino == null)
            {
                throw new DomainException("Conta de origem ou destino não encontrada.");
            }
            if (valor <= 0.0)
            {
                throw new DomainException("O valor da transferência deve ser maior que zero.");
            }
            if (origem == destino)
            {
                throw new DomainException("Não é possível transferir para a mesma conta.");
            }

            // realiza saque na conta de origem e depósito na conta destino
            origem.Saque(valor);
            destino.Deposito(valor);
        }
    }
}
