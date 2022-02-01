using System.Collections.Generic;
using UgolkiController;

namespace Tools.GraphSearch
{
    public interface IGraphSearch
    {
        List<int> GetPath(Dictionary<int, Node<Coord>> adjacencyList, int source, int destination);
    }
}