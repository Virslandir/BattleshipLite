using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLiteLibrary.Models
{
    public class GridSpotModel : IEquatable<GridSpotModel>
    {
        public string SpotLetter { get; set; }
        public string SpotNumber { get; set; }
        public GridSpotStatus Status { get; set; } = GridSpotStatus.Empty;
        public bool Equals(GridSpotModel other)
        {
            if (this.SpotLetter == other.SpotLetter && this.SpotNumber == other.SpotNumber && this.Status == other.Status)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
