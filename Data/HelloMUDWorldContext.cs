using HelloMUDWorld.Data.Game;
using HelloMUDWorld.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelloMUDWorld.Data;

public class HelloMUDWorldContext : IdentityDbContext<HelloMUDWorldUser>
{
    public DbSet<Character>? Characters { get; set; }

    public HelloMUDWorldContext(DbContextOptions<HelloMUDWorldContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
