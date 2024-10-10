using Boggle.Controllers;
using System.Runtime.CompilerServices;

namespace Boggle.Models
{
    /**
     * This class represents a Boggle board as an array of strings
     */
    public class Board
    {
        public List<string> State { get; set; } = new List<string>();

        public Board(Die[,] State)
        {
            foreach (Die die in State)
            {
                this.State.Add(die.GetRolledLetter().ToString());
            }
        }
    }
}
