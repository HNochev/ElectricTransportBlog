using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ElectricTransportBlog.Infrastructure.Data.Models;

namespace ElectricTransportBlog.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<WebsiteUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Publication> Publications { get; set; }

        public DbSet<PublicationComment> PublicationComments { get; set; }

        public DbSet<WebsiteUser> WebsiteUsers { get; set; }

        public DbSet<TransportNetwork> TransportNetworks { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Line> Lines { get; set; }

        public DbSet<LineType> LineTypes { get; set; }

        public DbSet<Download> Downloads { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<PublicationComment>()
                .HasOne(x => x.Publication)
                .WithMany(x => x.PublicationComments)
                .HasForeignKey(x => x.PublicationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Contact>()
                .HasOne(x => x.TransportNetwork)
                .WithOne(x => x.Contact)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}