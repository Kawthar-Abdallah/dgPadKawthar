using dgPadCms.Models;
using PublicWebsite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PublicWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly dgPadContext context;
        public HomeController(dgPadContext context)
        {
            this.context = context;

        }


        public IActionResult Index()
        {

            //var todaysPosts = context.posts.Include(x=>x.postType).Where(x => x.CreationDate.Day == DateTime.Today.Day).ToList();
            var recentPosts = context.Posts.Include(x => x.PostType).OrderByDescending(x => x.CreationDate).ToList();
            var recentSixPosts = new List<Post>();
            for (int i = 0; i < 2; i++)
            {
                recentSixPosts.Add(recentPosts[i]);
            }


            return View(recentSixPosts);
        }
    }
}
