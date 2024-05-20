using NUnit.Framework;
using Questao5.Domain.Entities;

namespace Questao5.Tests.Domain.Entities
{
    [TestFixture]
    public class MovimentoTest
    {
        [Test]
        public void Constructor_ValidParameters_CreatesInstance()
        {
            // Arrange
            var id = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            var idContaCorrente = "FA99D033-7067-ED11-96C6-7C5DFA4A16C9";
            var dataMovimento = DateTime.Parse("2024-05-10");
            var tipoMovimento = 'C';
            var valor = 100.00m;

            // Act
            var movimento = new Movimento(id, idContaCorrente, dataMovimento, tipoMovimento, valor);

            // Assert
            Assert.IsNotNull(movimento);
            Assert.AreEqual(id, movimento.Id);
            Assert.AreEqual(idContaCorrente, movimento.IdContaCorrente);
            Assert.AreEqual(dataMovimento, movimento.DataMovimento);
            Assert.AreEqual(tipoMovimento, movimento.TipoMovimento);
            Assert.AreEqual(valor, movimento.Valor);
        }

        [Test]
        public void Constructor_InvalidTipoMovimento_ThrowsArgumentException()
        {
            // Arrange
            var id = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            var idContaCorrente = "FA99D033-7067-ED11-96C6-7C5DFA4A16C9";
            var dataMovimento = DateTime.Parse("2024-05-10");
            var tipoMovimento = 'X';

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Movimento(id, idContaCorrente, dataMovimento, tipoMovimento, 100.00m));
        }

        [Test]
        public void Constructor_NegativeValue_ThrowsArgumentException()
        {
            // Arrange
            var id = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            var idContaCorrente = "FA99D033-7067-ED11-96C6-7C5DFA4A16C9";
            var dataMovimento = DateTime.Parse("2024-05-10");
            var tipoMovimento = 'C';
            var valor = -100.00m;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Movimento(id, idContaCorrente, dataMovimento, tipoMovimento, valor));
        }

    }
}
