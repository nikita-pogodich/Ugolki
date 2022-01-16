using System;

namespace UgolkiController
{
    public class OutOfBoardException : Exception
    {
    }
    
    public readonly struct Coord
    {
        private readonly int _row;
        private readonly int _column;

        public int Row => _row;
        public int Column => _column;

        public Coord(int row, int column)
        {
            _row = row;
            _column = column;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                Coord coord = (Coord)obj;
                return _row == coord.Row && _column == coord.Column;
            }
        }

        public override int GetHashCode()
        {
            return _row * 8 + _column;
        }

        public static bool operator ==(Coord coord1, Coord coord2)
        {
            return coord1.Equals(coord2);
        }

        public static bool operator !=(Coord coord1, Coord coord2)
        {
            return !coord1.Equals(coord2);
        }
    }
}