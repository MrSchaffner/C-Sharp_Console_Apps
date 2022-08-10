using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public abstract class Game //class to inherit from
    {
        private List<Player> _players = new List<Player>(); //instantiated private empty list. public list getters enable access
        public List<Player> Players { get { return _players; } set { _players = value; } } 

        public string Name { get; set; } 
        public string Dealer { get; set; }

        public Dictionary<Player, int> _bets = new Dictionary<Player, int>();
        public Dictionary<Player, int> Bets { get {return _bets; } set {_bets = value; }  }//= new Dictionary<Player, int>();

        public virtual void ListPlayers()
        {
            if (Players.Count < 1)
            {
                Console.WriteLine("No Players in this game");
                return;
            }

            Console.WriteLine("Current Players are:");
            foreach (Player player in Players)
            {
                Console.WriteLine(player.Name);
            }
        }

        public abstract void Play(); //forces any class inheriting must implement this method.

        public static int AskPlayerForAmount(string requestMessage) //returns an int after requesting console input
        {
            bool validAnswer = false;
            int balance = 0;
            while (!validAnswer||balance<0)
            {
                //int newBalance;
                Console.WriteLine(requestMessage);
                validAnswer = int.TryParse(Console.ReadLine(), out balance);
                
                if (!validAnswer || balance < 0) { Console.WriteLine("Enter digits only.");
                    if (balance < 0) { throw new FraudException("Security! Kick this Person Out"); }
                    validAnswer = false; //this only is here if validAnswer was true, but Balance was a negative int.
                }
          
            }
            
           // Console.WriteLine("entered {0}",balance);
            return balance;
        }

        public static Game operator +(Game game, Player player)
        {
            game.Players.Add(player);
            return game;
        }

        public static Game operator -(Game game, Player player)
        {

            game.Players.Remove(player);
            return game;
        }

    }
}
