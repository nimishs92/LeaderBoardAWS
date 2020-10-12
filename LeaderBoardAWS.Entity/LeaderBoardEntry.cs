using System;

namespace LeaderBoardAWS.Entity
{
    public class LeaderBoardEntry
    {
        public string UserName { get; set; }
        public int Rank { get; set; }

        public int Kills { get; set; }

        public long Score { get; set; }
    }
}
