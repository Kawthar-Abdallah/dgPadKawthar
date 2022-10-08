
using dgPadCms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCms.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class TaxonomiesController : Controller
    {
        private readonly dgPadContext context;
        public TaxonomiesController(dgPadContext context)
        {
            this.context = context;
        }

        // GET /admin/taxonomies
        public async Task<IActionResult> Index()
        {
            var taxonomies = await context.Taxonomies.OrderByDescending(p => p.TaxonomyId).ToListAsync();
            return View(taxonomies);
        }

        // GET /admin/taxonomies/create
        public IActionResult Create() => View();

        // POST /admin/taxonomies/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Taxonomy taxonomy)
        {
            if (!ModelState.IsValid)
            {
                return View(taxonomy);
            }

            var name = await context.Taxonomies.FirstOrDefaultAsync(x => x.Name == taxonomy.Name);
            var code = await context.Taxonomies.FirstOrDefaultAsync(x => x.Code == taxonomy.Code);
            if (name != null)
            {
                ModelState.AddModelError("", "This Taxonomy already exist");
                return View(taxonomy);
            }
            if (code != null)
            {
                ModelState.AddModelError("", "This Code already exist");
                return View(taxonomy);
            }

            context.Add(taxonomy);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        // GET /admin/taxonomies/edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var taxonomy = await context.Taxonomies.FindAsync(id);
            return View(taxonomy);
        }

        // POST /admin/taxonomies/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Taxonomy taxonomy)
        {
            if (!ModelState.IsValid)
                return View(taxonomy);

            var name = await context.Taxonomies
                .Where(x => x.TaxonomyId != taxonomy.TaxonomyId)
                .FirstOrDefaultAsync(x => x.Name == taxonomy.Name);
            var code = await context.Taxonomies
                .Where(x => x.TaxonomyId != taxonomy.TaxonomyId)
                .FirstOrDefaultAsync(x => x.Code == taxonomy.Code);
            if (name != null)
            {
                ModelState.AddModelError("", "This Taxonomy already exist");
                return View(taxonomy);
            }
            if (code != null)
            {
                ModelState.AddModelError("", "This Code already exist");
                return View(taxonomy);
            }

            context.Update(taxonomy);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET /admin/taxonomies/delete/id
        public async Task<IActionResult> Delete(int id)
        {
            var taxonomy = await context.Taxonomies.FindAsync(id);
            context.Taxonomies.Remove(taxonomy);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
