using System;
using System.Collections.Generic;
using System.Text;

namespace WealthManager.Core.Model
{
    public class Token
    {
        public string id { get; set; }
        public string auth_token { get; set; }
        public DateTime expired_in { get; set; }
    }
}
