using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace WLInstall.ViewModels
{
    public static class SHA1Calculator
    {

        public static string CalSHAbyChunk(string filePath)
        {
            const int chunkSize = 10 * 1024 * 1024; // read the file by chunks of 10MB
            try
            {
                using (var file = File.OpenRead(filePath))
                {
                    byte[] hashBytes;
                    int bytesRead;
                    var buffer = new byte[chunkSize];
                    bytesRead = file.Read(buffer, 0, buffer.Length);
                    //Console.WriteLine(Encoding.Default.GetString(buffer));

                    var sha1 = SHA1.Create();
                    hashBytes = sha1.ComputeHash(buffer);

                    return HexStringFromBytes(hashBytes);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return e.ToString();
            }
            
        }

        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
