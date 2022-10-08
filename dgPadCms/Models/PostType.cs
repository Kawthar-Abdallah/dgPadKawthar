using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dgPadCms.Models
{
    public class PostType
    {
        public int PostTypeId { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Code { get; set; }

        public ICollection<TaxonomyPostType> TaxonomyPostTypes { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
