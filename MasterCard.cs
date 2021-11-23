using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBingo
{
    internal class MasterCard
    {
        //Seperating numbers by corresponding letter. each number corresponds to one letter
        public List<Square> mastersquares = new List<Square>();
        //{[1-15], [16-30], [31-45], [46-60], [61-75]}
        public List<IEnumerable<int>> bingo_columns = new List<IEnumerable<int>>() { Enumerable.Range(1, 15), Enumerable.Range(16, 15), Enumerable.Range(31, 15), Enumerable.Range(46, 15), Enumerable.Range(61, 15) };
        

        IEnumerable<int> column = Enumerable.Range(1,15);
        //Dictionary that holds the master card for one game: <number,ispicked>
        public Dictionary<int, bool> mastercard = new Dictionary<int, bool>();
        //How will you populate the mastercard to begin playing?
        public MasterCard()
        {
            for(int i = 1; i <76; i++)
            {
                Square square = new Square(i, false);
                mastersquares.Add(square);
            }
        }
    }
}
