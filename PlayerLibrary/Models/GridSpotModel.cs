using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLibrary.Models
{
    public class GridSpotModel
    {
        public string SpotLetter { get; set; }
        public string SpotNumber { get; set; }
        public GridSpotStatus Status { get; set; } = GridSpotStatus.Empty;
    }
}
