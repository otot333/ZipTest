using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MessagePack;
using ZipTest.Model;
using System.Diagnostics;

namespace ZipTest.MsgPack
{
    public class MsgPack
    {
        public void MsgSerializeAndDeserialize(Package pkg)
        {
            Console.WriteLine("-----------Msg-----------");
            #region old
            //byte[] bytes;
            //using (var ms = new MemoryStream())
            //{

            //    Stopwatch sw = new Stopwatch();
            //    sw.Start();
            //    MessagePackSerializer.Serialize(ms, pkg);
            //    sw.Stop();                
            //    bytes = ms.ToArray();
            //    Console.WriteLine($"Serialize {bytes.Length} bytes, Time:{sw.ElapsedMilliseconds}");      

            //};

            //MsgDeserialize(bytes);
            #endregion
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var bytes = MessagePackSerializer.Serialize(pkg);
            sw.Stop();
            Console.WriteLine($"Serialize {bytes.Length} bytes, Time:{sw.ElapsedMilliseconds}");
            MsgDeserialize(bytes);
        }

        private void MsgDeserialize(byte[] btyes)
        {
            using (var ms = new MemoryStream(btyes))
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var pkg = MessagePackSerializer.Deserialize<Package>(ms);                
                sw.Stop();
                Console.WriteLine($"gid:{pkg.gid}");
                Console.WriteLine($"Deserialize Time:{sw.ElapsedMilliseconds}");
            };
            Console.WriteLine();
        }

        public void MsgSerializeWithLZ4(Package pkg)
        {
            //Console.WriteLine("-----------MsgLZ4-----------");
           
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            var bytes = LZ4MessagePackSerializer.Serialize(pkg);
            //sw.Stop();
           /* Console.WriteLine($"LZ4 + Msg {bytes.Length} bytes, Time:{sw.Elapsed}")*/;
            MsgDeserializeWithLZ4(bytes);
            
        }
        

        public void MsgDeserializeWithLZ4(byte[] bytes)
        {
            //using (var ms = new MemoryStream(bytes))
            //{
                //Stopwatch sw = new Stopwatch();
                //sw.Start();
                var pkg = LZ4MessagePackSerializer.Deserialize<Package>(bytes);
                //sw.Stop();
                //Console.WriteLine($"gid:{pkg.gid}");
                //Console.WriteLine($"Deserialize Time:{sw.Elapsed}");
            //};

            //Console.WriteLine();
            
        }
    }
}
