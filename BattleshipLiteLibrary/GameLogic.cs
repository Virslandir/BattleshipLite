using BattleshipLiteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLiteLibrary
{
    public static class GameLogic
    {
        public static void InitializeGrid(PlayerInfoModel model)
        {
            List<string> letters = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };

            List<string> numbers = new List<string>
            {
                "1",
                "2",
                "3",
                "4",
                "5"
            };

            foreach (string letter in letters)
            {
                foreach (string number in numbers)
                {
                    AddGridSpot(model, letter, number);
                }
            }

        }

        public static bool PlayerStillActive(PlayerInfoModel player)
        {
            bool isActive = false;

            foreach (var ship in player.ShipLocations)
            {
                if (ship.Status != GridSpotStatus.Sunk)
                {
                    isActive = true;
                }; 
            }

            return isActive;
        }

        private static void AddGridSpot(PlayerInfoModel model, string letter, string number)
        {
            GridSpotModel gridSpot = new GridSpotModel()
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };
            model.ShotGrid.Add(gridSpot);
        }

        public static bool PlaceShip(PlayerInfoModel model, string location)
        {
            bool output = false;

            (string row, string column) = SplitShotIntoRowAndColumn(location);

            bool isValidLocation = ValidateGridLocation(model, row, column);
            bool isSpotOpen = ValidateShipLocation(model, row, column);

            if (isValidLocation && isSpotOpen )
            {
                model.ShipLocations.Add(new GridSpotModel() 
                {
                    SpotLetter = row.ToUpper(),
                    SpotNumber = column,
                    Status = GridSpotStatus.Ship
                });
                output = true;
            }

            return output;
        }

        private static bool ValidateShipLocation(PlayerInfoModel model, string row, string column)
        {
            bool isValidLocation = true;

            foreach (var location in model.ShipLocations)
            {
                if (location.SpotLetter.ToUpper() == row && location.SpotNumber == column)
                {
                    isValidLocation = false;
                }
            }

            return isValidLocation;
        }

        private static bool ValidateGridLocation(PlayerInfoModel model, string row, string column)
        {
            bool isValidLocation = false;

            foreach (var location in model.ShotGrid)
            {
                if (location.SpotLetter.ToUpper() == row && location.SpotNumber == column)
                {
                    isValidLocation = true;
                }
            }

            return isValidLocation;
        }

        public static int GetShotCount(PlayerInfoModel player)
        {
            int shotCount = 0;

            foreach (GridSpotModel shot in player.ShotGrid)
            {
                if (shot.Status == GridSpotStatus.Hit || shot.Status == GridSpotStatus.Miss)
                {
                    shotCount++;
                }
            }

            return shotCount;
        }

        public static (string row, string column) SplitShotIntoRowAndColumn(string shot)
        {
            string row = shot.Substring(0, 1).ToUpper();
            string column = shot.Substring(1, 1).ToUpper();

            return (row, column);
        }

        public static bool ValidateShot(PlayerInfoModel activePlayer, string row, string column)
        {
            bool output = false;

            GridSpotModel shot = new GridSpotModel()    
            {
                SpotLetter = row.ToUpper(),
                SpotNumber = column,
                Status = GridSpotStatus.Empty
            };

            if (activePlayer.ShotGrid.Contains(shot))
            {
                output = true;
            }

            return output;

        }

        // IdentifyShotResult sam radio drugacije nego Tim!! Dobro proveriti da li zaista radi sa ovime
        public static bool IdentifyShotResult(PlayerInfoModel opponent, string row, string column)
        {
            bool output = false;

            GridSpotModel shot = new GridSpotModel()
            {
                SpotLetter = row,
                SpotNumber = column,
                Status = GridSpotStatus.Ship
            };

            if (opponent.ShipLocations.Contains(shot))
            {
                output = true;
            }

            return output;
        }

        public static void MarkShotResult(PlayerInfoModel player, PlayerInfoModel opponent, string row, string column, bool isAHit)
        {
            foreach (var location in player.ShotGrid)
            {
                if (location.SpotLetter == row && location.SpotNumber == column)
                {
                    if (isAHit)
                    {
                        location.Status = GridSpotStatus.Hit;
                    }
                    else
                    {
                        location.Status = GridSpotStatus.Miss;
                    }
                }
            }
            foreach (var ship in opponent.ShipLocations)
            {
                if (ship.SpotLetter == row && ship.SpotNumber == column)
                {
                    if (isAHit)
                    {
                        ship.Status = GridSpotStatus.Sunk;
                    }
                }
            }
        }
    }
}
