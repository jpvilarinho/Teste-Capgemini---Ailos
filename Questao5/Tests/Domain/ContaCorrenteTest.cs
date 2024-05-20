using NUnit.Framework;
using Questao5.Domain.Entities;

namespace Questao5.Tests.Domain.Entities
{
    [TestFixture]
    public class ContaCorrenteTest
    {
        [Test]
        public void Constructor_ValidParameters_CreatesInstance()
        {
            // Arrange
            var id = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            var numero = 123;
            var nomeTitular  = "Katherine Sanchez";

            // Act
            var contaCorrente = new ContaCorrente(id, numero, nomeTitular );

            // Assert
            Assert.IsNotNull(contaCorrente);
            Assert.AreEqual(id, contaCorrente.Id);
            Assert.AreEqual(numero, contaCorrente.Numero);
            Assert.AreEqual(nomeTitular , contaCorrente.NomeTitular);
        }

        [Test]
        public void Constructor_NegativeNumero_ThrowsArgumentException()
        {
            // Arrange
            var id = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            var numero = -123;
            var nomeTitular = "Katherine Sanchez";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new ContaCorrente(id, numero, nomeTitular));
        }

        [Test]
        public void Constructor_NullNome_ThrowsArgumentNullException()
        {
            // Arrange
            var id = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            var numero = 123;
            string nomeTitular = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ContaCorrente(id, numero, nomeTitular));
        }

    }
}
