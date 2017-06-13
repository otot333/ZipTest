using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZipTest.Model;

namespace ZipTest.Redis
{
    public class RedisService
    {
        private static IDatabase db = ConnectionMultiplexer.Connect("localhost").GetDatabase(5);
        private static string key = "TESTLIST";
        public static IDatabase GetRedieDb()
        {
            return db;
        }

        public static async Task<long> LPush(byte[] bytes)
        {
            var task = await db.ListLeftPushAsync(key, bytes);
            return task;
        }

        public static async Task<byte[]> RPop()
        {
            var task = await db.ListRightPopAsync(key);
            return task;
        }



    }
}
