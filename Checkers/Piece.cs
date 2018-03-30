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

        private void Moves(Piece[,] Board, int from_X, int from_Y, int to_X, int to_Y)
        {
            List<int[,]> PossibleMoves = GetLegalMoves(from_X, from_Y);

            //IF to_X,to_Y in PossibleMoves Move Piece
        }

        private static List<int[,]> GetLegalMoves(int X, int Y)
        {
            List<int[,]> PossibleMoves = new List<int[,]>();

            //calculate possible moves here

            return PossibleMoves;
        }
    }
}
