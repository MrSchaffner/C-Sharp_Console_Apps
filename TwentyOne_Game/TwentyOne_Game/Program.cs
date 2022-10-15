using Casino;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace TwentyOne_Game
{
    class Program
    {
        public static string logFileURL = @"C:\Users\Razer\OneDrive\GitHub\C-Sharp_Console_Apps\TwentyOne_Game\Logged_Files\id_Log.txt";

        static void Main(string[] args)
        {
            //DoDateTime();
            //writeToTextFile();


            Console.WriteLine("Welcome to the Casino. Enter your Name:");
            string playerName = Console.ReadLine();

            if (playerName.ToLower() == "admin")
            {
                List<ExceptionEntity> Exceptions = ReadExceptions(); //should produce list of exceptions in the db
                foreach (var exception in Exceptions)
                {
                    Console.Write(exception.Id + " | ");
                    Console.Write(exception.ExceptionType + " | ");
                    Console.Write(exception.ExceptionMessage + " | ");
                    Console.Write(exception.TimeStamp + " | ");
                    Console.WriteLine();
                }
                Console.ReadLine();
            }

            //DEPOSIT FUNDS
            int balance = Game.AskPlayerForAmount("How much would you like to deposit?");

            Console.WriteLine("Hello, {0}, would you like to join game now?", playerName);
            string response = Console.ReadLine().ToLower();
            if (response == "yes" || response == "y" || response == "ye" || response == "yeah")
            {
                //PLAYER LOGGING
                Player player = new Player(playerName, balance);
                player.Id = Guid.NewGuid();
             //   using (
             //StreamWriter file = new StreamWriter(path: logFileURL, append: true)
             ////appends to current log if TRUE
             //)
                //{
                //    file.WriteLine(DateTime.Now);
                //    file.WriteLine("Player {1} with id# {0} created.", player.Id, player.Name);
                //}

                Game game = new TwentyOneGame();
                //game += player;
                game.Players.Add(player);
                //+ operator didnt work
                player.isActivelyPlaying = true;
                while (player.isActivelyPlaying && player.Balance > 0)
                {
                    try
                    {
                        game.Play();
                    }
                    catch (FraudException ex)
                    {
                        Console.WriteLine(ex.Message);
                        updateDBwithException(ex);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred. Details: {0}", ex);
                        updateDBwithException(ex);
                    }
                }
                //exited loop
                game -= player;
                Console.WriteLine("You have left the game!");
            }

            endProgram();

        }



        private static void DoDateTime()
        {

            DateTime dateTime = new DateTime(1995, 5, 23, 8, 32, 45);
            DateTime graduationDate = new DateTime(2013, 5, 23, 8, 32, 45);
            TimeSpan age = graduationDate - dateTime;

            //throw new NotImplementedException();
        }

        public static void writeToTextFile()
        {

            string textRead = File.ReadAllText(logFileURL);
            Console.WriteLine(textRead);
        }

        public static void endProgram()
        {
            Console.WriteLine("Check out our other Casino Amenities.");
            Console.WriteLine("================= PROGRAM OVER - Enter 'r' to Restart =================");
            if (Console.ReadLine() == "r")
            {
                string[] args = { };
                Main(args);
            }
        }

        //public void Tests()
        //{
        //    //CONSOLE FUNCTIONS
        //    Console.OutputEncoding = System.Text.Encoding.Unicode;


        //    //Game initializer
        //    Game game = new TwentyOneGame() { Dealer.Name = "HumanTron 3.0" };
        //    game.Players = new List<Player>();

        //    Player player4 = new Player() { Name = "Zed" };
        //    game += player4; //the overloaded operator+ is contained in Player class
        //    //game -= player4;

        //    //DECK
        //    Deck deck = new Deck();
        //    deck.Shuffle(20);

        //    // Lambda function
        //    Console.WriteLine("Lambda Function tests:");
        //    int aceCount = deck.Cards.Count(x => x.Face == Face.Ace);
        //    Console.WriteLine("aces counted are {0}", aceCount);

        //    List<Card> newList = deck.Cards.Where(x => x.Face == Face.King).ToList();
        //    foreach (Card card in newList)
        //    {
        //        Console.WriteLine("{0} of {1}", card.FaceSymbols[(int)card.Face], card.SuitsSymbols[(int)card.Suit]);
        //    }

        //    List<int> numList = new List<int>() { 2, 4, 545, 968 };
        //    Console.WriteLine(numList.Sum());
        //    Console.WriteLine(numList.Sum(x => x + 2));
        //    Console.WriteLine(numList.Max(x => x + 2));


        //    //PRINT TESTS
        //    game.ListPlayers();

        //    foreach (Card card in deck.Cards)
        //    {
        //        Console.Write(card.ToString());
        //        Console.Write(" | ");
        //    }
        //    Console.WriteLine("\n\nANY KEY to HIT, ENTER to STAY-");
        //    string answer = Console.ReadLine().ToLower();
        //    if (answer == "") //STAY
        //    {
        //       // stay
        //    }


        //    Console.ReadLine();
        //}

        public enum DaysOfWeek
        {
            Mon, Tue, Wed, Thur, Fri, Sat, Sun
        }

        //connection string from sql server object explorer TwentyOneGame- rightClick properties
        const string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=TwentyOneGame;
Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static void updateDBwithException(Exception ex)
        {
            // an SQl query RENAMED
            string queryString = @"INSERT INTO Exceptions2 (ExceptionType, ExceptionMessage, TimeStamp) VALUES (@ExceptionType, @ExceptionMessage, @TimeStamp)";

            // creates 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //command consists of a query applied to a location / connection
                SqlCommand command = new SqlCommand(queryString, connection);
                //parameters correspond to those entries entered in Values of quesryString.
                command.Parameters.Add("@ExceptionType", SqlDbType.VarChar);
                command.Parameters.Add("@ExceptionMessage", SqlDbType.VarChar);
                command.Parameters.Add("@TimeStamp", SqlDbType.DateTime);
                //Set Values for those parameters
                command.Parameters["@ExceptionType"].Value = ex.GetType().ToString();
                command.Parameters["@ExceptionMessage"].Value = ex.Message;
                command.Parameters["@TimeStamp"].Value = DateTime.Now;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();



            };
        }

        private static List<ExceptionEntity> ReadExceptions()
        {
            //use connectionString 

            string queryString2 = @"SELECT * FROM Exceptions2"; //changed name

            List<ExceptionEntity> ExceptionsList = new List<ExceptionEntity>();

            // creates 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString2, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ExceptionEntity exception = new ExceptionEntity();
                    exception.Id = Convert.ToInt32(reader["Id"]);
                    exception.ExceptionType = reader["ExceptionType"].ToString();
                    exception.ExceptionMessage = reader["ExceptionMessage"].ToString();
                    exception.TimeStamp = Convert.ToDateTime(reader["TimeStamp"]);
                    ExceptionsList.Add(exception);
                }
                connection.Close();
            }
            return ExceptionsList;
        }//end readExceptions()

    } //class
} //namespace
