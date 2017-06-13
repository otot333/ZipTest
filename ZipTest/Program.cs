using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZipTest.Json;
using ZipTest.Model;
using ZipTest.Redis;

namespace ZipTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonpack = new JsonPack();
            var msgpack = new MsgPack.MsgPack();
            var pkg301 = PackageHelper.Get301Pkg();
            var pkg211 = PackageHelper.Get211Pkg();
            var sw = new Stopwatch();

            msgpack.MsgSerializeWithLZ4WithRedis(pkg211);
            jsonpack.JsonSerializeWithGzipWithRedis(pkg211);
            #region WithRedis
            jsonpack.JsonSerializeWithGzip(pkg211);
            msgpack.MsgSerializeWithLZ4(pkg211);
            Console.WriteLine("===========SBC 211 For loop 1500====================");

            Console.WriteLine("--------LZ4 + Msg --------");
            sw.Start();
            for (int i = 0; i < 1500; i++)
            {
                msgpack.MsgSerializeWithLZ4(pkg211);
            }
            sw.Stop();
            Console.WriteLine($"Time : {sw.Elapsed}");
            sw.Reset();

            Console.WriteLine();
            Console.WriteLine("--------GZip + Json --------");
            sw.Start();
            for (int i = 0; i < 1500; i++)
            {
                jsonpack.JsonSerializeWithGzip(pkg211);
            }
            sw.Stop();
            Console.WriteLine($"Time : {sw.Elapsed}");
            sw.Reset();

            Console.WriteLine();

            Console.WriteLine("===========SBC 301 For loop 1500====================");
            Console.WriteLine("--------LZ4 + Msg --------");
            sw.Start();
            for (int i = 0; i < 1500; i++)
            {
                msgpack.MsgSerializeWithLZ4(pkg301);
            }
            sw.Stop();
            Console.WriteLine($"Time : {sw.Elapsed}");
            sw.Reset();

            Console.WriteLine();
            Console.WriteLine("--------GZip + Json --------");
            sw.Start();
            for (int i = 0; i < 1500; i++)
            {
                jsonpack.JsonSerializeWithGzip(pkg301);
            }
            sw.Stop();
            Console.WriteLine($"Time : {sw.Elapsed}");
            #endregion 

            #region NoRedis
            //jsonpack.JsonSerializeWithGzip(pkg211);
            //msgpack.MsgSerializeWithLZ4(pkg211);
            //Console.WriteLine("===========SBC 211 For loop 1500====================");

            //Console.WriteLine("--------LZ4 + Msg --------");
            //sw.Start();
            //for (int i = 0; i < 1500; i++)
            //{
            //    msgpack.MsgSerializeWithLZ4(pkg211);
            //}
            //sw.Stop();
            //Console.WriteLine($"Time : {sw.Elapsed}");
            //sw.Reset();

            //Console.WriteLine();
            //Console.WriteLine("--------GZip + Json --------");
            //sw.Start();
            //for (int i = 0; i < 1500; i++)
            //{
            //    jsonpack.JsonSerializeWithGzip(pkg211);
            //}
            //sw.Stop();
            //Console.WriteLine($"Time : {sw.Elapsed}");
            //sw.Reset();

            //Console.WriteLine();

            //Console.WriteLine("===========SBC 301 For loop 1500====================");
            //Console.WriteLine("--------LZ4 + Msg --------");
            //sw.Start();
            //for (int i = 0; i < 1500; i++)
            //{
            //    msgpack.MsgSerializeWithLZ4(pkg301);
            //}
            //sw.Stop();
            //Console.WriteLine($"Time : {sw.Elapsed}");
            //sw.Reset();

            //Console.WriteLine();
            //Console.WriteLine("--------GZip + Json --------");
            //sw.Start();
            //for (int i = 0; i < 1500; i++)
            //{
            //    jsonpack.JsonSerializeWithGzip(pkg301);
            //}
            //sw.Stop();
            //Console.WriteLine($"Time : {sw.Elapsed}");
            #endregion 

            Console.ReadLine();
        }
    }
}
