using Model;

public class ContaPoupanca : Conta
{
    #region Atributos
    public double TaxaRendimento => 0.02;
    #endregion
    public ContaPoupanca(int numero, Cliente titular) : base(numero, titular){}

    public void RenderJuros()
    {
        Saldo += Saldo * TaxaRendimento;
        Console.WriteLine("Juros aplicados com sucesso.");
    }
}