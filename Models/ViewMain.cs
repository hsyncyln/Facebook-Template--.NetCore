
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class ViewMain
    {
        public User MainUser { get; set; }
        public List<User> AllUsers { get; set; }
        public List<User> ContactFriends { get; set; }
        public List<Post> Posts { get; set; }
        public List<Image> Photos { get; set; }
        public bool IsMessageBoxOpen { get; set; }
        public List<Message> Messages { get; set; }
        public User MessageFriend{ get; set; }

        public ViewMain()
        {
            AllUsers = new List<User>();
            ContactFriends = new List<User>();
            Posts = new List<Post>();
            Photos = new List<Image>();
            Messages = new List<Message>();
            IsMessageBoxOpen = false;
        }


    }
}
