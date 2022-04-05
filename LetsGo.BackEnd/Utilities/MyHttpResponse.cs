using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.BackEnd.Utilities
{
    public class NotificationRequestBody
    {
        public string to { get; set; }
        public string priority { get; set; }
        public Notification notification { get; set; }
        public Dictionary<string, string> data { get; set; } = new Dictionary<string, string>();
    }

    public class Notification
    {
        public string body { get; set; }
        public string title { get; set; }
        public string android_channel_id { get; set; }
    }

    public class NotificationResponse
    {
        public int success { get; set; }
        public int failure { get; set; }
        public List<Dictionary<string, dynamic>> results { get; set; } = new List<Dictionary<string, dynamic>>();
    }
}
