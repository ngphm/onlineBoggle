using Boggle.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBoggle
{
    [TestClass]
    public class UnitTestMultigame
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
    }
}
