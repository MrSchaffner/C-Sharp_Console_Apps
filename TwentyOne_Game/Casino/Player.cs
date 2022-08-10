using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public class Player
    {
        //private string playerName; // NOTE duplivcate
        public Guid Id { get; set; }
        public bool Stay { get; set; }
        public List<Card> Hand { get { return _hand; } set { _hand = value; } }
        private List<Card> _hand = new List<Card>();
        public int Balance { get; set; }
        public string Name { get; set; }
        public bool isActivelyPlaying { get; set; }

        public Player(string playerName) : this(playerName, 100)
        {
        }

        public Player(string playerName, int Balance)
        {
            this.Name = playerName;
            this.Balance = Balance;
            this.Hand = new List<Card>();
        }



        public bool Bet(int Amount)
        {
            if (Balance - Amount < 0)
            {
                Console.WriteLine("Balance insufficient. Current Balance: {0}",this.Balance);
                return false;
            }
            else
            {
                Balance -= Amount;
                return true;
            }
        }

        //public static Game operator+ (Game game, Player player) MOVED TO GAME CLASS
  


    }
}
