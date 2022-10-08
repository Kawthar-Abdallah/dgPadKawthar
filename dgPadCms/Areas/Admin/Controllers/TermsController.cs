
using dgPadCms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCms.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class TermsController : Controller
    {
        private readonly dgPadContext context;
        public TermsController(dgPadContext context)
        {
            this.context = context;
        }

        // GET /admin/terms
        public async Task<IActionResult> Index()
        {

            var terms = await context.Terms.OrderByDescending(t => t.TermId).Include(t => t.Taxonomy).ToListAsync();

            return View(terms);
        }

        // GET /admin/terms/create
        public IActionResult Create()
        {
            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Name), "TaxonomyId", "Name");
            return View();
        }

        // POST /admin/terms/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Term term)
        {
            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Name), "TaxonomyId", "Name");

            if (!ModelState.IsValid)
            {
                return View(term);
            }

            var name = await context.Taxonomies.FirstOrDefaultAsync(x => x.Name == term.Name);
            var code = await context.Taxonomies.FirstOrDefaultAsync(x => x.Code == term.Code);
            if (name != null)
            {
                ModelState.AddModelError("", "This Term already exist");
                return View(term);
            }
            if (code != null)
            {
                ModelState.AddModelError("", "This Code already exist");
                return View(term);
            }


            context.Add(term);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        // GET /admin/terms/edit/id
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Name), "TaxonomyId", "Name");

            var term = await context.Terms.FindAsync(id);
            return View(term);
        }

        // POST /admin/terms/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Term term)
        {
            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Name), "TaxonomyId", "Name");

            if (!ModelState.IsValid)
                return View(term);

            var name = await context.Terms.FirstOrDefaultAsync(x => x.Name == term.Name);
            var code = await context.Terms.FirstOrDefaultAsync(x => x.Code == term.Code);
            if (name != null)
            {
                ModelState.AddModelError("", "This Term already exist");
                return View(term);
            }
            if (code != null)
            {
                ModelState.AddModelError("", "This Code already exist");
                return View(term);
            }

            context.Update(term);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET /admin/terms/delete/id
        public async Task<IActionResult> Delete(int id)
        {
            var term = await context.Terms.FindAsync(id);
            context.Terms.Remove(term);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
    