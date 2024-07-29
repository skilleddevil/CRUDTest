using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserDetail> userDetails { get; set; }
}

public class ApplicationUser : IdentityUser
{
}

public class UserDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
     public string Email { get; set; }
      public string Address { get; set; }
    public string Number { get; set; }
}
