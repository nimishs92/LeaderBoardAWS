using Amazon;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderBoardAWS.Service
{
    public class DynamoDBClientService
    {
        private static AmazonDynamoDBConfig clientConfig;
        private static AmazonDynamoDBClient client;

        private static IConfiguration _configuration;

        public DynamoDBClientService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public AmazonDynamoDBClient CreateClient()
        {
            if (client == null)
            {
                clientConfig = new AmazonDynamoDBConfig();
                // Set the endpoint URL
                if (_configuration["UseLocal"] == "true")
                {
                    clientConfig.ServiceURL = "http://localhost:8000";
                }
                clientConfig.RegionEndpoint = RegionEndpoint.APSouth1;
                client = new AmazonDynamoDBClient(clientConfig);
            }
            return client;
        }
    }
}
