using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XamWebApiClient.Models
{
    public class AdddBook
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public byte[] ImageArray { get; set; }

    }
}
