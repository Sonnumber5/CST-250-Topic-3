using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    internal class GameBoard
    {
        public int Size { get; set; }
        public Cell[,] Grid { get; set; }
        public double Difficulty { get; set; }
        public int TotalCells { get; set; }
        public double TotalBombs { get; set; }



        public GameBoard(int size)
        {
            this.Size = size;
            this.Grid = new Cell[size, size];


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Grid[i, j] = new Cell() { Row = i, Col = j };
                }
            }

        }
       
        /// <summary>
        /// This method generates random coordinates on the grid to place bombs
        /// </summary>
        public void SetupLiveNeighbors()
        {
            Random rand = new Random();
            this.TotalCells = Size * Size;
            this.TotalBombs = (int)TotalCells / Difficulty;

            // goes through this loop (liveCells) number of times
            for (int i = 0; i < TotalBombs; i++)
            {
                //generates a random coordinate for the row and column
                int row = rand.Next(Size);
                int col = rand.Next(Size);

                //checks if the coordinate is already live, if so, generates new coordinates 
                while (Grid[row, col].IsLive)
                {
                    row = rand.Next(Size);
                    col = rand.Next(Size);
                }
                // sets the Live status of the Cell at the given coordinates to true
                Grid[row, col].IsLive = true;
            }
        }
        

     
        /// <summary>
        /// This method calculates how many of it's neighbors are bombs. neighbors include the 8 cells directly around each cell
        /// </summary>
        public void CalculateLiveNeighbors()
        {
            // 2 for loops iterate through each node in the gameboard
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    //instantiate new Cell objects for each cell in the gameboard
                    Cell cell = Grid[row, col];

                    //checks if the cell is live, if so, set the LiveNeighbors to 9 and move to next cell
                    if (cell.IsLive)
                    {
                        cell.LiveNeighbors = 9;
                        continue;
                    }
                    // local variable to hold liveNeighbors
                    int liveNeighbors = 0;

                    // 2 for loops to represent a 3 x 3 grid around the cell to represent its direct neighbors
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            // skips to the next cell when the iteration reaches the cell we are currently on
                            if (x == 0 && y == 0)
                            {
                                continue;
                            }
                            // delcare local properties to represent to each neighbor node
                            int neighborRow = row + x;
                            int neighborCol = col + y;

                            // checks if the current neighbor nodes are in the gameboard
                            if (neighborRow >= 0 && neighborRow < Size && neighborCol >= 0 && neighborCol < Size)
                            {
                                // if any of the neighbor cells are bombs, liveNeighbors is incremented
                                if (Grid[neighborRow, neighborCol].IsLive)
                                {
                                    liveNeighbors++;
                                }
                            }
                        }
                    }
                    //assigns the value of the local liveNeighbors variable to the Current cell in question's LiveNeighbors property
                    Grid[row, col].LiveNeighbors = liveNeighbors;
                }
            }
        }

        public void FloodFill(int row, int col)
        {
            //returns if the cell is out of bounds
            if (row < 0 || row >= Size || col < 0 || col >= Size)
            {
                return;
            }

            Cell cell = Grid[row, col];

            // return if the cell has already been visited or is a live
            if (cell.Visited || cell.IsLive)
            {
                return;
            }

            //set the cell's visited property to true so we can reveal the cell on the board
            cell.Visited = true;

            //checks if the cell has live neighbors and returns if it does 
            if (cell.LiveNeighbors > 0)
            {
                return;
            }

            //recursively calls every neighbor of every cell until the base case exits the function
            FloodFill(row - 1, col);
            FloodFill(row + 1, col);
            FloodFill(row, col - 1);
            FloodFill(row, col + 1);
            FloodFill(row - 1, col - 1);
            FloodFill(row - 1, col + 1);
            FloodFill(row + 1, col - 1);
            FloodFill(row + 1, col + 1);
        }

    }
}
