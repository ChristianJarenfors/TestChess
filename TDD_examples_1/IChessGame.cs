using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_examples_1
{
    public interface IChessGame
    {
        /* Note that both rows and columns range from 1 to 8.
         * 
         * When testing a specific function, you may assume
         * that every other function works as intended, because
         * they should be tested separately.
         * 
         * getPieceAt and putPiece will be useful when testing
         * movePiece.
         */

        /* Initializes the board and puts every piece at its
         * starting position.
         */
        void setupBoard();

        /* Returns the piece at the specified row and
         * column or Piece.None.
         */
        Piece getPieceAt(int row, int column);

        /* Puts a piece at a specific position, overwriting any
         * other piece currently occupying that position.
         */
        void putPiece(Piece p, int row, int column);

        /* Attempts to move a piece from a position to another
         * position. If successful, the origin position should
         * be empty and the destination position should change
         * to the moving piece.
         * If unsuccessful, nothing should change.
         * If there is no piece at the origin it should throw
         * a NoPieceAtPositionException.
         * If the suggested move if not possible for any reason
         * it should throw an IllegalMoveException.
         */
        void movePiece(int rowFrom, int columnFrom, int rowTo, int columnTo);
    }

    public class IllegalMoveException : Exception { }
    public class NoPieceAtPositionException : Exception { }

    public class Piece
    {
        public ePiece ePiece;
        public eColor eColor;
    }
    public enum ePiece
    {
        None,
        Pawn,    // bonde
        Rook,    // torn
        Knight,  // häst/springare
        Bishop,  // löpare
        Queen,   // drottning
        King     // kung
    }
    public enum eColor
    {
        Black,
        White
    }
    public class NotAllowedMoveException : Exception
    {
        public NotAllowedMoveException(string message) : base(message) { }
    }
    public class MoveToFriendlyOccupiedSpaceException : Exception
    {
        public MoveToFriendlyOccupiedSpaceException(string message) : base(message) { }
    }
    public class PieceNotMovingException : Exception
    {
        public PieceNotMovingException(string message) : base(message) { }
    }
    //public class Chess : IChessGame
}
