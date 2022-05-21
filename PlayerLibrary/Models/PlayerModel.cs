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
        public List<GridSpotModel> ShipLocations { get; set; }
        public List<GridSpotModel> ShotGrid { get; set; }

        public PlayerModel ()
        {
            ShipLocations = new List<GridSpotModel>();
            ShotGrid = new List<GridSpotModel>();
        }
    }
}
