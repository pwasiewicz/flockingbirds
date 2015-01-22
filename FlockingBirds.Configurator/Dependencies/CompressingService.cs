
namespace FlockingBirds.Configurator.Dependencies
{
    using System;
    using System.IO;
    using System.Text;
    using System.IO.Compression;


    public class CompressingService : ICompressingService
    {
        public string Compress(string text)
        {
            var buffer = Encoding.Default.GetBytes(text);
            var ms = new MemoryStream();

            using (var zip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }

            ms.Position = 0;

            var compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            var gzBuffer = new byte[compressed.Length + 4];

            Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);

            return Convert.ToBase64String(gzBuffer);
        }

        public string Decompress(string text)
        {
            var gzBuffer = Convert.FromBase64String(text);

            using (var ms = new MemoryStream())
            {
                var msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

                var buffer = new byte[msgLength];

                ms.Position = 0;
                using (var zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }

                return Encoding.Default.GetString(buffer, 0, buffer.Length);
            }
        }
    }
}
