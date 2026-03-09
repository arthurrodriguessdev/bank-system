using Model;

public class Conta
{
    #region Atributos
    public int Numero {get; private set;}
    public double Saldo {get; protected set;}
    public Cliente Titular {get; private set;}
    #endregion

    public Conta(int numero, Cliente titular)
    {
        Numero = numero;
        Titular = titular;
        Saldo = 0.0;
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
            Console.WriteLine("Valor inválido");
            return;
        }

        Saldo += valorDeposito;
        Console.WriteLine($"\nDepósito realizado com sucesso. Saldo atual: {Saldo:F2}\n");
    }
}