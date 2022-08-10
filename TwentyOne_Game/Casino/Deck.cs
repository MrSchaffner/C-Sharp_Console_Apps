using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public class Deck
    {
        public List<Card> Cards = new List<Card>();

        public Deck() //CONSTRUCTOR
        {
            Cards = new List<Card>();

            for (int i = 0; i < 13; i++)
            {

                for (int j = 0; j < 4; j++)
                {
                    Card card = new Card();
                    card.Face = (Face)i;
                    card.Suit = (Suit)j;
                    Cards.Add(card);
                }
            }


            //List<string> Suits = new List<string>() { "\x2665", "\x2666", "\x2660", "\x2663" }; //"hearts: \x2665", "diamonds: \x2666", " spades: \x2660", "clubs: \x2663"
            //List<string> Faces = new List<string>() { " A"," 2"," 3"," 4"," 5"," 6"," 7"," 8"," 9","10"," J"," Q"," K"};

            //foreach (string suit in Suits)
            //{
            //    foreach (string face in Faces)
            //    {
            //        Card card = new Card(suit, face);
            //        Cards.Add(card);
            //    }
            //}

        }


        public void Shuffle(byte times = 1) // out int timesShuffledOUT,
        {
            for (int i = 0; i < times; i++) // how many times to shuffle
            {
                Random rnd = new Random();
                List<Card> TempList = new List<Card>();

                while (Cards.Count > 0) //shuffling code and assigning to TempList
                {
                    int randomIndex = rnd.Next(0, Cards.Count); //starts at 52
                    TempList.Add(Cards[randomIndex]);
                    Cards.RemoveAt(randomIndex);

                }
                Cards = TempList;
            }
        }

    }
}
