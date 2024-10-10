using Boggle.Controllers;
using Boggle.Models;
using Microsoft.AspNetCore.Mvc;


// testing BoardController.cs
// place "Word lists in csv" folder inside test project
namespace TestProject1
{
    [TestClass]
    public class UnitTestBoardController
    {
        [TestMethod]
        public void initializeDiceTest()
        {
            // Arrange
            Die[,] dice = new Die[4, 4]; 

            // Act
            var state = dice.Length;

            // Assert
            Assert.AreEqual(16, state);
        }

        [TestMethod]
        public void initializeDiceValueTest()
        {
            // Arrange
            BoardController boardController = new BoardController();

            // Act
            var state = boardController.State;
            state[0, 0] = new("rifobx");

            // Assert
            Assert.IsNotNull(state);

        }

        [TestMethod]
        public void exceptionGetBadRequestTest()
        {
            // Arrange
            var boardController = new BoardController();

            // Act
            var result = boardController.Get() as BadRequestObjectResult;

            // Assert
            Assert.IsNull(result);
        }


        [TestMethod]
        public void postWordTest()
        {
            // Arrange
            Word word = new("word");
            BoardController boardController = new BoardController();

            // Act
            var result = boardController.Post(word);

            // Assert
            Assert.IsNotNull(result);

        }


        [TestMethod]
        public void wordInDictionaryTest()
        {
            // Arrange
            string testWord = "word";
            BoardController boardController = new BoardController();

            // Act
            var result = boardController.WordInDictionary(testWord);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void wordNotInDictionaryTest()
        {
            // Arrange
            string testWord = "wodr";
            BoardController boardController = new BoardController();

            // Act
            var result = boardController.WordInDictionary(testWord);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
