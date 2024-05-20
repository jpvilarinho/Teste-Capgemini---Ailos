using NUnit.Framework;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Application.Handlers;
using Questao5.Domain.Repositories;
using Questao5.Domain.Entities;
using System.Data;

namespace Questao5.Tests.Application.Handlers
{
    [TestFixture]
    public class SaldoHandlerTest
    {
        private Mock<IDbConnection> _mockDbConnection;

        [SetUp]
        public void Setup()
        {
            _mockDbConnection = new Mock<IDbConnection>();
        }

        [Test]
        public async Task Handle_ValidRequest_ReturnsSaldoResponse()
        {
            // Arrange
            var idContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            var request = new SaldoRequest(idContaCorrente);
            var handler = new SaldoHandler(_mockDbConnection.Object);

            handler = new SaldoHandler(_mockDbConnection.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOf<SaldoResponse>(response);
        }

        [Test]
        public async Task Handle_NonexistentContaCorrente_ReturnsNullSaldoResponse()
        {
            // Arrange
            var request = new SaldoRequest("nonexistentId");
            var mockContaCorrenteRepository = new Mock<IContaCorrenteRepository>();
            mockContaCorrenteRepository.Setup(repo => repo.GetContaCorrenteByIdAsync(It.IsAny<string>()))
                                       .ReturnsAsync((ContaCorrente?)null);
            var handler = new SaldoHandler(_mockDbConnection.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNull(response);
        }
    }
}
