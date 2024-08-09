using System;

// Jack Parkison
// CST-250// 8/3/24
// This is my own work


namespace Minesweeper

{
    class Program
    {

        static void Main(string[] args)
        {

            GameBoard gameBoard = StartGame();
            gameBoard.SetupLiveNeighbors();
            gameBoard.CalculateLiveNeighbors();
            PrintBoard(gameBoard, null);
            RevealCell(gameBoard);

        }

        /// <summary>
        /// This method contains all of the initial inputs from the userto initialize the board and all of the game details
        /// </summary>
        /// <returns></returns>
        public static GameBoard StartGame()
        {
            Console.WriteLine("Welcome to Minesweeper!");
            Console.WriteLine();

            int selectedDifficulty;
            int boardSize;
            while (true)
            {
                Console.WriteLine("Please enter the size of the board you would like to play on (between 5-20):");
                string input = Console.ReadLine();

                // checks to make sure the input is valid
                if (int.TryParse(input, out boardSize))
                {
                    if (boardSize >= 5 && boardSize <= 20)
                    {
                        Console.WriteLine("The gameboard will be " + boardSize + " x " + boardSize);
                        break;
                    }
                    Console.WriteLine("Please enter a valid number");
                }
            }
            GameBoard gameBoard = new GameBoard(boardSize);


            while (true)
            {
                Console.WriteLine("Please select a difficulty: 1: Easy, 2: Medium, 3: Hard");
                string input = Console.ReadLine();

                // checks to make sure the input is valild
                if (int.TryParse(input, out selectedDifficulty))
                {
                    if (selectedDifficulty >= 1 && selectedDifficulty <= 3)
                    {
                        // switch statement with cases for each option for difficulty
                        switch (selectedDifficulty)
                        {
                            case (1):
                                gameBoard.Difficulty = 13.0;
                                Console.WriteLine("You chose: Easy");
                                break;
                            case (2):
                                gameBoard.Difficulty = 10.0;
                                Console.WriteLine("You chose: Medium");
                                break;
                            case (3):
                                gameBoard.Difficulty = 6.0;
                                Console.WriteLine("You chose: Hard");
                                break;
                        }
                        break;
                    }
                    Console.WriteLine("Please enter '1', '2', or '3'");
                }

            }
            return gameBoard;
        }


        /// <summary>
        /// This method Prints the initial blank board
        /// </summary>
        /// <returns></returns>
        public static void PrintBoard(GameBoard board, Cell chosenCell)
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    Console.Write("+---"); // sets the horizontal lines between cells
                }
                Console.WriteLine("+");

