namespace Model
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

        private Cliente(string nome, string sobrenome, string cpf, DateOnly datanascimento)
        {
            Nome = nome;
            SobreNome = sobrenome;
            Cpf = cpf;
            DataNascimento = datanascimento;
        }

        public int CalcularIdade()
        {   
            DateTime hoje = DateTime.Now;
            int idade = hoje.Year - DataNascimento.Year;

            if(hoje.Month < DataNascimento.Month || hoje.Month == DataNascimento.Month && hoje.Day < DataNascimento.Day)
            {
                idade--;
            }

            return idade;
        }
    }
}