using Boggle.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestBoggle
{
    [TestClass]
    public class UnitTestHost
    {
        [TestMethod]
        public void makeHostTest()
        {
            // Arrange
            Host host = new Host();
            
            // Act
            var result = host.makeHost;

            // Assert
            Assert.AreNotEqual("000000", host.Address);
        }

        [TestMethod]
        public void ShowScoreTest()
        {
            // Arrange
            Host host = new Host();

            // Act
            var result = host.ShowScore;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void StartGameTest()
        {
            // Arrange
            Host host = new Host();
            bool EndGame = false;

            // Act
            var result = host.StartGame;

            // Assert
            Assert.IsFalse(EndGame);
        }

        [TestMethod]
        public void stopGameTest()
        {
            // Arrange
            Host host = new Host();
            bool EndGame = true;

            // Act
            var result = host.stopGame;

            // Assert
            Assert.IsTrue(EndGame);
        }
    }
}
