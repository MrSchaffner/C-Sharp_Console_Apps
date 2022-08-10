using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public class TwentyOneGame : Game, IWalkAway
    {
        public new TwentyOneDealer Dealer { get; set; }


        public override void Play() //Plays ONE hand
        {
            TwentyOneDealer Dealer = new TwentyOneDealer();
            foreach (Player player in Players)
            { //Resetting

                player.Hand = new List<Card>();
                player.Stay = false;

            }
            //Dealing
            Dealer.Hand = new List<Card>();
            Dealer.Stay = false;
            Dealer.Deck = new Deck(); //new deck each hand
            Dealer.Deck.Shuffle();
            Console.WriteLine("Place your bet");

            foreach (Player player in Players) //Betting
            {
                int bet = Game.AskPlayerForAmount("How much to bet this round?"); //!validAnswer
                bool successfullyBet = player.Bet(bet);
                if (!successfullyBet)
                {
                    return;
                    //continue;
                } // Bet successful
                else
                {
                    Bets[player] = bet; // add bet to dictionary item
                  //  player.Balance -= bet;
                }
            } //end betting

            for (int i = 0; i < 2; i++)//DEALING. 2 is number of cards in each hand
            {
                Console.WriteLine("Dealing to Players: ");
                foreach (Player player in Players) //deal one card to each player
                {
                    Console.Write("{0}: ", player.Name);
                    Dealer.Deal(player.Hand);
                    if (i == 1) //second card dealt
                    {
                        if (TwentyOneRules.CheckForBlackJack(player.Hand))
                        {
                            Console.WriteLine("That's BlackJack! {0} wins {1}", player.Name, Bets[player]);
                            player.Balance += Convert.ToInt32((Bets[player] * 2.5)); //add back your bet and 150% winnings
                            //Bets.Remove(player); //so it doesnt pay again later in game
                            return; // for single player
                        }
                    }
                }
                Console.WriteLine("Dealing to Dealer: ");
                Dealer.Deal(Dealer.Hand); //deals card to dealer
                if (i == 1)
                {
                    if (TwentyOneRules.CheckForBlackJack(Dealer.Hand))
                    {
                        Console.WriteLine("Dealer hit the BlackJackPot! Y'all lose!");
                        //attemot to replace with lambda:
                        //Dealer.Balance += Bets.Sum()
                        foreach (KeyValuePair<Player, int> entry in Bets)
                        {
                            Dealer.Balance += entry.Value;
                        }
                        return;
                    }
                }
            } //end of Dealing

            foreach (Player player in Players)
            {
                while (!player.Stay)
                {
                    Console.WriteLine("Current Hand is :");
                    foreach (Card card in player.Hand)
                    {

                        Console.Write(card.ToString());
                        Console.Write(" | ");
                    }
                    //----------------------print hand val.ues
                    TwentyOneRules.printPlayersHandValues(player);

                    Console.WriteLine("\n\nANY KEY to HIT, ENTER or S to STAY");
                    string answer = Console.ReadLine().ToLower();
                    if (answer == "" || answer =="s" ||answer=="stay") //STAY
                    {
                        player.Stay = true;
                        break; //exit while loop
                    }
                    else //HIT
                    {
                        Dealer.Deal(player.Hand);
                        
                    }
                    if(TwentyOneRules.isBusted(player.Hand)) //check if busted
                    {
                        Dealer.Balance += Bets[player];
                        Console.WriteLine("{0} Busted. You lost your bet of {1}. You balance is now {2}.", player.Name, Bets[player], player.Balance);
                        player.isActivelyPlaying = AskKeepPlaying(player);
                        return;
                        
                    }

                }
            }
            //DEALER hits himself
            Dealer.isBusted = TwentyOneRules.isBusted(Dealer.Hand);
            Dealer.Stay = TwentyOneRules.CheckForStay(Dealer.Hand);
            while (!Dealer.Stay && !Dealer.isBusted)
            {
                Console.WriteLine("Dealer Hits-");
                Dealer.Deal(Dealer.Hand);

                Dealer.isBusted = TwentyOneRules.isBusted(Dealer.Hand);
                Dealer.Stay = TwentyOneRules.CheckForStay(Dealer.Hand);
            }
            if (Dealer.Stay)
            {
                Console.WriteLine("Dealer Stays");
            }
            // PAYING PEOPLE
            if (Dealer.isBusted)
            {
                Console.WriteLine("Dealer is Busted.");
                foreach (KeyValuePair<Player, int> entry in Bets) //dealer BUST -> pay each player
                {
                    Console.WriteLine("{0} won {1}", entry.Key.Name, entry.Value);
                    Players.Where(x => x.Name == entry.Key.Name).First().Balance += (entry.Value*2);// add money to each player
                    Dealer.Balance -= entry.Value; //take money from dealer
                }
                return;
            }
            foreach (Player player in Players)  //scoring if dealer didnt bust
            {
                bool? playerWon = TwentyOneRules.DoesPlayerWin(player.Hand, Dealer.Hand);
                if (playerWon == null) //tie, a PUSH
                {
                    player.Balance += Bets[player];
                    Console.WriteLine("PUSH. Betting balance returned.");
                    Bets.Remove(player);
                } else if (playerWon == true) {
                    player.Balance += (Bets[player] * 2);
                    Dealer.Balance -= (Bets[player]);
                    Console.WriteLine("{0} won against the dealer. New Balance: {1}", player.Name, player.Balance);
                } else if (playerWon == false)
                {
                    Console.WriteLine("Dealer wins {0}. New Balance: {1}", Bets[player], player.Balance);
                    //player.Balance += (Bets[player] * 2);
                    Dealer.Balance += (Bets[player]);
                }
                player.isActivelyPlaying = AskKeepPlaying(player);

            } // end of scoring
           



        }

        public override void ListPlayers()
        {
            Console.WriteLine("21 Game - inherited method ListPlayers():");
            base.ListPlayers();
        }

        public void WalkAway(Player player)
        {
            throw new NotImplementedException();
        }

        public bool AskKeepPlaying(Player player)
        {
            Console.WriteLine("Keep playing?");
            string answer = Console.ReadLine().ToLower();
            if (!(answer == "yes" || answer == "yeah" || answer == "ye" || answer == "y" || answer == ""))
            {
                return false;
                //=========================================================need to check for this
            }
            else return true;

        }

    }
}
