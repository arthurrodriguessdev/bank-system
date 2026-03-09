using Model;

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
            Console.WriteLine("Valor inválido.");
            return;

        } else if((Saldo + Limite) < valorSaque)
        {
            Console.WriteLine($"Valor indisponível para saque. Saldo com limite aplicado: R${(Saldo + Limite):F2}");
            return;
        }

        Saldo -= valorSaque + CalcularTaxaSaque(valorSaque); 
        Console.WriteLine($"Saque realizado com sucesso. Saldo com limite aplicado: R${(Saldo + Limite):F2}");
    }
}