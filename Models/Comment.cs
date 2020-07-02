using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int UserId { get; set; }
        public virtual User CommentUser { get; set; }
        public DateTime CreateDate { get; set; }

        public List<Like> Likes { get; set; }

        public Comment()
        {
            Likes = new List<Like>();
        }
    }
}
