using System.Data;

namespace Example_4
{
    class Program
    {
        static int BoardSize = 8;
        static int attemptedMoves = 0;
        // these represent the coordinates for the movers the knight can make in relation to where it's currently standing
        static int[] xMove = { 2, 1, -1, -2, -2, -1, 1, 2 };
        static int[] yMove = { 1, 2, 2, 1, -1, -2, -2, -1 };

        // boardGrid is an 8x8 array that contains -1 for an unvisisted square or a move number between 0 and 63.
        static int[,] boardGrid = new int[BoardSize, BoardSize];
        static void Main(string[] args)
        {
            solveKT();
            Console.ReadLine();
        }

        /*
         * This function solves the knight tour problem using backtracking. This function uses solveKTUtil() to solve the 
         * problem. It returns false if no complete tour is possible, otherwise return true and prints the tour. There may 
         * be more than one solution
         */
        static void solveKT()
        {
            // initialization of solution matrix. value of -1 means "not visited yet"
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    boardGrid[x, y] = -1;
                }
            }
            int startX = 0;
            int startY = 4;

            //set starting position for knight
            boardGrid[startX, startY] = 0;

            //count the total number of guesses
            attemptedMoves++;

            //explore all tours using solveKTUtil()
            if(!solveKTUtil(startX, startY, 1))
            {
                Console.WriteLine($"Solution does not exist for {startX} and {startY}");
            }
            else
            {
                PrintSolution(boardGrid);
                Console.Out.WriteLine($"Total attempted moves: {attemptedMoves}");
            }
        }

        // a recursive utility function to solve knight tour problem
        static bool solveKTUtil(int x, int y, int moveCount)
        {
            attemptedMoves++;
            if (attemptedMoves % 1000000 == 0) Console.Out.WriteLine($"Attempts: {attemptedMoves}");

            int next_x;
            int next_y;

            // Check to see if we have reached a solution. 64 = moveCount
            if (moveCount == BoardSize * BoardSize)
            {
                return true;
            }

            // Store all possible moves and the count of unvisited neighbors for each move
            int[][] moveOptions = new int[8][];
            for (int k = 0; k < 8; k++)
            {
                next_x = x + xMove[k];
                next_y = y + yMove[k];

                //pass both to the CountUnvisitedNeighbors method 
                int unvisitedCount = CountUnvisitedNeighbors(next_x, next_y);

                // Store the move index and the number of unvisited neighbors
                moveOptions[k] = new int[] { k, unvisitedCount };
            }

            // Sort moveOptions based on the number of unvisited neighbors
            for (int i = 0; i < moveOptions.Length - 1; i++)
            {
                for (int j = i + 1; j < moveOptions.Length; j++)
                {
                    if (moveOptions[i][1] > moveOptions[j][1])
                    {
                        // Swap the moves
                        var temp = moveOptions[i];
                        moveOptions[i] = moveOptions[j];
                        moveOptions[j] = temp;
                    }
                }
            }

            // Try all next moves based on the sort order
            for (int i = 0; i < 8; i++)
            {
                int k = moveOptions[i][0];
                next_x = x + xMove[k];
                next_y = y + yMove[k];

                if (isSquareSafe(next_x, next_y))
                {
                    boardGrid[next_x, next_y] = moveCount;
                    if (solveKTUtil(next_x, next_y, moveCount + 1))
                    {
                        return true;
                    }
                    else
                    {
                        // Backtracking
                        boardGrid[next_x, next_y] = -1;
                    }
                }
            }
            return false;
        }


        //this method counts unvisited 
        static int CountUnvisitedNeighbors(int x, int y)
        {
            int count = 0;
            // loop through all possible knight moves
            for (int k = 0; k < 8; k++)
            {
                // calculate the coordinates of the next potential move
                int next_x = x + xMove[k];
                int next_y = y + yMove[k];
                if (isSquareSafe(next_x, next_y))
                {
                     //increment the counter
                    count++;
                }
            }
            return count;
        }

        // a utility function to check if i and j are within the boundaries of the board
        static bool isSquareSafe(int x, int y)
        {
            return (x >= 0 && x < BoardSize && y >= 0 && y < BoardSize && boardGrid[x, y] == -1);
        }

        static void PrintSolution(int[,] solution)
        {
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    Console.Write(solution[x, y] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
