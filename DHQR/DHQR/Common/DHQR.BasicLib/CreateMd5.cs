using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace DHQR.BasicLib
{
    public class CreateMd5
    {
        public static string Md5String(string str)
        {
            byte[] buffer = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(str));
            var builder = new StringBuilder();
            foreach (var num in buffer)
            {
                builder.AppendFormat("{0:X2}", num);
            }
            return builder.ToString();
        }


    }
}
