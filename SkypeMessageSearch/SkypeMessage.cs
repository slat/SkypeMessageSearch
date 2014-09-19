using System;
using System.Net;

namespace SkypeMessageSearch
{
    public class SkypeMessage
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public int Seconds { get; set; }
        public string Body { get; set; }
        public string Conversation { get; set; }

        /// <summary>
        /// Convert Skype Timestamp
        /// http://smalikquadri.blogspot.co.nz/2011/04/cnet-and-timestamp-from-sqlite.html
        /// (But actually starts from 1970, 1, 1)
        /// </summary>
        public string Timestamp
        {
            get
            {
                var ts = TimeSpan.FromSeconds(Seconds);
                var dt1 = new DateTime(1970, 1, 1);
                var dt = new DateTime(ts.Ticks + dt1.Ticks);
                return dt.ToLocalTime().ToString();
            }
        }

        public string Text
        {
            get
            {
                return WebUtility.HtmlDecode(Body);
            }
        }
    }
}
