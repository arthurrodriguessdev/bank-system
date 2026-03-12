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
            RegistrarMovimentao(Enums.TipoMovimentacao.SAQUE, valorSaque);
        }

        public void Depositar(double valorDeposito)
        {
            if(valorDeposito <= 0)
            {
                throw new ArgumentOutOfRangeException("O valor do depósito deve ser maior que zero.");
            } 
            
            if(valorDeposito < VALOR_MINIMO_DEPOSITO){
                throw new InvalidOperationException($"O valor mínimo para depósito é de R${VALOR_MINIMO_DEPOSITO}");
            }

            Saldo += valorDeposito;
            Console.WriteLine(Saldo);

            // Registrando a movimentação de depósito
            RegistrarMovimentao(Enums.TipoMovimentacao.DEPOSITO, valorDeposito);
        }

        public void Transferir(Conta contaDestino, double valorTransferir){
            if(contaDestino == null){
                throw new ArgumentNullException("A conta de destino não foi encontrada.");
            }

            else if(valorTransferir > Saldo){
                throw new InvalidOperationException("A conta de origem não possui esse valor para transferir");
            }

            Saldo -= valorTransferir;
            contaDestino.Depositar(valorTransferir);
        }

        public void RegistrarMovimentao(TipoMovimentacao tipo, double valor = 0){
            Movimentacao novaMovimentacao;

            if(tipo != Enums.TipoMovimentacao.ABERTURA_CONTA && valor <= 0){
                throw new ArgumentException("Movimentações devem possuir valor.");

            } else if(tipo == Enums.TipoMovimentacao.ABERTURA_CONTA){
                novaMovimentacao = new Movimentacao(tipo);
            }
            
            else{
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
        }
    }
}