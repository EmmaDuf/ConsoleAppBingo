using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBingo
{
    internal class Program
    {
        //Prints a visualization of the card to the terminal
        //Cyan colored numbers are picked numbers
        static void PrintCard(Card card)
        { 
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("{0}'s board:",card.Owner);
            Console.WriteLine("B\tI\tN\tG\tO\t");
            for (int i = 0; i < 5; i ++)
            {
                for (int j = 0; j < 25; j+=5)
                {
                    if(card.cardsquares[j+i].Pick == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else { Console.ForegroundColor = ConsoleColor.White; }
                    Console.Write("{0}\t", card.cardsquares[j + i].Id);
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to Bingo! Here are the options for playing");
            bool interested_user = true;
            while (interested_user)
            {
                Console.WriteLine("1) Simulate a game");
                Console.WriteLine("2) Create your own game");
                Console.WriteLine("Pick an option or press q to stop playing");
                bool still_playing = Int32.TryParse(Console.ReadLine(),out int result);
                if (still_playing)
                {
                    if(result == 1)
                    {
                        GameSpace sim_game = new GameSpace();
                        Console.WriteLine("Welcome to the simulated game!");
                        int players = 3;
                        Console.WriteLine("{0} players will compete",players);
                        for(int i = 0; i < players; i++)
                        {
                            int player_num = i+1;
                            string nameof = "Player " + player_num;
                            sim_game.CreateCard(nameof);
                        }
                        Console.WriteLine("Here are their cards!");
                        foreach(Card card in sim_game.Cards_In_Play)
                        {
                            PrintCard(card);
                        }
                        Console.WriteLine("Simulating all Bingo Rolls to declare a winner....");
                        Console.WriteLine("Numbers Picked: ");
                        while (!sim_game.PlayRound())
                        {
                            //PlayRound() rolls a number, updates player's cards for that number
                            //And checks if there is a winner 
                            //PlayRound returns true if there is a winner.
                            //Keep playing if PlayRound returns false
                        }
                        foreach(Card card in sim_game.Cards_In_Play)
                        {
                            PrintCard(card);
                        }
                        Console.WriteLine("Game Completed!");
                        Console.WriteLine("Would you like to continue with Bingo? Input yes or q to quit.");
                        string user_input = Console.ReadLine();
                        if (user_input.Trim().ToLower().Contains("q"))
                        {
                            interested_user = false;
                        }
                    }
                    if(result == 2)
                    {
                        GameSpace mygame = new GameSpace();
                        Console.WriteLine("Let's create your own game!");
                        Console.WriteLine("How many players do you want to play?");
                        int players = Int32.Parse(Console.ReadLine());
                        for(int i = 0; i < players; i++)
                        {
                            int player_num = i+1;
                            Console.WriteLine("Please give a name for player #{0}",player_num);
                            string nameof = Console.ReadLine();
                            mygame.CreateCard(nameof);
                        }
                        Console.WriteLine("Here are their cards!");
                        foreach(Card card in mygame.Cards_In_Play)
                        {
                            PrintCard(card);
                        }
                        //winner is output of PlayRound, which returns true of there is a winner.
                        //Stop playing when there's a winner
                        bool winner = false;
                        while (!winner)
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Input roll to simulate a roll or skip to see who the winner is");
                            string user_request = Console.ReadLine();
                            if (user_request.Trim().ToLower().Contains("r"))
                            {
                                winner = mygame.PlayRound();
                                foreach(Card card in mygame.Cards_In_Play)
                                {
                                    PrintCard(card);
                                }
                            }
                            if (user_request.Trim().ToLower().Contains("s"))
                            {
                                while (!winner)
                                {
                                    winner = mygame.PlayRound();
                                }
                                foreach(Card card in mygame.Cards_In_Play)
                                {
                                    PrintCard(card);
                                }
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Game Completed!");
                        Console.WriteLine("Would you like to continue with Bingo? Input yes or q to quit.");
                        string user_input = Console.ReadLine();
                        if (user_input.Trim().ToLower().Contains("q"))
                        {
                            interested_user = false;
                        }
                        
                    }
                }
                else { interested_user=false;}    
            }
            Console.WriteLine("Thanks for playing!");
            Console.ReadLine();
        }
    }
}
