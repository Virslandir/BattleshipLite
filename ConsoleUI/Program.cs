using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerLibrary.Models;

namespace ConsoleUI
{
    class Program
    {
        public static PlayerModel player1 = new PlayerModel();
        public static PlayerModel player2 = new PlayerModel();
        public static List<PlayerModel> players = new List<PlayerModel> { player1, player2 };

        public static List<string> gridArea = new List<string> { "A1", "A2", "A3", "A4", "A5", "B1", "B2", "B3", "B4", "B5", "C1", "C2", "C3", "C4", "C5", "D1", "D2", "D3", "D4", "D5", "E1", "E2", "E3", "E4", "E5" };

        static void Main()
        {
            Console.WriteLine("Welcome to BATTLESHIP Lite!");

            GetPlayerNames();
            Console.Clear();

            Console.WriteLine("Ship placement phase starts!");
            Console.WriteLine("Player 1 place your ships:");
            PlaceShips(player1);
            Console.Clear();
            Console.WriteLine("Player 2 place your ships:");
            PlaceShips(player2);
            Console.Clear();

            Console.WriteLine("Battle phase starts!");
            //ShowGridWithAttempts(player1);

            Fire(player1);
            /*            
            DetermineHitOrMiss();
            CheckIfGameOver();
            */





            //CheckData();
        }

        public static void GetPlayerNames()
        {
            Console.WriteLine("Player 1 type in your name:");
            player1.PlayerName = Console.ReadLine();
            Console.WriteLine("Player 2 type in your name:");
            player2.PlayerName = Console.ReadLine();
        }

        public static void PlaceShips(PlayerModel player)
        {
            for (int i = 1; i < 6; i++)
            {
                string userInput;
                bool validInput;

                do
                {
                    Console.Write($"SHIP {i}: ");

                    userInput = Console.ReadLine();
                    validInput = ValidateShipInput(userInput, player);

                    if (validInput)
                    {
                        StoreListData(player.ShipLocations, userInput);
                    }
                         
                } while (validInput == false);
            }
        }

        public static void CheckData()
        {


            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine($"Player {i+1} name: {players[i].PlayerName}");
                foreach (string ship in players[i].ShipLocations)
                {
                    Console.WriteLine(ship);
                }
            }
        }

        public static bool ValidateShipInput(string userInput, PlayerModel player)
        {
            if (ValidGridLocation(userInput))
            {
                if (NoDuplicatesInList(userInput, player.ShipLocations))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"You have already placed a ship at location {userInput.ToUpper()}.");
                    Console.WriteLine("Try again.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. The grid area is from A1 to E5.");
                Console.WriteLine("Try again.");
                return false;
            }
        }

        public static bool ValidGridLocation(string userInput)
        {
            return gridArea.Contains(userInput.ToUpper());
        }

        public static bool NoDuplicatesInList(string userInput, List<string> list)
        {
            return !list.Contains(userInput.ToUpper());
        }

        public static void StoreListData(List<string> list, string userInput)
        {
            list.Add(userInput.ToUpper());
        }

        public static void Fire(PlayerModel player)
        {
            string userInput;
            bool validInput;

            do
            {
                Console.Write($"{player.PlayerName} choose a field to fire on (A1 - E5): ");

                userInput = Console.ReadLine();
                validInput = ValidateAttemptInput(userInput, player);

            } while (validInput == false);

            if (IsOnTarget(userInput, player2.ShipLocations))
            {
                StoreListData(player.AttemptedSpots, userInput);
            }

        }

        public static bool ValidateAttemptInput(string userInput, PlayerModel player)
        {
            if (ValidGridLocation(userInput))
            {
                if (NoDuplicatesInList(userInput, player.AttemptedSpots))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"You have already fired at location {userInput.ToUpper()}.");
                    Console.WriteLine("Try again.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. The grid area is from A1 to E5.");
                Console.WriteLine("Try again.");
                return false;
            }
        }

        public static bool IsOnTarget(string userInput, List<string> list)
        {
            return list.Contains(userInput.ToUpper());
                       
        }



        /*
        CheckHitOrMiss()
        CheckGameOver()
        */
        }
    }
