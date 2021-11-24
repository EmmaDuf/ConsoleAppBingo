using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBingo
{
    internal class GameSpace
    {
        //hold all cards
        public List<Card> Cards_In_Play = new List<Card>();
        //
        Random rolling = new Random();
        //hold mastercard copy of game (only one) //probably don't need this much information
        public static MasterCard mastercard = new MasterCard();
        //hold all played numbers
        public List<int> numbers_played = new List<int>();
        //Generates all possible numbers to be played
        public List<int> numbers_in_wheel = Enumerable.Range(1, 75).ToList();
        //check for a winner

        //grant unique cards to players and add it to cards in play
        public void CreateCard(string owner)
        {
            while(true)
            {
                Card card = new Card(owner);
                card.cardsquares.Sort((x, y) => x.Id.CompareTo(y.Id));
                if (!CardAlreadyInPlay(card))
                {
                    //freebie, middle number
                    card.cardsquares[12].Pick = true;
                    this.Cards_In_Play.Add(card);
                    break;
                }
            }  
        }
        public bool PlayRound()
        {
            //roll for a bingo number
            int roll = Roll();
            Console.Write("{0}|", roll);
            //update the master card (maybe)
            //update the player cards? (do sorting multiple times)
            UpdateCards(roll);
            return CheckForWinner();
            //check for winner
            //end game if there is a winner 
        }
        public int Roll()
        {
            
            //only roll numbers left to play
            int _index = this.rolling.Next(this.numbers_in_wheel.Count-1);
            int number = numbers_in_wheel[_index];
            numbers_played.Add(number);
            numbers_in_wheel.Remove(number);
            return number;            
        }
        public void UpdateCards(int roll)
        {
            foreach(Card card in this.Cards_In_Play)
            {
                foreach(Square square in card.cardsquares)
                {
                    if(square.Id == roll) { square.Pick = true; }
                }
            }
        }
        //Check all possible different ways to win
        //Assume numbers are in order (this means that the index means something)
        public bool CheckForWinner()
        {
            foreach(Card card in this.Cards_In_Play)
            {
                //check for four vertically (1-15,16-30,etc)
                for(int i = 0; i < 25; i+=5)
                {
                    int count = 0;
                    for (int j = 0; j < 5; j++)
                    {
                        if (card.cardsquares[i+j].Pick == true) { count++; }

                        //Console.Write("Number: {0}, Picked: {1}", card.cardsquares[j + i].Id, card.cardsquares[j + i].Pick);
                    }
                    if (count == 5)
                    {
                        Console.WriteLine();
                        Console.WriteLine("We have a vertical winner: {0}",card.Owner);
                        return true;
                    }
                }
                //check for four horizontally 
                for(int i = 0; i < 5; i++)
                {
                    int count = 0;
                    for(int j = 0; j< 25; j += 5)
                    {
                        if(card.cardsquares[i + j].Pick == true) { count++; }
                    }
                    if (count == 5)
                    {
                        Console.WriteLine();
                        Console.WriteLine("We have a horizontal winner: {0}", card.Owner);
                        return true;
                    }
                }
                //check for diagonal wins first option[0,6,12,18,25], second option[4,8,12,16,20]
                int _count = 0;
                for(int i = 0; i < 25; i += 6)
                {
                    if(card.cardsquares[i].Pick == true) { _count++; }
                }
                if (_count == 5) {
                    Console.WriteLine();
                    Console.WriteLine("Diagonal Win by: {0}", card.Owner);
                    return true;
                }
                _count = 0;
                for(int i = 5; i < 25; i += 4)
                {
                    if(card.cardsquares[i].Pick == true) { _count++; }
                }
                if (_count == 5)
                {
                    Console.WriteLine();
                    Console.WriteLine("Diagonal win by: {0}", card.Owner);
                    return true;
                }
                //check for four corners which is [0,4,20,24]
                if(card.cardsquares[0].Pick == true && card.cardsquares[4].Pick == true && card.cardsquares[20].Pick == true && card.cardsquares[24].Pick == true){
                    Console.WriteLine();
                    Console.WriteLine("Win for four corners by: {0}", card.Owner);
                    return true;
                }
            }
            return false;
        }
        //verify uniqueness of card
        public bool CardAlreadyInPlay(Card new_card_possibility)
        {
            //check against each card already in play
            foreach (Card card in this.Cards_In_Play)
            {
                List<int> _existing = (from square in card.cardsquares select square.Id).ToList();
                List<int> _new = (from square in new_card_possibility.cardsquares select square.Id).ToList();
                //this Equals comparison might never evaluate true if comparing object 'instantiation'
                if (Enumerable.SequenceEqual(_existing, _new))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
