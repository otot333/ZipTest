using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipTest.Redis
{
    public class RedisService
    {
        public static IDatabase GetRedieDb()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            return redis.GetDatabase(5);
        }


    }
}
