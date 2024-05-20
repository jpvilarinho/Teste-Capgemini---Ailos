using MediatR;
using Dapper;
using System.Data;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Newtonsoft.Json;

namespace Questao5.Application.Handlers
{
    public class MovimentoHandler : IRequestHandler<MovimentoRequest, MovimentoResponse>
    {
        private readonly IDbConnection _dbConnection;

        public MovimentoHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<MovimentoResponse> Handle(MovimentoRequest request, CancellationToken cancellationToken)
        {
            var contaCorrente = await _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(
                "SELECT * FROM contacorrente WHERE idcontacorrente = @IdContaCorrente", new { request.IdContaCorrente });

            if (contaCorrente == null)
                throw new HttpRequestException("INVALID_ACCOUNT");

            if (!contaCorrente.Ativo)
                throw new HttpRequestException("INACTIVE_ACCOUNT");

            if (request.Valor <= 0)
                throw new HttpRequestException("INVALID_VALUE");

            if (request.TipoMovimento != "C" && request.TipoMovimento != "D")
                throw new HttpRequestException("INVALID_TYPE");

            var idempotencia = await _dbConnection.QueryFirstOrDefaultAsync<Idempotencia>(
                "SELECT * FROM idempotencia WHERE chave_idempotencia = @IdempotencyKey", new { request.IdempotencyKey });

            if (idempotencia != null)
            {
                if (idempotencia.Resultado != null)
                {
                    try
                    {
                        var existingResponse = JsonConvert.DeserializeObject<MovimentoResponse>(idempotencia.Resultado);
                        if (existingResponse != null)
                        {
                            return existingResponse;
                        }
                    }
                    catch (JsonException)
                    {
                        throw new HttpRequestException("INVALID_IDEMPOTENCY_RESPONSE");
                    }
                }
            }

            var idMovimento = Guid.NewGuid().ToString();
            await _dbConnection.ExecuteAsync(
                "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)",
                new { IdMovimento = idMovimento, request.IdContaCorrente, DataMovimento = DateTime.Now.ToString("dd/MM/yyyy"), request.TipoMovimento, request.Valor });

            var response = new MovimentoResponse { IdMovimento = idMovimento };
            await _dbConnection.ExecuteAsync(
                "INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@IdempotencyKey, @Requisicao, @Resultado)",
                new { request.IdempotencyKey, Requisicao = JsonConvert.SerializeObject(request), Resultado = JsonConvert.SerializeObject(response) });

            return response;
        }
    }
}
