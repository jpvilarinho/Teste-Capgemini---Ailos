using NUnit.Framework;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Tests.Infrastructure.Data
{
    [TestFixture]
    public class DatabaseBootstrapTest
    {
        
        private DatabaseConfig _databaseConfig = null!;

        [SetUp]
        public void Setup()
        {
            _databaseConfig = new DatabaseConfig { Name = "DataSource=:memory:" };
        }

        [Test]
        public async Task InitializeDatabaseAsync_ValidParameters_ReturnsTrue()
        {
            // Arrange
            var databaseBootstrap = new DatabaseBootstrap(_databaseConfig);

            // Act
            var result = await databaseBootstrap.InitializeDatabaseAsync(CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
