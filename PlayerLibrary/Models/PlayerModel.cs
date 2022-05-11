using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLibrary.Models
{
    public class PlayerModel
    {
        public string PlayerName { get; set; }
        public List<string> ShipLocations { get; set; }
        //public List<GridSpotModel> ShipLocations { get; set; }
        //public List<GridSpotModel> ShotGrid { get; set; }
        public int ShipCount = 5;
        public List<string> AttemptedSpots { get; set; }

        public PlayerModel ()
        {
            AttemptedSpots = new List<string>();
            ShipLocations = new List<string>();
        }
    }
}
