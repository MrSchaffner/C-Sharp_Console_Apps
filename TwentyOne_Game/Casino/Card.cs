using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public class Card //instructor made it struct, I dont want to.
    {
        //public Card(string suit = "Spades", string face = "Ace")
        //{
        //    Suit = suit;
        //    Face = face;
        //}

        public Suit Suit { get; set; }
        public Face Face { get; set; }
        public List<string> SuitsSymbols = new List<string>() { "\x2665", "\x2666", "\x2660", "\x2663" }; //see enum suit
        public List<string> FaceSymbols = new List<string>() { "II", "III", "IV", "V", "VI", "VII", "VII", "IX", "X", "J", "Q", "K", "A" }; //SEE ENUM FACE
        //public List<string> FaceSymbols = new List<string>() { "I I", "III", "I V", " V ", "V I", "VII", "IIX", "I X", " X ", " J ", " Q ", " K ", " A " }; //SEE ENUM FACE

        public override string ToString()
        {
            return string.Format("{0} of {1}", this.FaceSymbols[(int)this.Face], this.SuitsSymbols[(int)this.Suit]);

        }

    }

    public enum Suit
    { //dont change order or you have to change SuitsSymbols
        Hearts, Diamonds, Spades, Clubs

    }

    public enum Face
    {
        Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
    }
}
