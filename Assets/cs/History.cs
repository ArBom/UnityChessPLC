using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Assets.cs
{
    class History
    {
        List<Relay> Relays = new List<Relay>();
        private Relay relay = new Relay() { Number = 1 };

        public string LastMove;

        public void NewMove(HistoryMove move)
        {
            if (relay.White == null)
            {
                relay.White = move;
            }
            else
            {
                relay.Black = move;
                Relays.Add(relay);
                relay = new Relay() { Number = (uint)(Relays.Count()+1) };
            }

            LastMove = move.ToShow();
            UnityEngine.MonoBehaviour.print(LastMove);
        }

        public string ListIt()
        {
            string toReturn = "";

            for (int i = 0; i != Relays.Count; ++i)
            {
                toReturn += Relays[i].Number + ". " + Relays[i].White.ToShow() + " " + Relays[i].Black.ToShow() + "\n";
            }

            if (relay.White != null)
                toReturn += relay.Number + ". " + relay.White.ToShow();

            return toReturn;
        }

        public void SaveToFile()
        {
            string path = EditorUtility.SaveFilePanel
            ("Save game state",
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "ChessGame_" + DateTime.Now.ToShortDateString() + ".txt",
            "txt");

            Task saveTask = new Task(() =>
            {
                string ListS = ListIt();
                byte[] ListB = Encoding.UTF8.GetBytes(ListS);

                using (FileStream file = File.Exists(path) ? File.OpenWrite(path) : File.Create(path))
                {
                    file.Write(ListB, 0, ListB.Length);
                    file.Close();
                }
            });

            saveTask.Start();
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

        public string Comment;
        public string ToShow()
        {
            if (BeginP == null || BeginP == String.Empty)
                return "";

            if (Castling)
                return Comment;

            string ToReturn = ChessmanIcon;

            if (Confution)
                ToReturn += ":";

            ToReturn = ToReturn + BeginP + "-" + EndP;

            if (KingInDanger)
                ToReturn += "+";

            return ToReturn;
        }
    }

    struct Relay
    {
        public uint Number;
        public HistoryMove White;
        public HistoryMove Black;
    }
}
