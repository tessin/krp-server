
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Krp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Krp.Server
{
    public static class Settings
    {
        [FunctionName("Settings")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req, TraceWriter log)
        {
            //log.Info("");
            //string name = req.Query["name"];

            var userTable = GetTable("Users");

            TableContinuationToken token = null;

            var entities = userTable.ExecuteQuerySegmentedAsync(TableQuery<UserEntity>(), token).ToList();

            await SetupTable(log);

            return new JsonResult(new SettingsResponse());
        }

        /*
        private static async Task SetupTable(TraceWriter log)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=krpserver;AccountKey=azDx5Nx+GU48T8tsPlgXZOWZkFMx0pIZn0+0jZYywk+9ojd+B4X5k0rsgT3l4sZTAYZCzviPgN03dAOq98pVfg==");

                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                CloudTable table = tableClient.GetTableReference("Users");

                await table.CreateIfNotExistsAsync();

                var user = UserEntity.New("jonas@tessin.se");
                user.Name = "Jonas Björkman";
                user.SlackUser = "jonas";

                TableOperation insertOperation = TableOperation.Insert(user);

                await table.ExecuteAsync(insertOperation);
            }
            catch (Exception e)
            {
                log.Error("SetupTable failed.", e);
                throw;
            }
        }
        */

        private static CloudTable GetTable(string tableName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=krpserver;AccountKey=azDx5Nx+GU48T8tsPlgXZOWZkFMx0pIZn0+0jZYywk+9ojd+B4X5k0rsgT3l4sZTAYZCzviPgN03dAOq98pVfg==");

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(tableName);

            return table;
        }

    }


    public class SettingsResponse
    {

        public List<UserEntity> Users { get; set; }

    }

}
