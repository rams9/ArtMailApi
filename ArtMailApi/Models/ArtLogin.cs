using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtMailApi.Models
{
    public class ArtLogin
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    public class ArtLoginRes
    {
        public bool IsError { get; set; }
        public string Error { get; set; }
        public int Aid { get; set; }
        public string UserName { get; set; }
    }
}