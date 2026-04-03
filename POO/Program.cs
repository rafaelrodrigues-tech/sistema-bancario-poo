using System;
using System.Globalization;
using POO.Entities;
using POO.Entities.Exceptions;
using POO.Entities.Services;
namespace MyApp
{
    class POO
    {
        static void Main(string[] args)
        {
            try
            {
                Banco banco = new Banco();

                Console.WriteLine("Entre com uma conta:");
                Console.Write("Digite C para Conta Corrente ou P para Conta Poupança: ");
                char tipo = char.ToUpper(char.Parse(Console.ReadLine()));
                Console.Write("Número: ");
                int numero = int.Parse(Console.ReadLine());
                Console.Write("Titular: ");
                string titular = Console.ReadLine();
                Console.Write("Saldo: ");
                double saldo = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                if (tipo == 'C')
                {
                    Console.Write("Limite: ");
                    int limite = int.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                    ContaCorrente contaCorrente = new ContaCorrente(numero, titular, saldo, limite);
                    banco.AdicionarConta(contaCorrente);
                }
                else if(tipo == 'P')
                {
                    
                    ContaPoupanca conta = new ContaPoupanca(numero, titular, saldo);
                    banco.AdicionarConta(conta);
                }
                bool ExibirMenu = true;
                while (ExibirMenu)
                {
                    Console.WriteLine("""

                    --- OPERAÇÕES ---
                    1 - Depositar
                    2 - Sacar
                    3 - Transferir
                    4 - Aplicar Rendimento (Poupança)

                    --- CONTAS ---
                    5 - Buscar Conta
                    6 - Listar Contas
                    7 - Adicionar Conta
                    8 - Excluir Conta

                    0 - Sair
                    """);
                    Console.Write("Digite a operação que deseja realizar: ");
                    int op = int.Parse(Console.ReadLine());
                    switch (op)
                    {
                        case 1:
                            Console.Write("\nPara realizar um déposito, Insira o número da sua conta novamente: ");
                            numero = int.Parse(Console.ReadLine());
                            Conta conta = banco.BuscarConta(numero);

                            if (conta == null)
                            {
                                Console.WriteLine("Conta não encontrada!");
                            }
                            else {
                                Console.Write("Valor do Deposito: R$ ");
                                double valor = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                conta.Deposito(valor);
                            }
                            break;

                        case 2:
                            Console.Write("\nRealização do saque, Informe o número da Conta: ");
                            numero = int.Parse(Console.ReadLine());
                            Conta contaSaque = banco.BuscarConta(numero);

                            if (contaSaque == null)
                            {
                                Console.WriteLine("Conta não encontrada!");
                            }
                            else
                            {
                                Console.Write("Valor do Saque: R$ ");
                                double valor = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                contaSaque.Saque(valor);
                            }

                            break;
                        case 3:
                            Console.Write("Insira o número da sua conta: ");
                            numero = int.Parse(Console.ReadLine());
                            Conta contaOrigem = banco.BuscarConta(numero);
                            Console.Write("Insira o número da conta destinatária: ");
                            int numeroDestino = int.Parse(Console.ReadLine());
                            Conta contaDestino = banco.BuscarConta(numeroDestino);
                            if (contaOrigem == null || contaDestino == null)
                            {
                                Console.WriteLine("Conta não encontrada!");
                            }
                            else
                            {
                                Console.Write("Valor da transferência: ");
                                double valor = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                banco.Transferir(contaOrigem, contaDestino, valor);
                                Console.WriteLine("Transferência realizada com sucesso!");
                            }
                            break;
                        case 4:
                            Console.WriteLine("Aplicar rendimento (Poupança)");
                            Console.Write("Informe o número da conta: ");
                            numero = int.Parse(Console.ReadLine());
                            Conta contaParaRendimento = banco.BuscarConta(numero);

                            if (contaParaRendimento == null)
                            {
                                Console.WriteLine("Conta não encontrada!");
                            }
                            else
                            {
                                // Só é possível aplicar rendimento em ContaPoupanca
                                ContaPoupanca cp = contaParaRendimento as ContaPoupanca;
                                if (cp == null)
                                {
                                    Console.WriteLine("Operação inválida: apenas contas do tipo Poupança podem receber rendimento.");
                                }
                                else
                                {
                                    cp.AplicarRendimento();
                                    Console.WriteLine("Rendimento aplicado com sucesso!");
                                }
                            }
                            break;


                        case 5:
                            Console.Write("\nInsira o número da Conta que deseja buscar: ");
                            numero = int.Parse(Console.ReadLine());
                            Conta encontrar = banco.BuscarConta(numero);
                            Console.WriteLine(encontrar);
                            break;
                        case 6:
                            Console.WriteLine("\nLista de Contas:");
                            foreach (Conta c in banco.Contas)
                            {
                                Console.WriteLine(c);
                            }
                            Console.WriteLine();
                            break;

                        case 7:
                            Console.Write("\nConta Corrente(C) ou Conta Poupança(P)? ");
                            tipo = char.ToUpper(char.Parse(Console.ReadLine()));
                            Console.Write("Número: ");
                            numero = int.Parse(Console.ReadLine());
                            Console.Write("Titular: ");
                            titular = Console.ReadLine();
                            Console.Write("Saldo: ");
                            saldo = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                            if (tipo == 'C')
                            {
                                Console.WriteLine("Limite: ");
                                int limite = int.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                                ContaCorrente contaCorrente = new ContaCorrente(numero, titular, saldo, limite);
                                banco.AdicionarConta(contaCorrente);
                            }
                            else if (tipo == 'P')
                            {
                                ContaPoupanca contaPoupanca = new ContaPoupanca(numero, titular, saldo);
                                banco.AdicionarConta(contaPoupanca);
                            }
                            break;

                        case 8:
                            Console.Write("\nInsira o número da Conta para a Exclusão: ");
                            int Removerconta = int.Parse(Console.ReadLine());
                            banco.RemoverConta(Removerconta);
                            Console.WriteLine("Conta removida com sucesso!");
                            Console.WriteLine();
                            break;

                        case 0:
                            ExibirMenu = false;
                            Console.WriteLine("\nEncerrando...");
                            Thread.Sleep(2000); // trava a thread inteira( Congela por milissegundos, 2 seg)
                            break;

                        default:
                            Console.WriteLine("\nOperação Inválida! Por favor, Insira uma opção de 0 a 8.");
                            break;
                    }
                }
            }
            catch (DomainException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}