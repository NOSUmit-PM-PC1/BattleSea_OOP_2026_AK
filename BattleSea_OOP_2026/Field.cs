using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace BattleSea_OOP_2026
{
    public class Field
    {
        static Random random = new Random();

        public const int SIZE = 10;
        const int MISS = 4;
        List<Ship> ships = new List<Ship>();

        int[,] hit_matrix = new int[SIZE, SIZE];

        Dictionary<int, int> CountShips = new Dictionary<int, int>()
        {
            { 1, 4 },
            { 2, 3 },
            { 3, 2 },
            { 4, 1 }
        };

        void CreateRandomField()
        {
            foreach (var item in CountShips)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    bool placed = false;

                    while (!placed)
                    {
                        Direction dir = (Direction)random.Next(0, 2);
                        int startX = random.Next(0, SIZE);
                        int startY = random.Next(0, SIZE);
                        if (CanPlaceShip(item.Key, startX, startY, dir))
                        {
                            Ship temp = new Ship(item.Key, startX, startY, dir);
                            ships.Add(temp);
                            temp.ToMatrix(hit_matrix);
                            placed = true;
                        }
                    }
                }
            }
        }

        void CreateTestField()
        {
            Ship temp = new Ship(1, 2, 2, Direction.Right);
            ships.Add(temp);
            temp.ToMatrix(hit_matrix);
        }

        public Field() 
        {
           CreateRandomField();
           //CreateTestField();
        }

        public int CountAllShips()
        {
            int count = 0;
            foreach (var ship in ships)
            { 
                if (ship.Status == ShipStatus.Normal || ship.Status == ShipStatus.Wounded)
                    count++;
            }
            return count;
        }

        public int CountAllWounded()
        {
            int count = 0;
            foreach (var ship in ships)
            {
                if (ship.Status == ShipStatus.Wounded)
                    count++;
            }
            return count;
        }

        public int CountAllDead()
        {
            int count = 0;
            foreach (var ship in ships)
            {
                if (ship.Status == ShipStatus.Dead)
                    count++;
            }
            return count;
        }

        public bool CanPlaceShip(int size, int startX, int startY, Direction dir)
        {
            int dx, dy;
            if (dir == Direction.Down) { dx = 0; dy = 1; }
            else { dx = 1; dy = 0; }

            for (int i = 0; i < size; i++)
            {
                int x = startX + dx * i;
                int y = startY + dy * i;

                if (x>=SIZE || y>=SIZE) return false;

                for (int nx = -1; nx <= 1; nx++)
                {
                    for (int ny = -1; ny <= 1; ny++)
                    {
                        if (x + nx >= 0 && x + nx < SIZE && y + ny >= 0 && y + ny < SIZE)
                        {
                            if (hit_matrix[y + ny, x + nx] > 0) return false;
                        }
                    }
                }
            }
            return true;
        }

        public int Matrix(int row, int col)
        { 
            return hit_matrix[row, col]; 
        }

        bool CheckFireShips(int row, int col)
        {
            foreach (var ship in ships)
            {
                if (ship.Fire(row, col))
                {
                    ship.ToMatrix(hit_matrix);
                    return true;
                }
            }
            return false;
        }

        public void Fire(int row, int col)
        {
            if (!CheckFireShips(row, col))
            {
                hit_matrix[row, col] = MISS;
            }
        }
    }
}
