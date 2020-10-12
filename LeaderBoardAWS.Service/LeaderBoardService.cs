using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using LeaderBoardAWS.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderBoardAWS.Service
{
    public class LeaderBoardService : ILeaderBoardService
    {
        private AmazonDynamoDBClient _client;
        public LeaderBoardService(DynamoDBClientService dynamoDBClientService)
        {
            _client = dynamoDBClientService.CreateClient();
        }
        public IEnumerable<LeaderBoardEntry> GetLeaderBoard(string match, int duration)
        {
            DynamoDBContext context = new DynamoDBContext(_client);

            QueryFilter filter = new QueryFilter();
            filter.AddCondition("Match", QueryOperator.Equal, match);
            filter.AddCondition("TimeStamp", QueryOperator.GreaterThanOrEqual, DateTime.Now.AddHours(-duration));

            QueryOperationConfig queryOperationConfig = new QueryOperationConfig { Filter = filter, IndexName= "LeaderBoardIndex"};

            var playerStats = context.FromQueryAsync<PlayerStat>(queryOperationConfig).GetNextSetAsync().GetAwaiter().GetResult();

            playerStats.Sort((x, y) => { return y.Score.CompareTo(x.Score); });

            List <LeaderBoardEntry> leaderBoardEntries = new List<LeaderBoardEntry>();

            foreach (var playerStat in playerStats)
            {
                if (leaderBoardEntries.Find(item => item.UserName == playerStat.UserName) == null)
                {
                    leaderBoardEntries.Add(new LeaderBoardEntry
                    {
                        Kills = playerStat.Kills,
                        Score = playerStat.Score,
                        UserName = playerStat.UserName,
                        Rank = leaderBoardEntries.FindAll(item => item.Score > playerStat.Score).Count + 1
                    });
                }
            }

            return leaderBoardEntries;
        }
    }
}
