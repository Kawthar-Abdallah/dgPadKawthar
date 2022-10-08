using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using dgPadCms.Models;

namespace dgPadCms.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [Required]
        public string PostTitle { get; set; }

        public int PostTypeId { get; set; }

        public string CreationDate { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public string Summary { get; set; }
        
        public string Image { get; set; }

        public PostType PostType { get; set; }

        public ICollection<PostTerm> PostTerms { get; set; }

        [NotMapped]
        [FileExtention]
        public IFormFile ImageUpload { get; set; }

    }
}
