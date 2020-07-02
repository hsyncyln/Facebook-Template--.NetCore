using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        
        [MaxLength(50)]
        public string ImageLink { get; set; }



    }
}
