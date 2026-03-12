using Enums;

namespace Models{
    public class ContaCorrente : Conta
    {
        #region Atributos
        public double Limite => CalcularLimite();
        #endregion
        public ContaCorrente(int numero, Cliente titular) : base(numero, titular){}

        private double CalcularLimite()
        {
            if(Saldo <= 0)
            {
                return 0;
            }

            return Saldo / 2;
        }

        public double CalcularTaxaSaque(double valorSaque)
        {
            return valorSaque * 0.05;
        }

        public override void Sacar(double valorSaque)
        {
            if(valorSaque <= 0)
            {
                throw new ArgumentOutOfRangeException("O valor para saque precisa ser positivo");

            } else if((Saldo + Limite) < valorSaque)
            {
                throw new InvalidOperationException(
                    $"Saldo insuficiente para saque. Saldo atual com limite: R${(Saldo + Limite):F2}"
                );
            }

            Saldo -= valorSaque + CalcularTaxaSaque(valorSaque);
            RegistrarMovimentao(Enums.TipoMovimentacao.SAQUE, (valorSaque + CalcularTaxaSaque(valorSaque)));
        }
    }
}