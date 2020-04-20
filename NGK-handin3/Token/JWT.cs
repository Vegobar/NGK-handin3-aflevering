using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NGK_handin3.Token
{
    public class JWT
    {
        public string _secretKey { get; set; }

        public JWT()
        {
            var hmac = new HMACSHA256();
            var key = Convert.ToBase64String(hmac.Key);
            _secretKey = key;
        }
    }
}
