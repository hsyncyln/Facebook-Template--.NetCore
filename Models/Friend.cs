using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class Friend
    {
        public int Id { get; set; }

        public User FriendUser1 { get; set; } 
        public User FriendUser2 { get; set; } 

        public DateTime CreateDate { get; set; }
    }
}
