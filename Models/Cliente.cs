namespace Models
{
    public class Cliente
    {
        #region Atributos 
        public string Nome {get; set;}
        public string SobreNome {get; set;}
        public string NomeCompleto => $"{Nome} {SobreNome}";
        public string Cpf {get; private set;}
        public DateOnly DataNascimento {get; private set;}
        public int Idade => CalcularIdade();
        #endregion

        public Cliente(string nome, string sobrenome, string cpf, DateOnly dataNascimento)
        {
            Nome = nome;
            SobreNome = sobrenome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        private int CalcularIdade()
        {   
            DateTime hoje = DateTime.Now;
            int idade = hoje.Year - DataNascimento.Year;

            if(hoje.Month < DataNascimento.Month || hoje.Month == DataNascimento.Month && hoje.Day < DataNascimento.Day)
            {
                idade--;
            }

            return idade;
        }

        public string ExibirDados()
        {
            return $"{Nome} {SobreNome} ({Cpf})";
        }
    }
}