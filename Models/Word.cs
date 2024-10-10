namespace Boggle.Models
{
    /**
     * This class represents a word in Boggle
     */
    public class Word
    {
        public string Letters {get; set;}

        public Word(string Letters) {
            this.Letters = Letters;
        }
    }
}
