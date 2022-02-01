using System.Collections.Generic;
using Tools;

namespace UgolkiController.UgolkiRules
{
    public abstract class BaseUgolkiRule
    {
        public abstract void TryAddAvailableMoves(
            BoardCellType[,] board,
            Coord fromCell,
            Dictionary<int, Node<Coord>> graph,
            Queue<Coord> toCheck,
            List<Coord> canJump);

        protected void TryAddAvailableJump(
            BoardCellType[,] board,
            Coord currentFrom,
            int row,
            int column,
            Dictionary<int, Node<Coord>> graph,
            List<Coord> canJump,
            Queue<Coord> toCheck)
        {
            Coord currentTo;
            Coord currentToJump;

            try
            {
                currentTo = new Coord(currentFrom.Row + row, currentFrom.Column + column);
                currentToJump = new Coord(currentFrom.Row + row * 2, currentFrom.Column + column * 2);
            }
            catch (OutOfBoardException)
            {
                return;
            }

            if (board[currentTo.Row, currentTo.Column] != BoardCellType.Empty &&
                board[currentToJump.Row, currentToJump.Column] == BoardCellType.Empty &&
                canJump.Contains(currentToJump) == false)
            {
                toCheck.Enqueue(currentToJump);
                canJump.Add(currentToJump);

                Node<Coord> toJumpNode = null;

                //TODO optimize this
                foreach (Node<Coord> graphValue in graph.Values)
                {
                    if (graphValue.Value == currentToJump)
                    {
                        toJumpNode = graphValue;
                        break;
                    }
                }

                if (toJumpNode == null)
                {
                    toJumpNode = new Node<Coord>(graph.Count, currentToJump);
                    graph.Add(toJumpNode.Id, toJumpNode);
                }

                Node<Coord> fromNode = null;

                foreach (Node<Coord> graphValue in graph.Values)
                {
                    if (graphValue.Value == currentFrom)
                    {
                        fromNode = graphValue;
                    }
                }

                fromNode?.AddEdge(toJumpNode.Id);
            }
        }

        protected void TryAddAvailableMove(BoardCellType[,] board, Coord from, int row, int column, List<Coord> canJump)
        {
            Coord currentTo;

            try
            {
                currentTo = new Coord(from.Row + row, from.Column + column);
            }
            catch (OutOfBoardException)
            {
                return;
            }

            if (board[currentTo.Row, currentTo.Column] == BoardCellType.Empty &&
                canJump.Contains(currentTo) == false)
            {
                canJump.Add(currentTo);
            }
        }

        protected void FillGraphMoves(Dictionary<int, Node<Coord>> graph, List<Coord> canJump)
        {
            for (int i = 1; i < canJump.Count; i++)
            {
                Node<Coord> value = new Node<Coord>(graph.Count, canJump[i]);
                graph[0].AddEdge(value.Id);
                graph.Add(graph.Count, value);
            }
        }
    }
}