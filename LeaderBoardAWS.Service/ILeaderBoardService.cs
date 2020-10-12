using LeaderBoardAWS.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderBoardAWS.Service
{
    public interface ILeaderBoardService
    {
        public IEnumerable<LeaderBoardEntry> GetLeaderBoard(string match, int duration);
    }
}
