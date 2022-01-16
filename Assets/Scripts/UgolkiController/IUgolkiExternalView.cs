using System.Collections.Generic;

namespace UgolkiController
{
    public interface IUgolkiExternalView
    {
        void StartGame(BoardCellType[,] board, int boardSize);
        void EndGame(BoardCellType[,] board);
        void SelectPiece(Coord coord);
        void DeselectPiece(Coord coord);
        void MovePiece(List<Move> path);
    }
}