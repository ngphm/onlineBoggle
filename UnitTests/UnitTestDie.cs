using Boggle.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBoggle
{
    [TestClass]
    public class UnitTestDie
    {
        [TestMethod]
        public void getRolledLetterTest()
        {
            // Arrange
            string letters = "abcdef";
            Die dice = new Die(letters);

            // Act
            string result = dice.GetRolledLetter().ToString();

            // Assert
            Assert.AreNotEqual("k", result); 
        }

        [TestMethod]
        public void getRolledLetterTest1()
        {
            // Arrange
            string letters = "aaaaaa";
            Die dice = new Die(letters);

            // Act
            string result = dice.GetRolledLetter().ToString();

            // Assert
            Assert.AreEqual("a", result);
        }

        [TestMethod]
        public void getRolledLetterTest2()
        {
            // Arrange
            string letters = "qwerty";
            Die dice = new Die(letters);

            // Act
            string result = dice.GetRolledLetter().ToString();

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
