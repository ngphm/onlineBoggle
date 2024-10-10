using Boggle.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Boggle.Controllers
{
    /**
     * This class represents a Boggle board, a 4x4 grid of dice and contains
     * functions making API calls and checking if a word exists in the 
     * dictionary files.
     **/
    [ApiController]
    [Route("[controller]")]
    public class BoardController : ControllerBase
    {
        public Die[,] State { get; set; } = new Die[4, 4];

        /**
         * Creates a 4x4 array of randomized dice
         */
        public BoardController()
        {
            State[0, 0] = new("rifobx");
            State[0, 1] = new("ifehey");
            State[0, 2] = new("denows");
            State[0, 3] = new("utoknd");
            State[1, 0] = new("hmsrao");
            State[1, 1] = new("lupets");
            State[1, 2] = new("acitoa");
            State[1, 3] = new("ylgkue");
            State[2, 0] = new("qbmjoa");
            State[2, 1] = new("ehispn");
            State[2, 2] = new("vetign");
            State[2, 3] = new("baliyt");
            State[3, 0] = new("ezavnd");
            State[3, 1] = new("ralesc");
            State[3, 2] = new("uwilrg");
            State[3, 3] = new("pacemd");             
        }

        [HttpGet]
        public ObjectResult Get()
        {
            try
            {
                Board board = new(this.State);
                return Ok(board);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ObjectResult Post([FromBody] Word Word)
        {
            try
            {
                Board board = new(this.State);
                int Score = 0;
                if (Word.Letters.Length < 3)
                {
                    Score = 0;
                }
                else if (Word.Letters.Length < 5)
                {
                    Score = 1;
                }
                else if (Word.Letters.Length < 7)
                {
                    Score = Word.Letters.Length - 3;
                }
                else if (Word.Letters.Length == 7)
                {
                    Score = 5;
                }
                else if (Word.Letters.Length > 7)
                {
                    Score = 11;
                }
                if(!WordInDictionary(Word.Letters))
                {
                    Score = 0;
                }
                return Ok(new { score = Score });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Returns true if word exists in word list files
        public bool WordInDictionary(string word)
        {
            StringBuilder fileName = new StringBuilder();
            fileName.Append("Word lists in csv/");
            fileName.Append(Char.ToUpper(word[0]));
            fileName.Append("word.csv");
            string wordAsString = new string(word);
            if (word != null && word.Length > 1)
            {
                string[] allWords = System.IO.File.ReadAllLines(fileName.ToString());
                for (int i = 0; i < allWords.Length; i++)
                {
                    string wordInFile = allWords[i];
                    wordInFile = wordInFile.Remove(wordInFile.Length - 1); //removes the space character at the end of every word in file, leaving just the word
                    if (wordInFile == wordAsString)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
