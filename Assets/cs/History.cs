using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.cs
{
    class History
    {
        List<Relay> Relays = new List<Relay>();

        public string LastMove;

        public void NewMove(HistoryMove move)
        {

        }
    }

    public class HistoryMove
    {
        public string ChessmanIcon;
        public string BeginP;
        public string EndP;

        public bool Castling = false;
        public bool KingInDanger = false;
        public bool Promotion = false;
        public bool Confution = false;

        string Comment;
        string ToShow;
    }

    struct Relay
    {
        uint Number;
        HistoryMove White;
        HistoryMove Black;
    }
}
