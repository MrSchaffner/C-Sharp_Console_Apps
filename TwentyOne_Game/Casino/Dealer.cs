using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public class Dealer
    {

        public string Name { get; set; }
        public Deck Deck { get; set; }
        public int Balance { get; set; }

        public void Deal(List<Card> Hand) //add a card to a hand parameter
        {
            Hand.Add(Deck.Cards.First());
            string card = string.Format((Deck.Cards.First().ToString() + "\n"));
            Console.WriteLine(card);

            using (
                StreamWriter file = new StreamWriter(@"C:\Users\Razer\Desktop\BasicC#Programs\LOGGED_FILES\Log.txt", true) 
                //appends to current log if TRUE
                )
            {
                file.WriteLine(DateTime.Now);
                file.WriteLine(card);
            }

            Deck.Cards.RemoveAt(0);

        }
    }
}
