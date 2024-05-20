using NUnit.Framework;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;
using System.Data;

namespace Questao5.Tests.Application.Handlers
{
    [TestFixture]
    public class MovimentoHandlerTest
    {
        private Mock<IDbConnection> _mockDbConnection;

        [SetUp]
        public void Setup()
        {
            _mockDbConnection = new Mock<IDbConnection>();
        }

        [Test]
        public async Task Handle_ValidRequest_ReturnsMovimentoResponse()
        {
            // Arrange
            var request = new MovimentoRequest
            {
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                Valor = 100.00m,
                TipoMovimento = "C" // Tipo de movimento válido (C = Crédito)
            };
            var handler = new MovimentoHandler(_mockDbConnection.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOf<MovimentoResponse>(response);
        }

        [Test]
        public async Task Handle_InvalidRequest_InvalidMovementType_ReturnsNullMovimentoResponse()
        {
            // Arrange
            var request = new MovimentoRequest
            {
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                Valor = 100.00m,
                TipoMovimento = "X"
            };
            var handler = new MovimentoHandler(_mockDbConnection.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNull(response);
        }

        [Test]
        public async Task Handle_InvalidRequest_NegativeValue_ReturnsNullMovimentoResponse()
        {
            // Arrange
            var request = new MovimentoRequest
            {
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                Valor = -100.00m,
                TipoMovimento = "C"
            };
            var handler = new MovimentoHandler(_mockDbConnection.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNull(response);
        }

        [Test]
        public async Task Handle_NonexistentContaCorrente_ReturnsNullMovimentoResponse()
        {
            // Arrange
            var request = new MovimentoRequest
            {
                IdContaCorrente = "nonexistentId",
                Valor = 100.00m,
                TipoMovimento = "C"
            };
            var mockContaCorrenteRepository = new Mock<IContaCorrenteRepository>();
            mockContaCorrenteRepository.Setup(repo => repo.
            GetContaCorrenteByIdAsync(It.IsAny<string>()))
            .ReturnsAsync((ContaCorrente?)null);

            var handler = new MovimentoHandler(_mockDbConnection.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNull(response);
        }

    }
}
