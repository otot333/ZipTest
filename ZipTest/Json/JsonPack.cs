using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZipTest.Model;
using ZipTest.Redis;

namespace ZipTest.Json
{
    public class JsonPack
    {
        private JsonSerializer jsonSerializer = JsonSerializer.Create();
        public void JsonSerializeAndDeserialize(Package pkg)
        {
            Console.WriteLine("-----------Json-----------");

            Stopwatch sw = new Stopwatch();
            byte[] bytes;

            sw.Start();
            using (var ms = new MemoryStream())
            {
                var jTextwriter = new JsonTextWriter(new StreamWriter(ms));
                jsonSerializer.Serialize(jTextwriter, pkg);
                jTextwriter.Flush();
                bytes = ms.ToArray();
                sw.Stop();
                Console.WriteLine($"Serialize {bytes.Length} bytes, Time:{sw.ElapsedMilliseconds}");
                JsonDeserialize(bytes);
            };
        }

        private void JsonDeserialize(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                Stopwatch sw = new Stopwatch();
                var jTextReader = new JsonTextReader(new StreamReader(ms));
                sw.Start();
                var pkg = jsonSerializer.Deserialize<Package>(jTextReader);
                sw.Stop();
                Console.WriteLine($"gid:{pkg.gid}");
                Console.WriteLine($"Deserialize Time:{sw.ElapsedMilliseconds}");
            };

            Console.WriteLine();

        }

        public void JsonSerializeWithGzip(Package pkg)
        {
            //Console.WriteLine("-----------JsonWithGzip-----------");
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            var str = JsonConvert.SerializeObject(pkg);
            var bytes = Encoding.UTF8.GetBytes(str);
            byte[] Debytes;
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                Debytes = mso.ToArray();
            }
            //sw.Stop();
            //Console.WriteLine($"GZip + Json {Debytes.Length} bytes, Time:{sw.Elapsed}");
            JsonDeserializeWithGzip(Debytes);


            //using (var ms = new MemoryStream())
            //{
            //    sw.Start();
            //    var jTextwriter = new JsonTextWriter(new StreamWriter(ms));
            //    jsonSerializer.Serialize(jTextwriter, pkg);
            //    jTextwriter.Flush();
            //    bytes = ms.ToArray();

            //    using (var d = new MemoryStream(bytes))
            //    using (var ts = new MemoryStream()) {
            //        using (var gz = new GZipStream(ts, CompressionMode.Compress))
            //        {
            //            d.CopyTo(gz);
            //            sw.Stop();
            //            bytes2 = ts.ToArray();
            //        }
            //    }
            //};

        }

        public void JsonDeserializeWithGzip(byte[] bytes)
        {
            Stopwatch sw = new Stopwatch();
            Package pkg;
            //using (var ms = new MemoryStream(bytes))
            //using (var ts = new MemoryStream()) {
            //using (var gz = new GZipStream(ms, CompressionMode.Decompress))
            //{
            //    sw.Start();
            //    gz.CopyTo(ts);
            //    var str = Encoding.UTF8.GetString(ts.ToArray());
            //    pkg = JsonConvert.DeserializeObject<Package>(str);
            //    sw.Stop();
            //}
            //Console.WriteLine(pkg.gid);
            //sw.Start();
            var str = "";
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                str = Encoding.UTF8.GetString(mso.ToArray());
            }
            pkg = JsonConvert.DeserializeObject<Package>(str);
            //sw.Stop();
            //Console.WriteLine($"gid:{pkg.gid}");
            //Console.WriteLine($"Deserialize Time:{sw.Elapsed}");



        }

        public async void JsonSerializeWithGzipWithRedis(Package pkg)
        {
            var str = JsonConvert.SerializeObject(pkg);
            var bytes = Encoding.UTF8.GetBytes(str);
            byte[] Debytes;
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                Debytes = mso.ToArray();
            }
            var task = await RedisService.LPush(Debytes);

            JsonDeserializeWithGzipWithRedis();

        }

        public async void JsonDeserializeWithGzipWithRedis()
        {
            var task = await RedisService.RPop();
            Package pkg;
            var str = "";
            using (var msi = new MemoryStream(task))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                str = Encoding.UTF8.GetString(mso.ToArray());
            }
            pkg = JsonConvert.DeserializeObject<Package>(str);

        }
    }
}
