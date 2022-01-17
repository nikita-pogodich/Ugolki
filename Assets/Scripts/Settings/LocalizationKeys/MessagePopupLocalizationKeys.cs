using System.Collections.Generic;
using UgolkiController;

namespace Settings.LocalizationKeys
{
    public class MessagePopupLocalizationKeys
    {
        public const string NotYourMoveMessage = "not_your_move_message";
        public const string SelectPieceMessage = "select_piece_message";
        public const string MoveUnreachableMessage = "move_unreachable_message";

        public static readonly IReadOnlyDictionary<string, string> UgolkiMessagesMap = new Dictionary<string, string>
        {
            {UgolkiMessages.NotYourMove, NotYourMoveMessage},
            {UgolkiMessages.SelectPiece, SelectPieceMessage},
            {UgolkiMessages.MoveUnreachable, MoveUnreachableMessage}
        };
    }
}