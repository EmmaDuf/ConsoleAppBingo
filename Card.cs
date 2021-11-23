using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBingo
{
    internal class Card
    {
        //B I N G O 
        public List<Square> cardsquares = new List<Square>();
        public string Owner { get; set; }
        //Player Card is populated by referencing MasterCard.bingo_columns
        public Card()
        {
            MasterCard master = new MasterCard();
            Random random = new Random();
            int count = 0;
            //for each letter
            for (int i = 0; i < 5; i++)
            {
                int five_in_column = 0;
                //pick 5 numbers in the letter range
                while(five_in_column < 5){
                    List<Square> subset = master.mastersquares.GetRange(count, 15);
                    int index = random.Next(0, 14);
                    Square picked_square = subset[index];
                    if (cardsquares.Contains(picked_square)) { }
                    else
                    {
                        cardsquares.Add(picked_square);
                        five_in_column++;
                    }
                }   
                count += 15;
            } 
        }
        public Card(string owner) : this()
        {
            Owner = owner;
        }
    }

}
