using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSea_OOP_2026
{
    internal class Cell
    {
        bool hit = false;
        public int X { get; }
        public int Y { get; }

        public Cell(int x, int y)
        { 
            X = x;
            Y = y;
        }

        public bool isHit()
        { 
            return hit;
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        public void ToMatrix(int[,] m)
        {
            m[Y, X] = Convert.ToInt32(hit);
        }

        public bool Fire(int x, int y)
        { 
            if (X == x && Y == y)
                hit = true;
            return hit;
        }
    }
}
