using MediatR;
using Dapper;
using System.Data;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using System.Linq;
using System.Net.Http;

namespace Questao5.Application.Handlers
{
    public class SaldoHandler : IRequestHandler<SaldoRequest, SaldoResponse>
    {
        private readonly IDbConnection _dbConnection;

        public SaldoHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<SaldoResponse> Handle(SaldoRequest request, CancellationToken cancellationToken)
        {
            var contaCorrente = await _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(
                "SELECT * FROM contacorrente WHERE idcontacorrente = @IdContaCorrente", new { request.IdContaCorrente });

            if (contaCorrente == null)
                throw new HttpRequestException("INVALID_ACCOUNT");

            if (!contaCorrente.Ativo)
                throw new HttpRequestException("INACTIVE_ACCOUNT");

            var creditos = await _dbConnection.QueryAsync<decimal>(
                "SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @IdContaCorrente AND tipomovimento = 'C'", new { request.IdContaCorrente });
            var debitos = await _dbConnection.QueryAsync<decimal>(
                "SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @IdContaCorrente AND tipomovimento = 'D'", new { request.IdContaCorrente });

            var saldo = creditos.Sum() - debitos.Sum();

            return new SaldoResponse
            {
                Numero = contaCorrente.Numero.ToString(),
                Nome = contaCorrente.NomeTitular,
                DataConsulta = DateTime.Now,
                Saldo = saldo
            };
        }
    }
}
