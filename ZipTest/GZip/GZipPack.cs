using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZipTest.Model;

namespace ZipTest.GZip
{
    public class GZipPack
    {
        //public void GZipSerializeAndDeserialize(Package pkg)
        //{
        //    Console.WriteLine("-----------Msg-----------");
        //    using (var ms = new MemoryStream())
        //    {
        //        using (var gz = new GZipStream(ms, CompressionMode.Compress))
        //        {
                    
        //            Stopwatch sw = new Stopwatch();
        //            sw.Start();

        //            var bytes = MessagePackSerializer.Serialize(pkg);
        //            sw.Stop();
        //        }
                
        //    };
           

        //    Console.WriteLine($"Serialize {bytes.Length} bytes, Time:{sw.ElapsedMilliseconds}");

        //}


    }
}
