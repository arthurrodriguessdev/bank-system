using Models;
using Utils;
using Enums;
using System.Collections.Generic;

namespace Services{
    public class BancoServico
    {
        #region Listas 
        List<Conta> listaContas = new List<Conta>();
        List<Cliente> listaClientes = new List<Cliente>();
        #endregion

        public void Menu()
        {   
            bool continuar = true;
            while(continuar){
                Console.WriteLine("1 - Registrar Cliente");
                Console.WriteLine("2 - Conta Poupança");
                Console.WriteLine("3 - Conta Corrente");
                Console.WriteLine("0 - Sair");

                Console.Write("\nInsira a opção desejada: ");
                int opcaoInterna = int.Parse(Console.ReadLine());

                if(opcaoInterna == 0)
                {
                    continuar = false;
                    Console.WriteLine("Encerrando operações...");
                    return;
                }
                
                ChamarMetodos(opcaoInterna);
            }
        }

        // Recebe o serviço desejado (opcao) e controla a chamada dos métodos
        public void ChamarMetodos(int opcao)
        {
            switch (opcao)
            {
                case 1:
                    RegistrarCliente();
                    break;
                
                case 2:
                    ChamarMetodosPoupanca();
                    break;

                case 3:
                    ChamarMetodosCorrente();
                    break;
                
                case 4:
                    ChamarImprimirExtrato(typeof(ContaCorrente));
                    break;

            }
        }

        // Este método exibe as opções de menu tanto da conta corrente quanto da poupança de forma dinâmica (retorna a opção)
        public int ExibirMenuContas(string nomeMenu, string[] listaOpcoes, string nomeTipoConta)
        {
            bool continuar = true;
            Interface.DecorarModulo(nomeMenu);

            int indiceOpcoes = 0;
            string ultimaOpcao = listaOpcoes.Last();
            while(continuar)
            {
                Console.WriteLine($"{indiceOpcoes + 1} - {listaOpcoes[indiceOpcoes]}");
                if(listaOpcoes[indiceOpcoes].Equals(ultimaOpcao)){
                    Console.WriteLine("0 - Sair");
                    Console.Write("\nInsira a opção desejada: ");
                    int opcaoSelecionada = int.Parse(Console.ReadLine());

                    if(opcaoSelecionada == 0)
                    {
                        continuar = false;
                        Console.WriteLine($"\nEncerrando operações de {nomeTipoConta}...\n");
                        return 0;
                    }

                    return opcaoSelecionada;
                }
                
                indiceOpcoes ++;
            }

            return 0;
        }

        public void ChamarMetodosCorrente()
        {
            string[] opcoesCorrente ={"Adicionar Nova Conta", "Sacar", "Depositar", "Tirar Extrato"};
            int opcaoEscolhida = ExibirMenuContas("MENU CORRENTE", opcoesCorrente, "conta corrente");
            MetodosCorrente(opcaoEscolhida);
        }

        public void MetodosCorrente(int opcao)
        {
            switch (opcao)
            {
                case 1:
                    RegistrarContaCorrente();
                    break;

                case 2:
                    Sacar("SACAR CORRENTE", typeof(ContaCorrente));
                    break;
                
                case 3:
                    Depositar("DEPOSITAR CORRENTE", typeof(ContaCorrente));
                    break;
            }
        }

        public void ChamarMetodosPoupanca()
        {
            string[] opcoesCorrente ={"Adicionar Nova Conta", "Sacar", "Depositar", "Tirar Extrato"};
            int opcaoEscolhida = ExibirMenuContas("MENU POUPANÇA", opcoesCorrente, "conta poupança");
            MetodosPoupanca(opcaoEscolhida);
        }

        public void MetodosPoupanca(int opcao)
        {
            switch (opcao)
            {
                case 1:
                    RegistrarContaPoupanca();
                    break;

                case 2:
                    Sacar("SACAR POUPANÇA", typeof(ContaPoupanca));
                    break;
                
                case 3:
                    Depositar("DEPOSITAR POUPANÇA", typeof(ContaPoupanca));
                    break;
                
                case 4:
                    ChamarImprimirExtrato(typeof(ContaPoupanca));
                    break;
            }
        }

