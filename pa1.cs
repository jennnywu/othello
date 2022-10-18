#nullable enable
using System;
using static System.Console;

namespace Bme121
{
    static partial class Program
    {
        static void Main( )
        {
            string[ , ] game = NewBoard( rows: 8, cols: 8 );
            const string blank = " ";
            string playerWhite = "O";
            string playerBlack = "X";
            
            Console.Clear( );
            WriteLine( );
            WriteLine( "Welcome to Othello!" );
            WriteLine( );
            
            // get player names
            Write( "Black disc (X) player name [or <Enter>]: " );
            string blackDisc = ReadLine();
            
            if(blackDisc.Length > 0) WriteLine($"Black disc (X) player name is {blackDisc}");
            else blackDisc = "Black disc";
            
            Write( "White disc (O) player name [or <Enter>]: ");
            string whiteDisc = ReadLine();
            
            if (whiteDisc.Length > 0) WriteLine($"White disc (O) player name is {whiteDisc}");
            else whiteDisc = "White disc";
            
            DisplayBoard( game );
            WriteLine( );
            
            // start game with playerWhite
            string currentTurn = playerWhite;
            string response = "";

            // gameplay
            while(response != "quit" && checkGameOn())
            {
                if (currentTurn == playerBlack) Write("{0} enter a move: ", blackDisc);
                else Write("{0} enter a move: ", whiteDisc);
                
                response = ReadLine();
                IndexAtLetter(response);
                
                if (response == "skip")
                {
                    WriteLine("Turn skipped");
                    changeTurn();
                }
                else if (response != "quit")
                {
                    bool validMove = checkMove(response);
                    if (! validMove )
                    {
                        WriteLine("Invalid move, try again");
                        DisplayBoard(game);
                    }
                    else
                    {
                        int row = IndexAtLetter(response.Substring(0, 1));
                        int col = IndexAtLetter(response.Substring(1, 1));
                        
                        
                        
                        game[row, col] = currentTurn;
                        DisplayBoard(game);
                        changeTurn();
                    }
                    int scoreX = currentScoreX();
                    int scoreO = currentScoreO();
                    
                    WriteLine("Current score:");
                    WriteLine($"{blackDisc}: {scoreX}");
                    WriteLine($"{whiteDisc}: {scoreO}");
                }
                else changeTurn();
            }
            WriteLine("Game over");
            WriteLine($"Winner: {findWinner()}");

            // change turn between moves
            void changeTurn()
            {
                if (currentTurn == playerBlack)
                {
                    currentTurn = playerWhite;
                }
                else
                {
                    currentTurn = playerBlack;
                }
            }
            
            // return X or O based on current player
            string getDisc()
            {
                if (currentTurn == playerBlack)
                {
                    return playerBlack;
                }
                else
                {
                    return playerWhite;
                }
            }
            
            // return X or O based on next turn's player
            string notDisc()
            {
                if (currentTurn == playerBlack)
                {
                    return playerWhite;
                }
                else
                {
                    return playerBlack;
                }
            }
            
            // check legality of move
            bool checkMove(string response)
            {
                if (response == "quit")
                {
                    WriteLine("The game is over");
                    return false;
                }
                else
                {
                    if (response.Length != 2)
                    {
                        return false;
                    }
                    else
                    {
                        int row = IndexAtLetter(response.Substring(0, 1));
                        int col = IndexAtLetter(response.Substring(1, 1));
                        
                        if (row < 0 || row > game.GetLength(0) || col < 0 || col > game.GetLength(1))
                        {
                            return false;
                        }
                        else
                        {
                            if( game[row, col] != blank)
                            {
                                return false;
                            }
                            else
                            {
                                bool check = false;
                                
                                if(row == 0)
                                {
                                    if(col == 0)
                                    {
                                        if( game[row, col + 1] == notDisc() || game[row + 1, col + 1] == notDisc() || game[row + 1, col] == notDisc() )
                                        {
                                            if(game[row, col + 1] == notDisc())
                                            {
                                                if(tryDirection(0, 1)) check = true;
                                            }
                                            
                                            if (game[row + 1, col + 1] == notDisc())
                                            {
                                                if(tryDirection(1, 1)) check = true;
                                            }
                                            
                                            if (game[row + 1, col] == notDisc())
                                            {
                                                if(tryDirection(1, 0)) check = true;
                                            }
                                            
                                            if(check == true) return true;
                                            else return false;
                                        }
                                        else return false;
                                        
                                    }
                                    else if (col == 7)
                                    {
                                        if(game[row + 1, col] == notDisc() || game[row + 1, col - 1] == notDisc() || game[row, col - 1] == notDisc())
                                        {
                                            if(game[row + 1, col] == notDisc())
                                            {
                                                if(tryDirection(1, 0)) check = true;
                                            }
                                            
                                            if (game[row + 1, col - 1] == notDisc())
                                            {
                                                if(tryDirection(1, -1)) check = true;
                                            }
                                            
                                            if (game[row, col - 1] == notDisc())
                                            {
                                                if(tryDirection(0, -1)) check = true;
                                            }
                                            
                                            if(check == true) return true;
                                            else return false;
                                        }
                                        else return false;
                                    }
                                    else
                                    {
                                        if(game[row, col + 1] == notDisc() || game[row + 1, col + 1] == notDisc() || game[row + 1, col] == notDisc() || game[row + 1, col - 1] == notDisc() || game[row, col - 1] == notDisc())
                                        {
                                            if(game[row, col + 1] == notDisc())
                                            {
                                                if(tryDirection(0, 1)) check = true;
                                            }
                                            
                                            if(game[row + 1, col + 1] == notDisc())
                                            {
                                                if(tryDirection(1, 1)) check = true;
                                            }
                                            
                                            if(game[row + 1, col] == notDisc())
                                            {
                                                if(tryDirection(1, 0)) check = true;
                                            }
                                            
                                            if(game[row + 1, col - 1] == notDisc())
                                            {
                                                if(tryDirection(1, -1)) check = true;
                                            }
                                            
                                            if(game[row, col - 1] == notDisc())
                                            {
                                                if(tryDirection(0, -1)) check = true;
                                            }
                                            if(check == true) return true;
                                            else return false;
                                        }
                                        else return false;
                                    }
                                }
                                else if(row == 7)
                                {
                                    if(col == 0)
                                    {
                                        if(game[row - 1, col] == notDisc() || game[row - 1, col + 1] == notDisc() || game[row, col + 1] == notDisc())
                                        {
                                            if(game[row - 1, col] == notDisc())
                                            {
                                                if(tryDirection(-1, 0)) check = true;
                                            }
                                            
                                            if(game[row - 1, col + 1] == notDisc())
                                            {
                                                if(tryDirection(-1, 1)) check = true;
                                            }
                                            
                                            if(game[row, col + 1] == notDisc())
                                            {
                                                if(tryDirection(0, 1)) check = true;
                                            }
                                            
                                            if(check == true) return true;
                                            else return false;
                                        }
                                        else return false;
                                    }
                                    else if(col == 7)
                                    {
                                        if(game[row - 1, col - 1] == notDisc() || game[row - 1, col] == notDisc() || game[row, col - 1] == notDisc())
                                        {
                                            if(game[row - 1, col - 1] == notDisc())
                                            {
                                                if(tryDirection(-1, -1)) check = true;
                                            }
                                            
                                            if(game[row - 1, col] == notDisc())
                                            {
                                                if(tryDirection(-1, 0)) check = true;
                                            }
                                            
                                            if(game[row, col - 1] == notDisc())
                                            {
                                                if(tryDirection(0, -1)) check = true;
                                            }
                                            
                                            if(check == true) return true;
                                            else return false;
                                        }
                                        else return false;
                                    }
                                    else
                                    {
                                        if(game[row - 1, col - 1] == notDisc() || game[row - 1, col] == notDisc() || game[row - 1, col + 1] == notDisc() || game[row, col + 1] == notDisc() || game[row, col - 1] == notDisc())
                                        {
                                            if(game[row - 1, col - 1] == notDisc())
                                            {
                                                if(tryDirection(-1, -1)) check = true;
                                            }
                                            
                                            if(game[row - 1, col] == notDisc())
                                            {
                                                if(tryDirection(-1, 0)) check = true;
                                            }
                                            
                                            if(game[row - 1, col + 1] == notDisc())
                                            {
                                                if(tryDirection(-1, 1)) check = true;
                                            }
                                            
                                            if(game[row, col + 1] == notDisc())
                                            {
                                                if(tryDirection(0, 1)) check = true;
                                            }
                                            
                                            if(game[row, col - 1] == notDisc())
                                            {
                                                if(tryDirection(0, -1)) check = true;
                                            }
                                            
                                            if(check == true) return true;
                                            else return false;
                                        }
                                        else return false;
                                    }
                                }
                                else
                                {
                                    if(col == 0)
                                    {
                                        if(game[row - 1, col] == notDisc() || game[row - 1, col + 1] == notDisc() || game[row, col + 1] == notDisc() || game[row + 1, col + 1] == notDisc() || game[row + 1, col] == notDisc())
                                        {
                                            if(game[row - 1, col] == notDisc())
                                            {
                                                if(tryDirection(-1, 0)) check = true;
                                            }
                                            
                                            if(game[row - 1, col + 1] == notDisc())
                                            {
                                                if(tryDirection(-1, 1)) check = true;
                                            }
                                            
                                            if(game[row, col + 1] == notDisc())
                                            {
                                                if(tryDirection(0, 1)) check = true;
                                            }
                                            
                                            if(game[row + 1, col + 1] == notDisc())
                                            {
                                                if(tryDirection(1, 1)) check = true;
                                            }
                                            
                                            if(game[row + 1, col] == notDisc())
                                            {
                                                if(tryDirection(1, 0)) check = true;
                                            }
                                            
                                            if(check == true) return true;
                                            else return false;
                                        }
                                        else return false;
                                    }
                                    
                                    else if(col == 7)
                                    {
                                        if(game[row - 1, col - 1] == notDisc() || game[row - 1, col] == notDisc() || game[row + 1, col] == notDisc() || game[row + 1, col - 1] == notDisc() || game[row, col - 1] == notDisc())
                                        {
                                            if(game[row - 1, col - 1] == notDisc())
                                            {
                                                if(tryDirection(-1, -1)) check = true;
                                            }
                                            
                                            if(game[row - 1, col] == notDisc())
                                            {
                                                if(tryDirection(-1, 0)) check = true;
                                            }
                                            
                                            if(game[row + 1, col] == notDisc())
                                            {
                                                if(tryDirection(1, 0)) check = true;
                                            }
                                            
                                            if(game[row + 1, col - 1] == notDisc())
                                            {
                                                if(tryDirection(1, -1)) check = true;
                                            }
                                            
                                            if(game[row, col - 1] == notDisc())
                                            {
                                                if(tryDirection(0, -1)) check = true;
                                            }
                                            if(check == true) return true;
                                            else return false;
                                        }
                                        else return false;
                                    }
                                    
                                    else
                                    {
                                        
                                        if(game[row - 1, col - 1] == notDisc() || game[row - 1, col] == notDisc() || game[row - 1, col + 1] ==  notDisc() || game[row, col + 1] == notDisc() 
                                            || game[row + 1, col + 1] == notDisc() || game[row + 1, col] == notDisc() || game[row + 1, col - 1] == notDisc() || game[row, col - 1] == notDisc() )
                                        {
                                            if(game[row - 1, col - 1] == notDisc())
                                            {
                                                if(tryDirection(-1, -1)) check = true;
                                            }
                                            
                                            if(game[row - 1, col] == notDisc())
                                            {
                                                if(tryDirection(-1, 0)) check = true;
                                            }
                                            
                                            if(game[row - 1, col + 1] ==  notDisc())
                                            {
                                                if(tryDirection(-1, 1)) check = true;
                                            }
                                            
                                            if(game[row, col + 1] == notDisc())
                                            {
                                                if(tryDirection(0, 1)) check = true;
                                            }
                                            
                                            if(game[row + 1, col + 1] == notDisc())
                                            {
                                                if(tryDirection(1, 1)) check = true;
                                            }
                                            
                                            if(game[row + 1, col] == notDisc())
                                            {
                                                if(tryDirection(1, 0)) check = true;
                                            }
                                            
                                            if(game[row + 1, col - 1] == notDisc())
                                            {
                                                if(tryDirection(1, -1)) check = true;
                                            }
                                            
                                            if(game[row, col - 1] == notDisc())
                                            {
                                                if(tryDirection(0, -1)) check = true;
                                            }
                                            
                                            if(check == true) return true;
                                            else return false;
                                        }
                                        else return false;
                                    }
                                }
                            }
                        } 
                    }
                }
            }
            
            bool tryDirection(int deltaRow, int deltaCol)
            {
                int row = IndexAtLetter(response.Substring(0, 1));
                int col = IndexAtLetter(response.Substring(1, 1));
                
                int newDeltaRow = 0;
                int newDeltaCol = 0;
                
                bool valid = true;
                int count = 0;
                
                for(int i = 0; i < 8; i++)
                {
                    newDeltaRow += deltaRow;
                    newDeltaCol += deltaCol;
                    
                    // check if index is at end of array
                    if (row + newDeltaRow < 0 || row + newDeltaRow > 7 || col + newDeltaCol < 0 || col + newDeltaCol > 7)
                    {
                        valid = false;
                        i = 8;
                    }
                    
                    // check adjacent squares
                    else if (game[row + newDeltaRow, col + newDeltaCol] == notDisc())
                    {
                        count++;
                    }
                    else if(game[row + newDeltaRow, col + newDeltaCol] == blank)
                    {
                        valid = false;
                        i = 8;
                    }
                    else if(game[row + newDeltaRow, col + newDeltaCol] == getDisc())
                    {
                        valid = true;
                        i = 8;
                    }
                }
                
                newDeltaRow = 0;
                newDeltaCol = 0;

                if (valid == false) return false;
                else
                {
                    for(int i = 0; i < count; i++)
                    {
                        newDeltaRow = deltaRow + newDeltaRow;
                        newDeltaCol = deltaCol + newDeltaCol;
                        
                        game[row + newDeltaRow, col + newDeltaCol] = getDisc();
                    }
                    return true;
                }
            }
            
            bool checkGameOn()
            {
                int count = 0;
                
                for(int i = 0; i < game.GetLength(0); i++)
                {
                    for(int j = 0; j < game.GetLength(1); j++)
                    {
                        if(game[i, j] != blank)
                        {
                            count++;
                        }
                    }
                }
                
                if (count == 64) return false;
                else return true;
            }
            
            int currentScoreX()
            {
                int scoreX = 0;
                int scoreO = 0;
                
                for(int i = 0; i < game.GetLength(0); i++)
                {
                    for(int j = 0; j < game.GetLength(1); j++)
                    {
                        if(game[i, j] == "X") scoreX++;
                    }
                }
                return scoreX;
            }
            
            int currentScoreO()
            {
                int scoreX = 0;
                int scoreO = 0;
                
                for(int i = 0; i < game.GetLength(0); i++)
                {
                    for(int j = 0; j < game.GetLength(1); j++)
                    {
                        if(game[i, j] == "O") scoreO++;
                    }
                }
                return scoreO;
            }
            
            string findWinner()
            {
                int countX = 0;
                int countO = 0;
                
                for(int i = 0; i < game.GetLength(0); i++)
                {
                    for(int j = 0; j < game.GetLength(1); j++)
                    {
                        if(game[i, j] == "X") countX++;
                        else countO++;
                    }
                }
                
                if(countX > countO) return blackDisc;
                else if (countX < countO) return whiteDisc;
                else return "tie";
            }
        }
    }
}
