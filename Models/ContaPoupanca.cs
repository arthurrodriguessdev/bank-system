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

    public override void ImprimirExtrato()
    {
        Console.WriteLine($"\n===== EXTRATO DA CONTA {Numero} =====");

        Console.WriteLine("\n--- DADOS DA CONTA ---");
        Console.WriteLine("TIPO DE CONTA: POUPANÇA");
        Console.WriteLine($"SALDO ATUAL: {Saldo:F2}");
        Console.WriteLine($"TAXA DE RENDIMENTO: {TaxaRendimento}");

        Console.WriteLine("\n--- DADOS DO TITULAR ---");
        Console.WriteLine($"NOME: {Titular.Nome}");
        Console.WriteLine($"CPF: {Titular.Cpf}");

        Console.WriteLine("\n==============================\n");
    }
}