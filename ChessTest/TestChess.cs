using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDD_examples_1;

namespace ChessTest
{
    [TestClass]
    public class TestChess
    {
        Chess c = new Chess();

        public TestChess()
        {
            c.setupBoard();
        }
        [TestMethod]
        public void TestingTheSetup()
        {
            CheckPiecesOfColor(eColor.White);
            CheckPiecesOfColor(eColor.Black);
        }
        [TestMethod]
        public void getPieceAtSuccess()
        {
            //
            Assert.AreEqual(true, CheckIfPiecesAreTheSame(new Piece() { ePiece = ePiece.King, eColor = eColor.Black }, c.getPieceAt(7, 4)), "Det var ingen svart kung här.");
            Assert.AreEqual(true, CheckIfPiecesAreTheSame(new Piece() { ePiece = ePiece.Queen, eColor = eColor.White }, c.getPieceAt(0, 3)), "Det var ingen svart kung här.");
            Assert.AreEqual(true, CheckIfPiecesAreTheSame(new Piece() { ePiece = ePiece.Knight, eColor = eColor.Black }, c.getPieceAt(7, 6)), "Det var ingen svart kung här.");
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TryingToGetPieceThatIsOffTheBoard()
        {
            //Wrong row
            c.getPieceAt(8, 8);
        }
        [TestMethod]
        public void PuttingAPieceOnTheBoardSuccess()
        {
            c.putPiece(new Piece() { ePiece = ePiece.Queen, eColor = eColor.White }, 0, 3);
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TryingToPutPieceOffTheBoard()
        {
            //Wrong row
            c.putPiece(new Piece() { ePiece = ePiece.Queen, eColor = eColor.White }, 8, 8);
        }


        #region Pawn
        [TestMethod]
        public void PawnMoveSuccess()
        {
            c.movePiece(1, 3, 3, 3);
            c.movePiece(6, 4, 4, 4);
            c.movePiece(3, 3, 4, 4);
            c.movePiece(6, 5, 5, 5);
            c.movePiece(4, 4, 5, 4);
        }
        [TestMethod]
        [ExpectedException(typeof(NotAllowedMoveException))]
        public void PawnMovingToManySpacesFromBase()
        {
            c.movePiece(1, 0, 4, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(NotAllowedMoveException))]
        public void PawnMovingToThroughOtherPieceFromBase()
        {
            //Behöver skriva hästkod.
            c.movePiece(0, 1, 2, 2);
            c.movePiece(1, 2, 3, 2);
        }
        [TestMethod]
        [ExpectedException(typeof(NotAllowedMoveException))]
        public void PawnMovingToManySpacesFromField()
        {
            c.movePiece(1, 0, 2, 0);
            c.movePiece(2, 0, 4, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(NotAllowedMoveException))]
        public void PawnMovingBack()
        {
            c.movePiece(1, 0, 2, 0);
            c.movePiece(2, 0, 1, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(NotAllowedMoveException))]
        public void PawnMovingChaos()
        {
            c.movePiece(1, 0, 2, 0);
            c.movePiece(2, 0, 5, 5);
        }
        [TestMethod]
        [ExpectedException(typeof(NotAllowedMoveException))]
        public void PawnMovingÍntoOccupiedSpaceInFront()
        {
            c.movePiece(1, 0, 3, 0);
            c.movePiece(6, 0, 4, 0);
            //Here the error happends
            c.movePiece(3, 0, 4, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(MoveToFriendlyOccupiedSpaceException))]
        public void PawnMovingÍntoFriendlyOccupiedSpace()
        {
            c.movePiece(1, 1, 2, 1);
            c.movePiece(1, 0, 2, 1);
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void PawnMovingOffTheBoard()
        {
            c.movePiece(1, 7, 2, 8);
        }

        #endregion


        #region Rook
        [TestMethod]
        public void RookMoveSuccess()
        {
            c.movePiece(1, 0, 3, 0);
            c.movePiece(0, 0, 1, 0);
            c.movePiece(1, 0, 2, 0);
            c.movePiece(2, 0, 2, 4);
            c.movePiece(2, 4, 6, 4);
            c.movePiece(6, 4, 5, 4);
            c.movePiece(5, 4, 5, 6);
            c.movePiece(5, 6, 3, 6);
            c.movePiece(3, 6, 3, 2);
            c.movePiece(3, 2, 4, 2);
        }
        [TestMethod]
        [ExpectedException(typeof(NotAllowedMoveException))]
        public void RookMovingToThroughOtherPiece()
        {
            c.movePiece(0, 0, 4, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(NotAllowedMoveException))]
        public void RookNotMovingInAStraightLine()
        {
            c.movePiece(1, 0, 3, 0);
            c.movePiece(0, 0, 2, 0);
            c.movePiece(2, 0, 4, 4);
        }
        [TestMethod]
        [ExpectedException(typeof(MoveToFriendlyOccupiedSpaceException))]
        public void RookMovingÍntoFriendlyOccupiedSpace()
        {
            c.movePiece(0, 0, 1, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void RookMovingOffTheBoard()
        {
            c.movePiece(0,7,0,8);
        }

        #endregion


        #region Knight
        [TestMethod]
        public void KnightMoveSuccess()
        {
            c.movePiece(0,1,2,2);
            c.movePiece(2,2,4,1);
            c.movePiece(4,1,6,2);
            c.movePiece(6,2,7,4);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NotAllowedMoveException))]
        public void KnightNotMovingAsItShould()
        {
            c.movePiece(7, 1, 4, 4);
        }
        [TestMethod]
        [ExpectedException(typeof(MoveToFriendlyOccupiedSpaceException))]
        public void KnightMovingÍntoFriendlyOccupiedSpace()
        {
            c.movePiece(0, 1, 1, 3);
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void KnightMovingOffTheBoard()
        {
            c.movePiece(0, 1, -1, -1);
        }
        #endregion


        #region Bishop
        [TestMethod]
        public void BishopMoveSuccess()
        {
            c.movePiece(1, 3, 2, 3);
            c.movePiece(0, 2, 4, 6);
            c.movePiece(4, 6, 6, 4);
        }
        [TestMethod]
        [ExpectedException(typeof(NotAllowedMoveException))]
        public void BishopMovingToThroughOtherPiece()
        {
            c.movePiece(7, 5, 5, 7);
        }
        [TestMethod]
        [ExpectedException(typeof(NotAllowedMoveException))]
        public void BishopNotMovingInADiagonalLine()
        {
            c.movePiece(1, 2, 3, 2);
            c.movePiece(0, 2, 2,2);
        }
        [TestMethod]
        [ExpectedException(typeof(MoveToFriendlyOccupiedSpaceException))]
        public void BishopMovingÍntoFriendlyOccupiedSpace()
        {
            c.movePiece(0, 2, 1, 3);
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void BishopMovingOffTheBoard()
        {
            c.movePiece(7, 5, 8, 6);
        }
        #endregion


        #region Queen
        [TestMethod]
        public void QueenMoveSuccess()
        {
            c.movePiece(1, 3, 2, 3);
            c.movePiece(0, 3, 1, 3);
            c.movePiece(1, 3, 4, 0);
            c.movePiece(4, 0, 6, 0);
            c.movePiece(6, 0, 7, 1);
        }
        [TestMethod]
        [ExpectedException(typeof(NotAllowedMoveException))]
        public void QueenMovingLikeAKnightOrOtherChaos()
        {
            c.movePiece(0, 3, 2, 4);
        }
        [TestMethod]
        [ExpectedException(typeof(NotAllowedMoveException))]
        public void QueenMovingToThroughOtherPieceStraight()
        {
            c.movePiece(0, 3, 4, 3);
        }
        [TestMethod]
        [ExpectedException(typeof(NotAllowedMoveException))]
        public void QueenMovingToThroughOtherPieceInADiagonalLine()
        {
            c.movePiece(0, 3, 3, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(MoveToFriendlyOccupiedSpaceException))]
        public void QueenMovingÍntoFriendlyOccupiedSpace()
        {
            c.movePiece(0, 3, 1, 3);
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void QueenMovingOffTheBoard()
        {
            c.movePiece(7, 3, 8, 3);
        }
        #endregion


        #region King
        [TestMethod]
        public void KingMoveSuccess()
        {
            c.movePiece(1, 3, 2, 3);
            c.movePiece(0, 4, 1, 3);
        }
        [TestMethod]
        [ExpectedException(typeof(NotAllowedMoveException))]
        public void KingMovingToManySpaces()
        {
            c.movePiece(1, 4, 3, 4);
            c.movePiece(0, 4, 2, 4);
        }
        [TestMethod]
        [ExpectedException(typeof(MoveToFriendlyOccupiedSpaceException))]
        public void KingMovingToOccupiedSpace()
        {
            c.movePiece(0, 4, 1, 4);
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void KingMovingOffTheBoard()
        {
            c.movePiece(7, 4, 8, 4);
        }
        [TestMethod]
        [ExpectedException(typeof(PieceNotMovingException))]
        public void KingTryingToMakeANoneMove()
        {
            c.movePiece(7, 4, 7, 4);
        }
        #endregion
        

        #region Hjälpfunktioner
        public void CheckPiecesOfColor(eColor eC)
        {
            Piece[] p = new Piece[6];
            p[0] = new Piece() { eColor = eC, ePiece = ePiece.Pawn };
            p[1] = new Piece() { eColor = eC, ePiece = ePiece.Rook };
            p[2] = new Piece() { eColor = eC, ePiece = ePiece.Knight };
            p[3] = new Piece() { eColor = eC, ePiece = ePiece.Bishop };
            p[4] = new Piece() { eColor = eC, ePiece = ePiece.Queen };
            p[5] = new Piece() { eColor = eC, ePiece = ePiece.King };
            if (eC == eColor.White)
            {
                for (int i = 0; i < 8; i++)
                {
                    Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[0], c.getPieceAt(1, i)), "There was no white pawn here");
                }
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[1], c.getPieceAt(0, 0)), "There was no white rook here");
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[1], c.getPieceAt(0, 7)), "There was no white rook here");
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[2], c.getPieceAt(0, 1)), "There was no white knight here");
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[2], c.getPieceAt(0, 6)), "There was no white knight here");
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[3], c.getPieceAt(0, 2)), "There was no white Bishop here");
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[3], c.getPieceAt(0, 5)), "There was no white Bishop here");
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[4], c.getPieceAt(0, 3)), "There was no white Queen here");
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[5], c.getPieceAt(0, 4)), "There was no white King here");
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[0], c.getPieceAt(6, i)), "There was no black pawn here");
                }
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[1], c.getPieceAt(7, 0)), "There was no black rook here");
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[1], c.getPieceAt(7, 7)), "There was no black rook here");
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[2], c.getPieceAt(7, 1)), "There was no black knight here");
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[2], c.getPieceAt(7, 6)), "There was no black knight here");
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[3], c.getPieceAt(7, 2)), "There was no black Bishop here");
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[3], c.getPieceAt(7, 5)), "There was no black Bishop here");
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[4], c.getPieceAt(7, 3)), "There was no black Queen here");
                Assert.AreEqual(true, CheckIfPiecesAreTheSame(p[5], c.getPieceAt(7, 4)), "There was no black King here");
            }
            for (int i = 2; i < 6; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    Assert.AreEqual(true, CheckIfPiecesAreTheSame(new Piece() { ePiece = ePiece.None }, c.getPieceAt(i, k)), "Det står nått här");
                }
            }
        }
        public bool CheckIfPiecesAreTheSame(Piece p1, Piece p2)
        {
            return p1.ePiece == p2.ePiece && p1.eColor == p2.eColor;
        }
        #endregion

    }
}
