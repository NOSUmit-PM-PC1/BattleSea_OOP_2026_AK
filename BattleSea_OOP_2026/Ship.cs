using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSea_OOP_2026
{
    public enum Direction
    { 
        Down,
        Right
    }

    public enum ShipStatus 
    { 
        Normal,
        Wounded,
        Dead
    }

    internal class Ship
    {
        
        List<Cell> cells;
        ShipStatus status;

        public Ship(int size, int startX, int startY, Direction dir)
        {
            int dx, dy;
            if (dir == Direction.Down) { dx = 0; dy = 1; }
            else { dx = 1; dy = 0; }

            cells = new List<Cell>();
            for (int i = 0; i < size; i++)
            {
                cells.Add(new Cell(startX + dx * i, startY + dy * i));
            }
            status = ShipStatus.Normal;
        }

        public void ToMatrix(int[,] m)
        {
            if (status == ShipStatus.Dead)
                foreach (var cell in cells)
                {
                    m[cell.Y, cell.X] = (int)status + 1;
                }
            else
                foreach (var cell in cells)
                {
                    m[cell.Y, cell.X] = Convert.ToInt16(cell.isHit()) + 1;
                }
        }

        public int CountHit()
        { 
            int count = 0;
            foreach (Cell cell in cells)
                count += Convert.ToInt32(cell.isHit());
            return count;
        }

        public override string ToString()
        {
            return $"{cells.Count} - {cells[0].X}, {cells[0].Y}: {CountHit()}";
        }

        public bool Fire(int row, int col)
        {
            foreach (Cell cell in cells)
            {
                if (cell.Fire(col, row))
                {
                    int countHit = CountHit();
                    if (countHit < cells.Count)
                        status = ShipStatus.Wounded;
                    else 
                        if (countHit == cells.Count)
                            status = ShipStatus.Dead;
                    return true;
                }
            }
            return false;
        }
    }
}
