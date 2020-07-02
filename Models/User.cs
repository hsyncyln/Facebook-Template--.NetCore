using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class User
    {
        public int UserId { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }

        [MaxLength(30)]
        public string Password { get; set; }

        [MaxLength(10)]
        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual Image ProfilePhoto { get; set; }
        public virtual Image BackgroundPhoto { get; set; }

        public List<Image> Images { get; set; }
        public List<Message> Messages { get; set; }
        public List<ShortCut> ShortCuts { get; set; }
        public List<Post> Posts { get; set; }

        public User()
        {
            Images = new List<Image>();
            Messages = new List<Message>();
            ShortCuts = new List<ShortCut>();
            Posts = new List<Post>();
        }

    }
}
