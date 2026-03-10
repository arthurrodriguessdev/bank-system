using Enums;

namespace Models{
    public class Movimentacao{
        private DateTime DataHoraMovimentacao {get; set;}
        private TipoMovimentacao TipoMovimentacao {get; set;}
        private double ValorMovimentacao {get; set;}
        
        // Construtor utilizado para ações: DEPOSITO, SAQUE E TRANSFERENCIA
        public Movimentacao(TipoMovimentacao tipoMovimentacao, double valorMovimentacao){
            DataHoraMovimentacao = DateTime.Now;
            TipoMovimentacao = tipoMovimentacao;
            ValorMovimentacao = valorMovimentacao;
        }

        // Construtor utilizado para ABERTURA_CONTA
        public Movimentacao(TipoMovimentacao tipoMovimentacao){
            DataHoraMovimentacao = DateTime.Now;
            TipoMovimentacao = tipoMovimentacao;
        }

        public override string ToString()
        {   
            string data = DataHoraMovimentacao.ToString("dd/MM/yyyy");
            string horasMinutos = DataHoraMovimentacao.ToString("HH:mm:ss");

            if(ValorMovimentacao == null){
                string valor = $"{ValorMovimentacao}";

                if(this.TipoMovimentacao == TipoMovimentacao.SAQUE){
                    valor = $"-{ValorMovimentacao}";

                } else if(this.TipoMovimentacao == TipoMovimentacao.DEPOSITO){
                    valor = $"+{ValorMovimentacao}";
                }

                return $"{data} às {horasMinutos}h | {TipoMovimentacao} - R$ {valor}";
            } 

            // Ação de: ABERTURA_CONTA
            return $"{data} às {horasMinutos}h | {TipoMovimentacao}";
        }
    }
}