using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace dgPadCms.Models.ViewModels
{
    public class CreatePostViewModel
    {
        public Taxonomy taxonomy { get; set; }
        public SelectList terms { get; set; }
    }
}
