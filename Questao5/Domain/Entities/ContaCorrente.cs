namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public string Id { get; }
        public int Numero { get; }
        public string NomeTitular { get; }
        public bool Ativo { get; }

        public ContaCorrente(string id, int numero, string nomeTitular)
        {
            if (numero <= 0)
            {
                throw new ArgumentException("O número da conta deve ser maior que zero.", nameof(numero));
            }

            if (string.IsNullOrEmpty(nomeTitular))
            {
                throw new ArgumentException("O nome do titular da conta não pode ser nulo ou vazio.", nameof(nomeTitular));
            }

            Id = id;
            Numero = numero;
            NomeTitular = nomeTitular;
            Ativo = true;
        }
    }
}
