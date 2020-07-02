using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class Like
    {
        public int Id { get; set; }

        public int LikeUserId { get; set; }
        public virtual User LikeUser { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
