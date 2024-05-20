namespace Questao5.Domain.Entities
{
    public class Idempotencia
    {
        public string ChaveIdempotencia { get; }
        public string Requisicao { get; }
        public string Resultado { get; }

        public Idempotencia(string chaveIdempotencia, string requisicao, string resultado)
        {
            ChaveIdempotencia = chaveIdempotencia;
            Requisicao = requisicao;
            Resultado = resultado;
        }
    }
}
