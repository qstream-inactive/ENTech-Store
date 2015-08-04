using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Infrastructure.FileStorage
{
    public class LocalFileStorage : FileStorageBase
    {
        public override string Save(string fileName, Stream s, Action<int> updateStatus)
        {
            var path = @"M:\_tempo\Files\" + fileName+DateTime.Now.Ticks;
            var b = new byte[1024 * 256];//256kb

            using (var fs = new FileStream(path, FileMode.CreateNew))
            {
                while (s.Position < s.Length)
                {
                    //write to file / IFileWriter
                    var actualLen = s.Read(b, 0, b.Length);
                    fs.Write(b, 0, actualLen);

                    updateStatus((int)s.Position);
                    //await Task.Delay(1000);
                }

                fs.Flush();
            }

            //save to cdn
            return "cdn:url";
        }
    }
}
