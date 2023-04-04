using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hb.RedisCacheSample.Cache
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer connection;

        public RedisService()
        {
            connection = ConnectionMultiplexer.Connect("localhost");
        }

        public IDatabase GetDatabase(int id)
        {
            return connection.GetDatabase(id);
        }

    }
}
