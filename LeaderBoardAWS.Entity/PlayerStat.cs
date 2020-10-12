using Amazon.DynamoDBv2.DataModel;
using System;

namespace LeaderBoardAWS.Entity
{
    [DynamoDBTable("PlayerStats")]
    public class PlayerStat
    {
        [DynamoDBProperty]
        public string Id { get; set; }
        [DynamoDBHashKey]
        public string UserName { get; set; }
        [DynamoDBGlobalSecondaryIndexHashKey]
        public string Match { get; set; }
        [DynamoDBProperty]
        public int Kills { get; set; }
        [DynamoDBProperty]
        public long Score { get; set; }
        [DynamoDBGlobalSecondaryIndexRangeKey]
        [DynamoDBRangeKey]
        public DateTime TimeStamp { get; set; }
    }
}
