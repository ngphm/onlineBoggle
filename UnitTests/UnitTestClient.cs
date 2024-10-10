using Boggle.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBoggle
{
    [TestClass]
    public class UnitTestClient
    {
        
        [TestMethod]
        public void StartClientTest()
        {
            // Arrange
            Client client = new Client();

            // Act
            String address = null;
            var result = client.StartClient(address);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void StartGameTest()
        {
            // Arrange
            Client client = new Client();

            // Act
            var result = client.StartGame;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void playGameTest()
        {
            // Arrange
            Client client = new Client();

            // Act
            var result = client.playGame;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SendScoreTest()
        {
            // Arrange
            Client client = new Client();
            int score = 1;

            // Act
            var result = client.SendScore;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
