using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary5
{
    public static class SessionExtensions
    {
        // ✅ Fixed GetString method to use TryGetValue()
        public static string GetString(this ISession session, string key)
        {
            session.TryGetValue(key, out byte[] data);
            return data == null ? null : Encoding.UTF8.GetString(data);
        }

        // ✅ SetString method remains the same
        public static void SetString(this ISession session, string key, string value)
        {
            session.Set(key, Encoding.UTF8.GetBytes(value));
        }
    }
}