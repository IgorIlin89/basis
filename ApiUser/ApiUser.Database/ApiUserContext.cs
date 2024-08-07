using Microsoft.EntityFrameworkCore;
using ApiUser.Domain;

namespace ApiUser.Database;

public class ApiUserContext : DbContext
{
    public ApiUserContext(DbContextOptions<ApiUserContext> options)
        : base(options)
    {

    }

    public DbSet<User> User { get; set; }
}