        public bool ExisteContasCadastradas()
        {
            return listaContas.Count > 0;
        }

        public bool ExisteClientesCadastrados()
        {
            return listaClientes.Count > 0;
        }

        // Esta função realiza o get dos dados da nova conta, seja ela poupança ou corrente
        public Dictionary<string, object> GetDadosNovaConta(string tipoConta)
        {
            Dictionary<string, object>dadosConta = new Dictionary<string, object>();

            Interface.DecorarModulo($"CADASTRO CONTA {tipoConta.ToUpper()}");
            if(!ExisteClientesCadastrados())
            {
                Console.WriteLine("\nAntes de criar uma conta, é preciso ter clientes titulares cadastrados.\n");
                return dadosConta; // Retorna o dicionário vazio
            }

            Console.Write("Informe o número da conta: ");
            int numero = int.Parse(Console.ReadLine());

            ExibirListaClientes();
            Console.Write("\nInforme o número equivalente ao cliente titular da conta: ");
            int indiceCliente = int.Parse(Console.ReadLine());

            if(indiceCliente <= 0 || indiceCliente > listaClientes.Count)
            {
                Console.WriteLine("\nCliente inválido.\n");
                return dadosConta; // Retorna o dicionário vazio
            }

            dadosConta.Add("indiceCliente", indiceCliente);
            dadosConta.Add("numeroConta", numero);
            return dadosConta; // Retorna o dicionário preenchido com índice e número de conta
        }
        public void RegistrarContaPoupanca()
        {   
            Dictionary<string, object>dadosConta = GetDadosNovaConta("POUPANÇA");
            if(!dadosConta.Any() || dadosConta.Count < 2){return;} // Se não retornar a quantidade que se espera, já sai

            Cliente novoTitularConta = listaClientes[(int) dadosConta["indiceCliente"] - 1];
            ContaPoupanca novaContaPoupanca = new ContaPoupanca((int) dadosConta["numeroConta"], novoTitularConta);

            AdicionarContaLista(novoTitularConta, (int) dadosConta["numeroConta"], novaContaPoupanca);
            return;
        }

        public void RegistrarContaCorrente()
        {   
            Dictionary<string, object>dadosConta = GetDadosNovaConta("CORRENTE");
            if(!dadosConta.Any() || dadosConta.Count < 2){return;} // Se não retornar a quantidade que se espera, já sai

            Cliente novoTitularConta = listaClientes[(int) dadosConta["indiceCliente"] - 1];
            ContaCorrente novaContaCorrente = new ContaCorrente((int) dadosConta["numeroConta"], novoTitularConta);

            AdicionarContaLista(novoTitularConta, (int) dadosConta["numeroConta"], novaContaCorrente);
            return;
        }

        public void RegistrarCliente()
        {
            Interface.DecorarModulo("CADASTRO DE CLIENTE");

            #region Entrada de Dados Cliente
            Console.Write("Informe o nome do cliente: ");
            string nome = Console.ReadLine();
            Console.Write("Informe o sobrenome do cliente: ");
            string sobrenome = Console.ReadLine();
            Console.Write("Informe o CPF do cliente: ");
            string cpf = Console.ReadLine();

            if(!Geral.ValidarCPF(cpf)){
                Console.WriteLine("\nO CPF deve ter exatamente 11 caracteres.\n");
                return;
            }

            Console.Write("Informe a data de nascimento do cliente (AAAA-MM-DD): ");
            DateOnly dataNascimento = DateOnly.Parse(Console.ReadLine());
            #endregion

            Cliente novoCliente = new Cliente(nome, sobrenome, cpf, dataNascimento);
            AdicionarClienteLista(novoCliente);
        }

