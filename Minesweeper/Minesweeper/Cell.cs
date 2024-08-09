using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    internal class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool Visited { get; set; }
        public bool IsLive { get; set; }
        public int LiveNeighbors { get; set; }


        public Cell()
        {
            Row = -1;
            Col = -1;
            Visited = false;
            IsLive = false;
            LiveNeighbors = 0;
        }
    }
}
