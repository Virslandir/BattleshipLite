using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipLiteLibrary.Models;
using BattleshipLiteLibrary;

namespace BattleshipLiteUI
{
    class Program
    {
        static void Main()
        {
            WelcomeMessage();
            
            PlayerInfoModel activePlayer = CreatePlayer("Player 1");
            PlayerInfoModel opponent = CreatePlayer("Player 2");
            PlayerInfoModel winner = null;

            do
            {
                DisplayShotGrid(activePlayer);

                RecordPlayerShot(activePlayer, opponent);

                bool doesGameContinue = GameLogic.PlayerStillActive(opponent);

                if (doesGameContinue == true)
                {
                    (activePlayer, opponent) = (opponent, activePlayer);
                }
                else
                {
                    winner = activePlayer;
                }

            } while (winner == null);

            IdentifyWinner(winner);

            Console.ReadLine();


        }

        private static void IdentifyWinner(PlayerInfoModel winner)
        {
            Console.WriteLine($"The winner is {winner.UsersName}! Congratulations!");
            Console.WriteLine($"{winner.UsersName} took { GameLogic.GetShotCount(winner) } shots.");
        }

        private static void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
        {
            string row = "";
            string column = "";
            bool isValidShot;

            do
            {
                string shot = AskForLocation(activePlayer, "please select your target location:");
                ( row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);

                isValidShot = GameLogic.ValidateShot( activePlayer,row, column);

                if (isValidShot == false)
                {
                    Console.WriteLine("Invalid location. Please try again.");
                }

            } while (isValidShot == false);

            bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);

            if (isAHit)
            {
                Console.WriteLine("Congratulations, you have hit your opponents ship!");
                Console.WriteLine("Press Enter to end your turn.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("That was a miss, better luck next time!");
                Console.WriteLine("Press Enter to switch to end your turn.");
                Console.ReadLine();

            }

            GameLogic.MarkShotResult(activePlayer, opponent, row, column, isAHit);

            Console.Clear();
            
        }

        private static string AskForLocation(PlayerInfoModel player, string message)
        {
            string output = "";
            do
            {
                Console.WriteLine($"{player.UsersName}, {message}");
                output = Console.ReadLine();

                if (output.Length!=2)
                {
                    Console.WriteLine("A valid location has only 2 characters. Please try again.");
                }
            } while (output.Length!=2);

            return output;
        }

        private static void DisplayShotGrid(PlayerInfoModel activePlayer)
        {
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;

            foreach (var gridSpot in activePlayer.ShotGrid)
            {
                if (currentRow != gridSpot.SpotLetter)
                {
                    Console.WriteLine();
                    currentRow = gridSpot.SpotLetter;
                }
                
                if (gridSpot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" { gridSpot.SpotLetter}{ gridSpot.SpotNumber} "); 
                }
                else if (gridSpot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X ");
                }
                else if (gridSpot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O ");
                }
                else
                {
                    Console.Write(" ? ");
                }
            }

            Console.WriteLine();
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship Lite!");
            Console.WriteLine("Created by Stefan Spiric!");
            Console.WriteLine("");

        }

        private static void PlaceShips(PlayerInfoModel model)
        {
            do
            {
                string location = AskForLocation(model, $"where do you want to place your ship number {model.ShipLocations.Count + 1}: ");

                bool isValidLocation = GameLogic.PlaceShip(model, location);

                if (isValidLocation == false)
                {
                    Console.WriteLine($"{location.ToUpper()} is not a valid location. Please, try again.");
                }
                
            } while (model.ShipLocations.Count < 5); ;
            
        }

        private static PlayerInfoModel CreatePlayer(string playerTitle)
        {
            PlayerInfoModel output = new PlayerInfoModel
            {
                UsersName = AskForUsersName(playerTitle)
            };

            GameLogic.InitializeGrid(output);

            PlaceShips(output);

            Console.Clear();

            return output;
        }

        private static string AskForUsersName(string playerTitle)
        {
            string output = "";
            do
            {
                Console.WriteLine($"{playerTitle} please input your name:");
                output = Console.ReadLine();

                if (String.IsNullOrEmpty(output))
                {
                    Console.WriteLine("Invalid name.");
                }
            } while (String.IsNullOrEmpty(output));

            return output;

        }
    }
}