        public void AdicionarClienteLista(Cliente novoCliente)
        {
            if(ExisteClientesCadastrados()){
                for(int i = 0; i < listaClientes.Count; i++)
                {
                    if(listaClientes[i].Cpf == novoCliente.Cpf)
                    {
                        Console.WriteLine("\nJá existe um cliente com esse CPF cadastrado.\n");
                        return;
                    }
                }
            }

            listaClientes.Add(novoCliente);
            Console.WriteLine("\nCliente cadastrado com sucesso.\n");
        }

        public void AdicionarContaLista(Cliente novoTitularConta, int numero, Conta novaConta)
        {
            if(ExisteContasCadastradas()){
                for(int i = 0; i < listaContas.Count; i++)
                {
                    if(listaContas[i].Titular == novoTitularConta || listaContas[i].Numero == numero)
                    {
                        Console.WriteLine("\nJá existe uma conta com esse número ou desse titular.\n");
                        return;
                    }
                }
            }

            listaContas.Add(novaConta);
            Console.WriteLine("\nConta adicionada com sucesso!\n");
        }

        #region Ações Genéricas de Contas
        //Essa função é genérica, sendo responsável por chamar métodos de buscar a conta e pegar dados (saque e depósito)
        public Conta IntermediadorAcaoConta(string tituloInterface, string nomeAcao, Type tipoConta)
        {
            Interface.DecorarModulo(tituloInterface);
            if (!ExisteContasCadastradas())
            {
                Console.WriteLine("Não existe contas cadastradas ainda.\n");
                return null;
            } 

            Console.Write($"Informe o número da conta para {nomeAcao}: ");
            int numero = int.Parse(Console.ReadLine());

            Conta contaEncontrada = Geral.BuscarContas(numero, ExisteContasCadastradas(), listaContas, tipoConta);
            return contaEncontrada;
        }
        public void Depositar(string titulo, Type tipoConta)
        {
            Conta contaBuscada = IntermediadorAcaoConta(titulo, "depósito", tipoConta);
            if(contaBuscada != null){
                Console.Write("Informe o valor de depósito: ");
                double valorDeposito = double.Parse(Console.ReadLine());

                try{
                    contaBuscada.Depositar(valorDeposito);
                    Console.WriteLine($"Depósito realizado com sucesso. Saldo atual: {contaBuscada.Saldo:F2}\n");

                } catch(Exception ex){
                    Console.WriteLine(ex.Message);
                }
                
                return;
            }

            return;
        }

        public void Sacar(string titulo, Type tipoConta)
        {
            Conta contaBuscada = IntermediadorAcaoConta(titulo, "saque", tipoConta);
            if(contaBuscada != null){
                Console.Write("Informe o valor de saque: ");
                double valorSaque = double.Parse(Console.ReadLine());

                try{
                    contaBuscada.Sacar(valorSaque);
                    Console.WriteLine($"\nSaque realizado com sucesso. Saldo atual: {contaBuscada.Saldo:F2}\n");

                } catch(Exception ex){
                    Console.WriteLine(ex.Message);
                }
            }

            return; 
        }
        #endregion

        public void ChamarImprimirExtrato(Type tipoConta)
        {
            Console.Write("Informe o número da conta que deseja consultar o extrato: ");
            int numeroConta = int.Parse(Console.ReadLine());

            Conta conta = Geral.BuscarContas(numeroConta, ExisteContasCadastradas(), listaContas, tipoConta);
            if(conta == null)
            {
                Console.WriteLine("Conta não encontrada\n");
                return;
            }

            conta.imprimirExtrato();
        }

        public void ExibirListaClientes()
        {
            if(listaClientes.Count != 0)
            {
                Cliente clienteAtual;
                for(int i = 0; i < listaClientes.Count; i++)
                {
                    clienteAtual = listaClientes[i];
                    Console.WriteLine($"{i + 1} - {clienteAtual.ExibirDados()}");
                }

                return;
            }

            Console.WriteLine("Não há clientes cadastrados\n");
            return;
        }
    }
}