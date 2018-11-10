using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;

namespace Krp.Server.Models
{
    public class UserEntity : TableEntity
    {

        public UserEntity() { }

        private UserEntity(string partitionKey, string rowKey)
        {
            RowKey = rowKey;
            PartitionKey = partitionKey;
        }

        public static UserEntity New(string email)
        {
            var user = new UserEntity("User", Guid.NewGuid().ToString())
            {
                Email = email
            };
            return user;
        }

        public string Id
        {
            get => RowKey; 
            set => RowKey = value;
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string SlackUser { get; set; }

    }
}
