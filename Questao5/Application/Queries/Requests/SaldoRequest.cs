using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests
{
    public class SaldoRequest : IRequest<SaldoResponse>
    {
        public string IdContaCorrente { get; }

        public SaldoRequest(string idContaCorrente)
        {
            IdContaCorrente = idContaCorrente;
        }
    }
}
