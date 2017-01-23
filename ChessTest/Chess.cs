using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD_examples_1;

namespace ChessTest
{
    class Chess : IChessGame
    {
        Piece[,] board = new Piece[8, 8];
        public void setupBoard()
        {

            //All pieces are white to begin with.

            putPiece(new Piece() { eColor = eColor.White, ePiece = ePiece.Rook }, 0, 0);
            putPiece(new Piece() { eColor = eColor.White, ePiece = ePiece.Knight }, 0, 1);
            putPiece(new Piece() { eColor = eColor.White, ePiece = ePiece.Bishop }, 0, 2);
            putPiece(new Piece() { eColor = eColor.White, ePiece = ePiece.Queen }, 0, 3);
            putPiece(new Piece() { eColor = eColor.White, ePiece = ePiece.King }, 0, 4);
            putPiece(new Piece() { eColor = eColor.White, ePiece = ePiece.Bishop }, 0, 5);
            putPiece(new Piece() { eColor = eColor.White, ePiece = ePiece.Knight }, 0, 6);
            putPiece(new Piece() { eColor = eColor.White, ePiece = ePiece.Rook }, 0, 7);
            for (int i = 0; i < 8; i++)
            {
                putPiece(new Piece() { eColor = eColor.White, ePiece = ePiece.Pawn }, 1, i);
            }
            for (int i = 2; i < 6; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    putPiece(new Piece() { ePiece = ePiece.None }, i, k);
                }
            }
            for (int i = 0; i < 8; i++)
            {
                putPiece(new Piece() { eColor = eColor.Black, ePiece = ePiece.Pawn }, 6, i);
            }
            putPiece(new Piece() { eColor = eColor.Black, ePiece = ePiece.Rook }, 7, 0);
            putPiece(new Piece() { eColor = eColor.Black, ePiece = ePiece.Knight }, 7, 1);
            putPiece(new Piece() { eColor = eColor.Black, ePiece = ePiece.Bishop }, 7, 2);
            putPiece(new Piece() { eColor = eColor.Black, ePiece = ePiece.Queen }, 7, 3);
            putPiece(new Piece() { eColor = eColor.Black, ePiece = ePiece.King }, 7, 4);
            putPiece(new Piece() { eColor = eColor.Black, ePiece = ePiece.Bishop }, 7, 5);
            putPiece(new Piece() { eColor = eColor.Black, ePiece = ePiece.Knight }, 7, 6);
            putPiece(new Piece() { eColor = eColor.Black, ePiece = ePiece.Rook }, 7, 7);

        }
        public Piece getPieceAt(int row, int column)
        {
            return board[row, column];
        }
        public void putPiece(Piece p, int row, int column)
        {
            board[row, column] = p;
        }
        public void movePiece(int rowFrom, int columnFrom, int rowTo, int columnTo)
        {
            Piece PieceToMove = getPieceAt(rowFrom, columnFrom);
            Piece PieceAtDestinaion = getPieceAt(rowTo, columnTo);
            if (rowFrom == rowTo && columnFrom == columnTo)
            {
                throw new PieceNotMovingException("");
            }
            switch (PieceToMove.ePiece)
            {
                case ePiece.None:
                    {
                        #region Kontrollerar ditt drag
                        throw new NotAllowedMoveException("Det står ingen pjäs här!");
                        #endregion
                        break;
                    }
                case ePiece.Pawn:
                    {
                        #region Kontrollerar ditt drag
                        switch (PieceToMove.eColor)
                        {
                            case eColor.Black:
                                {
                                    //Piece BlackPawn = new Piece() { ePiece = ePiece.Pawn, eColor = eColor.Black };
                                    int RowMovement = rowFrom - rowTo;
                                    int ColumnMovement = Math.Abs(columnFrom - columnTo);
                                    switch (RowMovement)
                                    {
                                        case 1:
                                            {
                                                switch (ColumnMovement)
                                                {
                                                    case 0:
                                                        {
                                                            //Kolla om nån står här
                                                            if (getPieceAt(rowTo, columnTo).ePiece == ePiece.None)
                                                            {
                                                                putPiece(getPieceAt(rowFrom, columnFrom), rowTo, columnTo);
                                                                putPiece(new Piece(), rowFrom, columnTo);
                                                            }
                                                            else
                                                            {
                                                                throw new NotAllowedMoveException("Det står någon rakt fram.");
                                                            }
                                                            break;
                                                        }
                                                    case 1:
                                                        {
                                                            Piece Target= getPieceAt(rowTo, columnTo);
                                                            //Kolla att det står en fiende här
                                                            if (Target.ePiece!=ePiece.None)
                                                            {
                                                                if (Target.eColor == eColor.White)
                                                                {
                                                                    putPiece(getPieceAt(rowFrom, columnFrom), rowTo, columnTo);
                                                                    putPiece(new Piece(), rowFrom, columnTo);
                                                                }
                                                                else
                                                                {
                                                                    throw new MoveToFriendlyOccupiedSpaceException("");
                                                                }

                                                            }
                                                            else
                                                            {
                                                                throw new NotAllowedMoveException("Det måste stå en fiende här.");
                                                            }
                                                            break;
                                                        }
                                                    default:
                                                        throw new NotAllowedMoveException("För många steg i kolumnerna");
                                                }
                                                break;
                                            }
                                        case 2:
                                            {
                                                if (ColumnMovement==0)
                                                {
                                                    if (rowFrom==6)
                                                    {
                                                        if (getPieceAt(5,columnFrom).ePiece==ePiece.None)
                                                        {
                                                            putPiece(getPieceAt(rowFrom, columnFrom), rowTo, columnTo);
                                                            putPiece(new Piece(), rowFrom, columnTo);
                                                        }
                                                        else
                                                        {
                                                            throw new NotAllowedMoveException("Det står en pjäs i vägen");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        throw new NotAllowedMoveException("Felaktig startrad");
                                                    }
                                                }
                                                else
                                                {
                                                    throw new NotAllowedMoveException("Felaktig rörelse i kolumnerna");
                                                }
                                                break;
                                            }
                                        default:
                                            {
                                                throw new NotAllowedMoveException("Felaktig rörelse i Raderna");

                                            }
                                    }


                                    break;
                                }
                            case eColor.White:
                                {
                                    //Piece BlackPawn = new Piece() { ePiece = ePiece.Pawn, eColor = eColor.Black };
                                    int RowMovement = rowTo - rowFrom;
                                    int ColumnMovement = Math.Abs(columnFrom - columnTo);
                                    switch (RowMovement)
                                    {
                                        case 1:
                                            {
                                                switch (ColumnMovement)
                                                {
                                                    case 0:
                                                        {
                                                            //Kolla om nån står här
                                                            if (getPieceAt(rowTo, columnTo).ePiece == ePiece.None)
                                                            {
                                                                putPiece(getPieceAt(rowFrom, columnFrom), rowTo, columnTo);
                                                                putPiece(new Piece(), rowFrom, columnTo);
                                                            }
                                                            else
                                                            {
                                                                throw new NotAllowedMoveException("Det står någon rakt fram.");
                                                            }
                                                            break;
                                                        }
                                                    case 1:
                                                        {
                                                            Piece Target = getPieceAt(rowTo, columnTo);
                                                            //Kolla att det står en fiende här
                                                            if (Target.ePiece != ePiece.None)
                                                            {
                                                                if (Target.eColor == eColor.Black)
                                                                {
                                                                    putPiece(getPieceAt(rowFrom, columnFrom), rowTo, columnTo);
                                                                    putPiece(new Piece(), rowFrom, columnTo);
                                                                }
                                                                else
                                                                {
                                                                    throw new MoveToFriendlyOccupiedSpaceException("");
                                                                }

                                                            }
                                                            else
                                                            {
                                                                throw new NotAllowedMoveException("Det måste stå en fiende här.");
                                                            }
                                                            break;
                                                        }
                                                    default:
                                                        throw new NotAllowedMoveException("För många steg i kolumnerna");
                                                }
                                                break;
                                            }
                                        case 2:
                                            {
                                                if (ColumnMovement == 0)
                                                {
                                                    if (rowFrom == 1)
                                                    {
                                                        if (getPieceAt(2, columnFrom).ePiece == ePiece.None)
                                                        {
                                                            putPiece(getPieceAt(rowFrom, columnFrom), rowTo, columnTo);
                                                            putPiece(new Piece(), rowFrom, columnTo);
                                                        }
                                                        else
                                                        {
                                                            throw new NotAllowedMoveException("Det står en pjäs i vägen");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        throw new NotAllowedMoveException("Felaktig startrad");
                                                    }
                                                }
                                                else
                                                {
                                                    throw new NotAllowedMoveException("Felaktig rörelse i kolumnerna");
                                                }
                                                break;
                                            }
                                        default:
                                            {
                                                throw new NotAllowedMoveException("Felaktig rörelse i Raderna");

                                            }
                                    }


                                    break;
                                }
                            default:
                                break;
                        }
                        #endregion
                        break;
                    }
                case ePiece.Rook:
                    {
                        #region Kontrollerar ditt drag
                        if(rowFrom!=rowTo&&columnFrom!=columnTo)
                        {
                            throw new NotAllowedMoveException("Du går inte i en rak linje");
                        }
                        else
                        {
                            if (PieceToMove.eColor==PieceAtDestinaion.eColor&&PieceAtDestinaion.ePiece!=ePiece.None)
                            {
                                throw new MoveToFriendlyOccupiedSpaceException("");
                            }
                            if (rowFrom==rowTo)
                            {
                                //kolla om nån står ivägen i kolumnen
                                if (columnFrom<columnTo)
                                {
                                    for (int i = columnFrom+1; i < columnTo; i++)
                                    {
                                        if (getPieceAt(rowFrom,i).ePiece!=ePiece.None)
                                        {
                                            throw new NotAllowedMoveException("Det står en pjäs ivägen");
                                        }
                                        
                                    }
                                    putPiece(PieceToMove, rowTo, columnTo);
                                    putPiece(new Piece(), rowFrom, columnFrom);
                                }
                                else
                                {
                                    for (int i = columnTo+1; i < columnFrom; i++)
                                    {
                                        if (getPieceAt(rowFrom, i).ePiece != ePiece.None)
                                        {
                                            throw new NotAllowedMoveException("Det står en pjäs ivägen");
                                        }
                                        
                                    }
                                    putPiece(PieceToMove, rowTo, columnTo);
                                    putPiece(new Piece(), rowFrom, columnFrom);
                                }
                                
                            }
                            else
                            {
                                //kolla om nån står ivägen i raden
                                if (rowFrom < rowTo)
                                {
                                    for (int i = rowFrom + 1; i < rowTo; i++)
                                    {
                                        if (getPieceAt(i, columnFrom).ePiece != ePiece.None)
                                        {
                                            throw new NotAllowedMoveException("Det står en pjäs ivägen");
                                        }
                                        
                                    }
                                    putPiece(PieceToMove, rowTo, columnTo);
                                    putPiece(new Piece(), rowFrom, columnFrom);
                                }
                                else
                                {
                                    for (int i = rowTo + 1; i < rowFrom; i++)
                                    {
                                        if (getPieceAt(i,columnFrom).ePiece != ePiece.None)
                                        {
                                            throw new NotAllowedMoveException("Det står en pjäs ivägen");
                                        }
                                        
                                    }
                                    putPiece(PieceToMove, rowTo, columnTo);
                                    putPiece(new Piece(), rowFrom, columnFrom);
                                }
                            }
                        }
                        #endregion
                        break;
                    }
                case ePiece.Knight:
                    {
                        #region Kontrollerar ditt drag
                        if (PieceToMove.eColor==PieceAtDestinaion.eColor&& PieceAtDestinaion.ePiece != ePiece.None)
                        {
                            throw new MoveToFriendlyOccupiedSpaceException("");
                        }
                        if ((Math.Abs(rowFrom-rowTo)==2&& Math.Abs(columnFrom-columnTo)==1)|| (Math.Abs(rowFrom - rowTo) ==1&& Math.Abs(columnFrom - columnTo) ==2))
                        {
                            putPiece(PieceToMove, rowTo, columnTo);
                            putPiece(new Piece(), rowFrom, columnFrom);
                        }
                        else
                        {
                            throw new NotAllowedMoveException("Felaktigt drag");
                        }

                        #endregion
                        break;
                    }
                case ePiece.Bishop:
                    {
                        #region Kontrollerar ditt drag
                        if (PieceToMove.eColor == PieceAtDestinaion.eColor && PieceAtDestinaion.ePiece != ePiece.None)
                        {
                            throw new MoveToFriendlyOccupiedSpaceException("");
                        }
                        if ((Math.Abs(rowFrom-rowTo)-Math.Abs(columnFrom-columnTo))!=0)
                        {
                            throw new NotAllowedMoveException("Du går inte som man skall med Biskopen");
                        }
                        int k;
                        if (rowTo-rowFrom>0)
                        {
                            if (columnTo-columnFrom>0)
                            {
                                k = columnFrom+1;
                                for (int i = rowFrom+1; i < rowTo; i++)
                                {                                    
                                    if (getPieceAt(i,k).ePiece!=ePiece.None)
                                    {
                                        throw new NotAllowedMoveException("Du försöker gå igenom en pjäs");
                                    }
                                    k++;
                                }

                            }
                            else
                            {
                                k = columnFrom - 1;
                                for (int i = rowFrom + 1; i < rowTo; i++)
                                {
                                    if (getPieceAt(i, k).ePiece != ePiece.None)
                                    {
                                        throw new NotAllowedMoveException("Du försöker gå igenom en pjäs");
                                    }
                                    k--;
                                }
                            }
                        }
                        else
                        {
                            if (columnTo - columnFrom > 0)
                            {
                                k = columnFrom + 1;
                                for (int i = rowFrom - 1; i > rowTo; i--)
                                {
                                    if (getPieceAt(i, k).ePiece != ePiece.None)
                                    {
                                        throw new NotAllowedMoveException("Du försöker gå igenom en pjäs");
                                    }
                                    k++;
                                }

                            }
                            else
                            {
                                k = columnFrom - 1;
                                for (int i = rowFrom - 1; i > rowTo; i--)
                                {
                                    if (getPieceAt(i, k).ePiece != ePiece.None)
                                    {
                                        throw new NotAllowedMoveException("Du försöker gå igenom en pjäs");
                                    }
                                    k--;
                                }
                            }
                        }
                        putPiece(PieceToMove, rowTo, columnTo);
                        putPiece(new Piece(), rowFrom, columnFrom);
                        #endregion
                        break;
                    }
                case ePiece.Queen:
                    {
                        #region Kontrollerar ditt drag
                        if (PieceToMove.eColor == PieceAtDestinaion.eColor && PieceAtDestinaion.ePiece != ePiece.None)
                        {
                            throw new MoveToFriendlyOccupiedSpaceException("");
                        }
                        if((rowFrom != rowTo && columnFrom != columnTo)&&((Math.Abs(rowFrom - rowTo) - Math.Abs(columnFrom - columnTo)) != 0))
                        {
                            throw new NotAllowedMoveException("Du går varken rakt eller diagonalt");
                        }
                        if (rowFrom == rowTo)
                        {
                            //kolla om nån står ivägen i kolumnen
                            if (columnFrom < columnTo)
                            {
                                for (int i = columnFrom + 1; i < columnTo; i++)
                                {
                                    if (getPieceAt(rowFrom, i).ePiece != ePiece.None)
                                    {
                                        throw new NotAllowedMoveException("Det står en pjäs ivägen");
                                    }
                                    
                                }
                                putPiece(getPieceAt(rowFrom, columnFrom), rowTo, columnTo);
                                putPiece(new Piece(), rowFrom, columnFrom);
                            }
                            else
                            {
                                for (int i = columnTo + 1; i < columnFrom; i++)
                                {
                                    if (getPieceAt(rowFrom, i).ePiece != ePiece.None)
                                    {
                                        throw new NotAllowedMoveException("Det står en pjäs ivägen");
                                    }
                                    
                                }
                                putPiece(getPieceAt(rowFrom, columnFrom), rowTo, columnTo);
                                putPiece(new Piece(), rowFrom, columnFrom);
                            }

                        }
                        else if (columnFrom == columnTo)

                        {
                            //kolla om nån står ivägen i raden
                            if (rowFrom < rowTo)
                            {
                                for (int i = rowFrom + 1; i < rowTo; i++)
                                {
                                    if (getPieceAt(i, columnFrom).ePiece != ePiece.None)
                                    {
                                        throw new NotAllowedMoveException("Det står en pjäs ivägen");
                                    }
    
                                    
                                }
                                putPiece(getPieceAt(rowFrom, columnFrom), rowTo, columnTo);
                                putPiece(new Piece(), rowFrom, columnFrom);
                            }
                            else
                            {
                                for (int i = rowTo + 1; i < rowFrom; i++)
                                {
                                    if (getPieceAt(i, columnFrom).ePiece != ePiece.None)
                                    {
                                        throw new NotAllowedMoveException("Det står en pjäs ivägen");
                                    }
                                }
                                putPiece(getPieceAt(rowFrom, columnFrom), rowTo, columnTo);
                                putPiece(new Piece(), rowFrom, columnFrom);
                            }
                        }
                        else
                        {
                            int k;
                            if (rowTo - rowFrom > 0)
                            {
                                if (columnTo - columnFrom > 0)
                                {
                                    k = columnFrom + 1;
                                    for (int i = rowFrom + 1; i < rowTo; i++)
                                    {
                                        if (getPieceAt(i, k).ePiece != ePiece.None)
                                        {
                                            throw new NotAllowedMoveException("Du försöker gå igenom en pjäs");
                                        }
                                        k++;
                                    }

                                }
                                else
                                {
                                    k = columnFrom - 1;
                                    for (int i = rowFrom + 1; i < rowTo; i++)
                                    {
                                        if (getPieceAt(i, k).ePiece != ePiece.None)
                                        {
                                            throw new NotAllowedMoveException("Du försöker gå igenom en pjäs");
                                        }
                                        k--;
                                    }
                                }
                            }
                            else
                            {
                                if (columnTo - columnFrom > 0)
                                {
                                    k = columnFrom + 1;
                                    for (int i = rowFrom - 1; i > rowTo; i--)
                                    {
                                        if (getPieceAt(i, k).ePiece != ePiece.None)
                                        {
                                            throw new NotAllowedMoveException("Du försöker gå igenom en pjäs");
                                        }
                                        k++;
                                    }

                                }
                                else
                                {
                                    k = columnFrom - 1;
                                    for (int i = rowFrom - 1; i > rowTo; i--)
                                    {
                                        if (getPieceAt(i, k).ePiece != ePiece.None)
                                        {
                                            throw new NotAllowedMoveException("Du försöker gå igenom en pjäs");
                                        }
                                        k--;
                                    }
                                }
                            }
                            putPiece(PieceToMove, rowTo, columnTo);
                            putPiece(new Piece(), rowFrom, columnFrom);
                        }
                            #endregion
                            break;
                    }
                case ePiece.King:
                    {
                        #region Kontrollerar ditt drag
                        if (rowTo == rowFrom && columnTo == columnFrom)
                        {
                            throw new PieceNotMovingException("");
                        }
                        if (!(Math.Abs(rowFrom - rowTo) <= 1 && Math.Abs(columnFrom - columnTo) <= 1))
                        {
                            throw new NotAllowedMoveException("För många steg");
                        }
                        if (!(ePiece.None == PieceAtDestinaion.ePiece))
                        {
                            if (PieceToMove.eColor == PieceAtDestinaion.eColor)
                            {
                                throw new MoveToFriendlyOccupiedSpaceException("Samma färg får man inte gå till");
                            }
                            else
                            {
                                putPiece(PieceToMove, rowTo, columnTo);
                                putPiece(new Piece(), rowFrom, columnFrom);
                            }
                        }
                        else
                        {
                            putPiece(PieceToMove, rowTo, columnTo);
                            putPiece(new Piece(), rowFrom, columnFrom);
                        }
                        #endregion
                        break;
                    }
                default:
                    break;
            }
        }

    }
}