                //iterates each column
                for (int j = 0; j < board.Size; j++)
                {
                    Cell cell = board.Grid[i, j];
                    // sets the current chosen cell to visisted and shows the live neighbors
                    if (cell == chosenCell)
                    {
                        //board.FloodFill(i, j);
                        cell.Visited = true;
                        if (cell.LiveNeighbors > 0)
                        {
                            Console.Write($"| {board.Grid[i, j].LiveNeighbors} ");
                        }
                        else
                        {
                            Console.Write($"| ~ ");
                        }
                    }
                    // checks if a cell has already been visited and doesnt have live neighbors, if so, prints "~"
                    else if (cell.Visited && cell.LiveNeighbors == 0)
                    {
                        Console.Write("| ~ ");
                    }
                    // checks if a cell has already been visited and has live neighbors, if so prints number of live neighbors
                    else if (cell.Visited && cell.LiveNeighbors > 0)
                    {
                        Console.Write($"| {cell.LiveNeighbors} ");
                    }
                    //prints a blank box if the cell has not yet been visited
                    else
                    {
                        Console.Write("|   ");
                    }
                }
                Console.WriteLine("|");
            }
            for (int j = 0; j < board.Size; j++)
            {
                Console.Write("+---");
            }
            Console.WriteLine("+"); ;
        }

        public static void PrintEndGameBoard(GameBoard board)
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    Console.Write("+---"); // sets the horizontal lines between cells
                }
                Console.WriteLine("+");

                //iterates each column
                for (int j = 0; j < board.Size; j++)
                {
                    Cell cell = board.Grid[i, j];
                    // sets the current chosen cell to visisted and shows the live neighbors
                    if (cell.IsLive)
                    {
                        Console.Write($"| ! ");
                    }
                    else
                    {
                        Console.Write("|   ");
                    }
                }
                Console.WriteLine("|");
            }
            for (int j = 0; j < board.Size; j++)
            {
                Console.Write("+---");
            }
            Console.WriteLine("+"); ;
        }

        /// <summary>
        /// This method is the main logic for selecting cells and looping the game. It checks with each loop if endgame conditions are met. 
        /// </summary>
        /// <param name="board"></param>
        public static void RevealCell(GameBoard board)
        {
            // declare and initialize local variables. 
            bool isValid = false;
            bool gameOver = false;
            int row = 0;
            int col = 0;
            int totalCells = board.Size * board.Size;

            // continues to loop until the gameOver boolean is true
            while (!gameOver)
            {
                isValid = false;// sets the isValid back to false again for the next time this code is reached
                while (!isValid) // continues to loop until isValid is true. If inputs are not valid, this block loops again
                {
                    Console.WriteLine("Please select a row: ");
                    string selectedRow = Console.ReadLine();

                    if (int.TryParse(selectedRow, out row))
                    {
                        if (row >= 0 && row < board.Size)
                        {
                            isValid = true; // if the input can be parsed to an int, the isValid boolean is set to true
                        }
                        else
                        {
                            Console.WriteLine("Please select a valid number");//error handling
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please select a valid number");//error handling
                    }
                }

                isValid = false; // sets the isValid back to false again for the next time this code is reached
                while (!isValid) // continues to loop until isValid is true. If inputs are not valid, this block loops again
                {
                    Console.WriteLine("Please select a Column: ");
                    string selectedColumn = Console.ReadLine();

                    if (int.TryParse(selectedColumn, out col))
                    {
                        if (col >= 0 && col < board.Size)
                        {
                            isValid = true; // if the input can be parsed to an int, the isValid boolean is set to true
                        }
                        else
                        {
                            Console.WriteLine("Please select a valid number"); //error handling
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please select a valid number");//error handling
                    }
                }


                Cell chosenCell = board.Grid[row, col]; // assigns the selected input to chosenCell reference of Cell

                // call the FloodFill method passing the coordinates of the cell that was just chosen. 
                board.FloodFill(row, col);

                if (CheckWin(board))
                {
                    PrintEndGameBoard(board);
                    Console.WriteLine("YOU WIN!");
                    gameOver = true;
                }
                else if (CheckLose(chosenCell)) // checks if the chosen cell is a live bomb, if so end the loop thus ending the game
                {
                    PrintEndGameBoard(board);
                    Console.WriteLine("GAME OVER!");
                    gameOver = true;
                }
                else
                {
                    PrintBoard(board, chosenCell);
                }
                /*
                else // otherwise, print the board again with the cell showing its live neighbors
                {
                    //iterates each row
                    for (int i = 0; i < board.Size; i++)
                    {
                        for (int j = 0; j < board.Size; j++)
                        {
                            Console.Write("+---"); // sets the horizontal lines between cells
                        }
                        Console.WriteLine("+");

                        //iterates each column
                        for (int j = 0; j < board.Size; j++)
                        {
                            // sets the current chosen cell to visisted and shows the live neighbors
                            if (board.Grid[i, j] == chosenCell)
                            {
                                //board.FloodFill(i, j);
                                board.Grid[i, j].Visited = true;
                                Console.Write($"| {board.Grid[i, j].LiveNeighbors} ");
                            }
                            // checks if a cell has already been visited, if so, prints the neighbors
                            else if (board.Grid[i, j].Visited)
                            {
                                Console.Write($"| {board.Grid[i, j].LiveNeighbors} ");
                            }

                            //prints a blank box
                            else
                            {
                                Console.Write("|   ");
                            }
                        }
                        Console.WriteLine("|");
                    }
                    for (int j = 0; j < board.Size; j++)
                    {
                        Console.Write("+---");
                    }
                    Console.WriteLine("+");
                }
                */
            }
        }

        public static bool CheckWin(GameBoard board)
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    Cell cell = board.Grid[i, j];

                    // go through every cell. if any the cells are both not live and not visited, this functions returns false
                    if (!cell.IsLive && !cell.Visited)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool CheckLose(Cell cell)
        {
            // if the cell passed to this function is live, it returns true
            if (cell.IsLive)
            {
                return true;
            }
            return false;
        }
    }
}