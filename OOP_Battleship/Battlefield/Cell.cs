using System.Linq;
using System.Collections.Generic;

namespace OOP_Battleship.Battlefield
{
    public class Cell
    {
        public OccupationType OccupationType { get; set; }
        public Coordinates Coordinates { get; set; }

        public Cell(int row, int column)
        {
            OccupationType = OccupationType.Empty;
            Coordinates = new Coordinates(row, column);
        }

        public string Status
        {
            get
            {
                return ((char)OccupationType).ToString();
            }
        }

        public bool IsOccupied
        {
            get
            {
                return OccupationType == OccupationType.Battlship
                    || OccupationType == OccupationType.Crusier
                    || OccupationType == OccupationType.Destroyer
                    || OccupationType == OccupationType.Submarine
                    || OccupationType == OccupationType.Carrier;
            }
        }
    }
}
