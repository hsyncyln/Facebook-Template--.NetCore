using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class Explore
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Icon { get; set; }

        [MaxLength(200)]
        public string Link { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
