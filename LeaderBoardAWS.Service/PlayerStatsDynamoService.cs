using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using LeaderBoardAWS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaderBoardAWS.Service
{
    public class PlayerStatsDynamoService : IPlayerStatsDynamoService
    {
        private AmazonDynamoDBClient _client;
        public PlayerStatsDynamoService(DynamoDBClientService dynamoDBClientService)
        {
            _client = dynamoDBClientService.CreateClient();
        }
        public PlayerStat Create(PlayerStat playerStat)
        {
            DynamoDBContext context = new DynamoDBContext(_client);

            playerStat.Id = Guid.NewGuid().ToString();
            //Change to lower case to maintain consistancy while searching
            playerStat.Match = playerStat.Match.ToLower(); 

            context.SaveAsync(playerStat).GetAwaiter().GetResult();

            return playerStat;
        }

        public IQueryable<PlayerStat> Get()
        {
            throw new NotImplementedException();
        }

        public IList<PlayerStat> GetPlayerStats(string id)
        {

            DynamoDBContext context = new DynamoDBContext(_client);

            QueryFilter filter = new QueryFilter();
            filter.AddCondition("UserName", QueryOperator.Equal, id);

            QueryOperationConfig queryOperationConfig = new QueryOperationConfig { Filter = filter };

            var result = context.FromQueryAsync<PlayerStat>(queryOperationConfig).GetNextSetAsync().GetAwaiter().GetResult();
            return result;
        }

        public IList<PlayerStat> GetPlayerStats(string id, int duration)
        {
            DynamoDBContext context = new DynamoDBContext(_client);

            QueryFilter filter = new QueryFilter();
            filter.AddCondition("UserName", QueryOperator.Equal, id);
            filter.AddCondition("TimeStamp", QueryOperator.GreaterThanOrEqual, DateTime.Now.AddHours(-duration));

            QueryOperationConfig queryOperationConfig = new QueryOperationConfig { Filter = filter };

            var result = context.FromQueryAsync<PlayerStat>(queryOperationConfig).GetNextSetAsync().GetAwaiter().GetResult();

            return result;
        }
    }
}
