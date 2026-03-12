using System.Collections.Generic;
using Enums;

namespace Models{
    public abstract class Conta
    {
        #region Atributos
        public int Numero {get; private set;}
        public double Saldo {get; protected set;}
        public Cliente Titular {get; private set;}
        public DateTime DataAbertura {get; private set;}
        private readonly double VALOR_MINIMO_DEPOSITO = 10.0;
        public List<Movimentacao>listaMovimentacoes {get; private set;}
        #endregion

        public Conta(int numero, Cliente titular)
        {
            Numero = numero;
            Titular = titular;
            Saldo = 0.0;
            DataAbertura = DateTime.Now;
            listaMovimentacoes = new List<Movimentacao>();
            RegistrarMovimentao(TipoMovimentacao.ABERTURA_CONTA);
        }

        public virtual void Sacar(double valorSaque)
        {
            if(valorSaque <= 0)
            {
                throw new ArgumentOutOfRangeException("O valor para saque precisa ser positivo");
            }

            if(valorSaque > Saldo)
            {
                throw new InvalidOperationException($"Saldo insuficiente. Saldo atual: {Saldo:F2}");
            }

            Saldo -= valorSaque; 

            // Registrando a movimentação de saque
            RegistrarMovimentao(TipoMovimentacao.SAQUE, valorSaque);
        }

        public void Depositar(double valorDeposito)
        {
            if(valorDeposito <= 0)
            {
                throw new ArgumentOutOfRangeException("\nO valor do depósito deve ser maior que zero.\n");
            } 
            
            if(valorDeposito < VALOR_MINIMO_DEPOSITO){
                throw new InvalidOperationException($"\nO valor mínimo para depósito é de R${VALOR_MINIMO_DEPOSITO}\n");
            }

            Saldo += valorDeposito;
            Console.WriteLine(Saldo);

            // Registrando a movimentação de depósito
            RegistrarMovimentao(TipoMovimentacao.DEPOSITO, valorDeposito);
        }

        public void Transferir(Conta contaDestino, double valorTransferir){
            if(contaDestino == null){
                throw new ArgumentNullException("\nA conta de destino não foi encontrada.\n");
            }

            else if(valorTransferir > Saldo){
                throw new InvalidOperationException("\nA conta de origem não possui esse valor para transferir\n");
            }

            Saldo -= valorTransferir;
            contaDestino.Depositar(valorTransferir);
            RegistrarMovimentao(TipoMovimentacao.TRANSFERENCIA, valorTransferir);
        }

        public void RegistrarMovimentao(TipoMovimentacao tipo, double valor = 0){
            Movimentacao novaMovimentacao;

            if (tipo == TipoMovimentacao.ABERTURA_CONTA)
            {
                novaMovimentacao = new Movimentacao(tipo);
            }
            else
            {
                if (valor <= 0)
                    throw new ArgumentException("\nMovimentações devem possuir valor.\n");
                novaMovimentacao = new Movimentacao(tipo, valor);
            }

            listaMovimentacoes.Add(novaMovimentacao);
        }

        public void imprimirExtrato()
        {
            if(listaMovimentacoes.Count <= 0)
            {
                return;
            }

            Console.WriteLine("=== EXTRATO DA CONTA ===\n");
            for(int i = 0; i < listaMovimentacoes.Count ; i++)
            {
                Console.WriteLine($"{listaMovimentacoes[i]}");
            }
            Console.Write("\n");
        }
    }
}