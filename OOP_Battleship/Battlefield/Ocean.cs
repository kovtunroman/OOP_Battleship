using System.Collections.Generic;
using System.Linq;

namespace OOP_Battleship.Battlefield
{
    public class Ocean
    {
        public List<Cell> Cells { get; set; }

        public Ocean()
        {
            Cells = new List<Cell>();

            for(int row = 1; row <= 10; row++)
            {
                for(int colomn = 1; colomn <= 10; colomn++)
                {
                    Cells.Add(new Cell(row, colomn));
                }
            }
        }

        public List<Cell> CellsInRange(int startRow, int startColumn, int endRow, int endColumn)
        {
            return Cells.Where(cell => cell.Coordinates.Row >= startRow
                                     && cell.Coordinates.Column >= startColumn
                                     && cell.Coordinates.Row <= endRow
                                     && cell.Coordinates.Column <= endColumn).ToList();
        }

        public Cell CellAtCoordinate(int row, int column)
        {
            return Cells.Where(cell => cell.Coordinates.Row == row && cell.Coordinates.Column == column).First();
        }
    }
}
