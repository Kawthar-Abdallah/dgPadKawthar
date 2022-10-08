
using dgPadCms.Models;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicWebsite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicWebsite.Controllers
{
    public class PostController : Controller
    {
        private readonly dgPadContext context;
        public PostController(dgPadContext context)
        {
            this.context = context;

        }

        public async Task<IActionResult> Index(int id)
        {
            var posts = await context.Posts.Include(x=>x.PostType).Where(x => x.PostTypeId == id).ToListAsync();
            var postType = await context.PostTypes.FindAsync(id);
            ViewBag.PostType = postType.Title;
            return View(posts);
        }
  
        public async Task<IActionResult> Detail(int id)
        {
            var pt = await context.PostTerms.Include(x => x.Term).Where(x=>x.PostId==id).ToListAsync();
            var p=await context.Posts.Include(x => x.PostTerms).ThenInclude(x=>x.Term).ToListAsync();
        

            var post = await context.Posts.FindAsync(id);
            DetailViewModel detailViewModel = new DetailViewModel();
            detailViewModel.post = post;
            detailViewModel.posts = p;
            detailViewModel.postTerms = pt;
            return View(detailViewModel);
        }
    }
}
