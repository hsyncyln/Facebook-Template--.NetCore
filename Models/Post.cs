using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Picture { get; set; }
        public DateTime CreateDate { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public List<Like> Likes { get; set; }
        public List<Comment> Comments { get; set; }

        public Post()
        {
            Likes = new List<Like>();
            Comments = new List<Comment>();
        }
    }
}
