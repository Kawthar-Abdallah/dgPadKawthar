using dgPadCms.Models;
using System.Collections.Generic;

namespace PublicWebsite.Models
{
    public class DetailViewModel
    {
        public Post post { get; set; }
        public List<Post> posts { get; set; }
        public List<PostTerm> postTerms { get; set; }
    }
}
