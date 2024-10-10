using System.Runtime.CompilerServices;

namespace Boggle.Controllers
{
    /**
     * This class represents a 6 sided die of letters
     **/
    public class Die
    {
        public string? Letters { get; set; }
        public int LetterIndex { get; set; }

        public Die(string Letters)
        {
            this.Letters = Letters;
            this.LetterIndex = new Random().Next(6);
        }

        //Returns the rolled letter aka letter on the side of the die facing up
        public char GetRolledLetter()
        {
            return Letters![LetterIndex];
        }
    }
}
