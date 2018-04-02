using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Piece
    {
        public int Colour { get; private set; }

        public Piece(int Team)
        {
            Colour = Team;
        }
        
        private static int[] Piece_taken = null;
        private static int[] TakePieceMove = null;

        public static Piece[,] Move(Piece[,] Board, int from_X, int from_Y, int to_X, int to_Y)
        {
            List<int[]> PossibleMoves = GetLegalMoves(Board, from_X, from_Y);

            foreach (int[] Move in PossibleMoves)
            {
                if (Move[0] == to_X && Move[1] == to_Y)
                {
                    if (Piece_taken != null && TakePieceMove[0] == Move[0] && TakePieceMove[1] == Move[1])
                    {
                        Board[Piece_taken[0], Piece_taken[1]] = null;
                        Piece_taken = null;
                    }

                    Board[to_X, to_Y] = Board[from_X, from_Y];
                    Board[from_X, from_Y] = null;
                    break;
                }
            }

            return Board;
        }

        public static List<int[]> GetLegalMoves(Piece[,] Board, int X, int Y)
        {
            List<int[]> PossibleMoves = new List<int[]>();
            int[] move;

            if (Board[X,Y].Colour == 0)
            {
                if (Y - 1 >= 0 && X + 1 < 8)
                {
                    if (Board[X + 1, Y - 1] == null)
                    {
                        move = new int[] { X + 1, Y - 1 };
                        PossibleMoves.Add(move);
                    }

                    else if (Y - 2 >= 0 && X + 2 < 8)
                    {
                        if (Board[X + 2, Y - 2] == null && Board[X + 1, Y - 1].Colour == 1)
                        {
                            move = new int[] { X + 2, Y - 2 };
                            PossibleMoves.Add(move);

                            Piece_taken = new int[] { X + 1, Y - 1 };
                            TakePieceMove = new int[] { X + 2, Y - 2 };
                        }
                    }
                }

                if (Y + 1 < 8 && X + 1 < 8)
                {
                    if (Board[X + 1, Y + 1] == null)
                    {
                        move = new int[] { X + 1, Y + 1 };
                        PossibleMoves.Add(move);
                    }

                    else if (Y + 2 < 8 && X + 2 < 8)
                    {
                        if (Board[X + 2, Y + 2] == null && Board[X + 1, Y + 1].Colour == 1)
                        {
                            move = new int[] { X + 2, Y + 2 };
                            PossibleMoves.Add(move);

                            Piece_taken = new int[] { X + 1, Y + 1 };
                            TakePieceMove = new int[] { X + 2, Y + 2 };
                        }
                    }
                }
            }

            else if (Board[X, Y].Colour == 1)
            {
                if (Y - 1 >= 0 && X - 1 >= 0)
                {
                    if (Board[X - 1, Y - 1] == null)
                    {
                        move = new int[] { X - 1, Y - 1 };
                        PossibleMoves.Add(move);
                    }

                    else if (Y - 2 >= 0 && X - 2 >= 0)
                    {
                        if (Board[X - 2, Y - 2] == null && Board[X - 1, Y - 1].Colour == 0)
                        {
                            move = new int[] { X - 2, Y - 2 };
                            PossibleMoves.Add(move);

                            Piece_taken = new int[] { X - 1, Y - 1 };
                            TakePieceMove = new int[] { X - 2, Y - 2 };
                        }
                    }
                }

                if (Y + 1 < 8 && X - 1 >= 0)
                {
                    if (Board[X - 1, Y + 1] == null)
                    {
                        move = new int[] { X - 1, Y + 1 };
                        PossibleMoves.Add(move);
                    }

                    else if (Y + 2 < 8 && X - 2 >= 0)
                    {
                        if (Board[X - 2, Y + 2] == null && Board[X - 1, Y + 1].Colour == 0)
                        {
                            move = new int[] { X - 2, Y + 2 };
                            PossibleMoves.Add(move);

                            Piece_taken = new int[] { X - 1, Y + 1 };
                            TakePieceMove = new int[] { X - 2, Y + 2 };
                        }
                    }
                }
            }

            return PossibleMoves;
        }
    }
}
