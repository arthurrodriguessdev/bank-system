using Model;

public class Conta
{
    #region Atributos
    public int Numero {get; private set;}
    public double Saldo {get; protected set;}
    public Cliente Titular {get; private set;}
    public DateTime DataAbertura {get; private set;}
    private readonly double VALOR_MINIMO_DEPOSITO = 10.0;
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
            Console.WriteLine("\nValor inválido.\n");
            return;
        }

        if(valorSaque > Saldo)
        {
            Console.WriteLine($"\nSaldo insuficiente. Saldo atual: {Saldo:F2}\n");
            return;
        }

        Saldo -= valorSaque; 
        Console.WriteLine($"\nSaque realizado com sucesso. Saldo atual: {Saldo:F2}\n");
    }

    public void Depositar(double valorDeposito)
    {
        if(valorDeposito <= 0)
        {
            throw new ArgumentOutOfRangeException("O valor do depósito deve ser maior que zero.");
        } 
        
        if(valorDeposito < VALOR_MINIMO_DEPOSITO){
            throw new InvalidOperationException($"O valor mínimo para depósit é de R${VALOR_MINIMO_DEPOSITO}");
        }

        Saldo += valorDeposito;
        Console.WriteLine($"Depósito realizado com sucesso. Saldo atual: {Saldo:F2}\n");
    }
}