using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class Message
    {
        public int Id { get; set; } 
        public string Content { get; set; }
        public DateTime SendDate { get; set; }

        public int FriendId { get; set; }
        public User FriendUser { get; set; }
    }
}
