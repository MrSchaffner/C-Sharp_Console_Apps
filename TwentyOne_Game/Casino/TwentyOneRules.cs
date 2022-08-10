using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    class TwentyOneRules
    {
        private static Dictionary<Face, int> _cardValues = new Dictionary<Face, int>()
        {
            [Face.Ace] = 1,
            [Face.Two] = 2,
            [Face.Three] = 3,
            [Face.Four] = 4,
            [Face.Five] = 5,
            [Face.Six] = 6,
            [Face.Seven] = 7,
            [Face.Eight] = 8,
            [Face.Nine] = 9,
            [Face.Ten] = 10,
            [Face.Jack] = 10,
            [Face.Queen] =10,
            [Face.King] = 10
        };

        public static int[] GetPossibleHandValues(List<Card> Hand)
        {
            int value = Hand.Sum(x => _cardValues[x.Face]);
            //bad way of doing this. Simpler: if there is 1 or more aces, the second value is exactly 10 higher
            //int aceCount = Hand.Count(x => x.Face == Face.Ace);
            //int[] posValues = new int[aceCount + 1];
            //int value = Hand.Sum(x => _cardValues[x.Face]); //Hand value where aces = 1
            //posValues[0] = value;
            ////if (posValues.Length == 1) return posValues;// unneccessary, accomplished on last line.

            //for (int i =1; i<posValues.Length; i++)
            //{
            //    posValues[i] = value += (i * 10); //for each possible value, there is another ace which could be worth 10 more.
            //}
            //simpler version :
            if (Hand.Count(x => x.Face == Face.Ace)>0) //there is one or more aces
            {
                int[] posValues = new int[2] { value, value + 10 };
                return posValues;
            } else // there are no aces
            {
                int[] posValues = new int[1] { value};
                return posValues;
            }
        }

        public static bool CheckForBlackJack(List<Card> Hand)
        {
            int[] possibleValues = GetPossibleHandValues(Hand);
            int value = possibleValues.Max();
            if (value == 21) return true;
            else return false;
        }

        internal static bool isBusted(List<Card> Hand)
        {
            if (GetPossibleHandValues(Hand).Min() > 21)
            {
                return true;
            }
            return false;
        }

        public static bool CheckForStay(List<Card> Hand)
        {
            int[] posValues = GetPossibleHandValues(Hand);
            foreach (int value in posValues)
            {
                if (value > 16 && value < 22)
                {
                    return true;
                }
            }
            return false;
        }

        internal static bool? DoesPlayerWin(List<Card> playerHand, List<Card> dealerHand)
        {
            int[] playerResults = GetPossibleHandValues(playerHand);
            int playerScore = playerResults.Where(x => x < 22).Max();
            int[] dealerResults = GetPossibleHandValues(dealerHand);
            int dealerScore = dealerResults.Where(x => x < 22).Max();

            // ?bool is nullable, thus has 3 values, not 2
            if (playerScore > dealerScore) return true;
            if (playerScore == dealerScore) return null;
            else return false; // (playerScore < dealerScore)

            //throw new NotImplementedException();
        }

        public static void printPlayersHandValues(Player player)
        {
            int[] handValues = TwentyOneRules.GetPossibleHandValues(player.Hand);
            if (handValues.Length > 1) {
                Console.WriteLine("Current hand value is {0} or {1}", handValues[0], handValues[1]) ;
            }
            else
            {
                Console.WriteLine("Current hand value is {0}", handValues[0]);
            }
        }
    }
}
