using System.Collections.Generic;

namespace UgolkiController
{
    public class UgolkiController : IUgolkiController
    {
        private List<string> _rules;
        private string _currentRule;
        
        public BoardCellType[,] Board { get; }

        public UgolkiController()
        {
            _rules = new List<string>
            {
                UgolkiRules.Rule1,
                UgolkiRules.Rule2,
                UgolkiRules.Rule3
            };
        }

        public List<string> GetRules()
        {
            return _rules;
        }

        public void SetRule(string rule)
        {
            _currentRule = rule;
        }

        public void StartGame()
        {
            throw new System.NotImplementedException();
        }

        public void EndGame()
        {
            throw new System.NotImplementedException();
        }

        public Player CheckWinner()
        {
            throw new System.NotImplementedException();
        }

        public bool TrySelectPiece(Coord cell, out string errorType)
        {
            throw new System.NotImplementedException();
        }

        public bool TryMovePiece(Coord @from, Coord to, Player player, string errorType)
        {
            throw new System.NotImplementedException();
        }
    }
}