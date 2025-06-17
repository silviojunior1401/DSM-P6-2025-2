using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardioCheck.Models
{
    public static class SessaoLogin
    {
        public static string Email { get; set; }
        public static string Token { get; set; }
        public static string UrlApi { get; set; } = "http://10.0.2.2:3000/v1";
    }
}
