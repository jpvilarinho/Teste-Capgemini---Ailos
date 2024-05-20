using NUnit.Framework;
using Questao5.Domain.Entities;

namespace Questao5.Tests.Domain.Entities
{
    [TestFixture]
    public class IdempotenciaTest
    {
        [Test]
        public void Constructor_ValidParameters_CreatesInstance()
        {
            // Arrange
            var chaveIdempotencia = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            var requisicao = "Some request data";
            var resultado = "Some result data";

            // Act
            var idempotencia = new Idempotencia(chaveIdempotencia, requisicao, resultado);

            // Assert
            Assert.IsNotNull(idempotencia);
            Assert.AreEqual(chaveIdempotencia, idempotencia.ChaveIdempotencia);
            Assert.AreEqual(requisicao, idempotencia.Requisicao);
            Assert.AreEqual(resultado, idempotencia.Resultado);
        }

        [Test]
        public void Constructor_NullChaveIdempotencia_ThrowsArgumentNullException()
        {
            // Arrange
            string chaveIdempotencia = string.Empty;
            var requisicao = "Some request data";
            var resultado = "Some result data";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Idempotencia(chaveIdempotencia, requisicao, resultado));
        }

        [Test]
        public void Constructor_NullRequisicao_ThrowsArgumentNullException()
        {
            // Arrange
            var chaveIdempotencia = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            string requisicao = string.Empty;
            var resultado = "Some result data";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Idempotencia(chaveIdempotencia, requisicao, resultado));
        }

        [Test]
        public void Constructor_NullResultado_ThrowsArgumentNullException()
        {
            // Arrange
            var chaveIdempotencia = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            var requisicao = "Some request data";
            string resultado = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Idempotencia(chaveIdempotencia, requisicao, resultado));
        }

    }
}
