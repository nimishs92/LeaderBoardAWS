using LeaderBoardAWS.Entity;
using System.Collections.Generic;
using System.Linq;

namespace LeaderBoardAWS.Service
{
    public interface IPlayerStatsDynamoService
    {
        public IQueryable<PlayerStat> Get();

        public IList<PlayerStat> GetPlayerStats(string id);

        public IList<PlayerStat> GetPlayerStats(string id, int duration);

        public PlayerStat Create(PlayerStat playerStat);

    }
}