using Model;

namespace Utils{
    public class Interface{
        public static void DecorarModulo(string nomeAcao)
        {
            Console.WriteLine($"\n===== {nomeAcao} =====");
            Console.WriteLine("----------------------------------\n");
        }
    }

    public class Geral
    {
        public static Conta BuscarContas(int numeroConta, bool existeContas, List<Conta>listaContas)
        {
            if (!existeContas)
            {
                Console.WriteLine("\nNão há contas cadastradas ainda.\n");
                return null;
            }

            Conta contaEncontrada = null;
            for(int i = 0; i < listaContas.Count; i++)
            {
                if(listaContas[i].Numero == numeroConta)
                {
                    contaEncontrada = listaContas[i];
                    break;
                }
            }

            if(contaEncontrada == null)
            {
                Console.WriteLine($"\nA conta de número {numeroConta} não foi encontrada.\n");
            }

            return contaEncontrada;
        }
    }
}