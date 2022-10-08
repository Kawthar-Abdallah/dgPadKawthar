using dgPadCms.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace dgPadCms.Models
{
    public class dgPadContext : IdentityDbContext<AppUser>
    {
        public dgPadContext(DbContextOptions<dgPadContext> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<Term>()
                .HasOne(t => t.Taxonomy)
                .WithMany(te => te.Terms);

            builder.Entity<Post>()
               .HasOne(p => p.PostType)
               .WithMany(pt => pt.Posts);

            builder.Entity<PostTerm>()
                .HasKey(pt => new { pt.PostId, pt.TermId });

            builder.Entity<PostTerm>()
                .HasOne(pt => pt.Term)
                .WithMany(t => t.PostTerms)
                .HasForeignKey(pt => pt.TermId);
            
            builder.Entity<PostTerm>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTerms)
                .HasForeignKey(pt => pt.PostId);

            builder.Entity<TaxonomyPostType>()
                .HasKey(tp => new { tp.TaxonomyId, tp.PostTypeId });

            builder.Entity<TaxonomyPostType>()
                .HasOne(tp => tp.Taxonomy)
                .WithMany(t => t.TaxonomyPostTypes)
                .HasForeignKey(tp => tp.TaxonomyId);

            builder.Entity<TaxonomyPostType>()
                .HasOne(tp => tp.PostType)
                .WithMany(p => p.TaxonomyPostTypes)
                .HasForeignKey(pt => pt.PostTypeId);
        }

        public DbSet<Taxonomy> Taxonomies { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<PostType> PostTypes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<TaxonomyPostType> TaxonomyPostTypes { get; set; }
        public DbSet<PostTerm> PostTerms { get; set; }

    }
}
