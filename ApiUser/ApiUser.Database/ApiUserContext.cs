using ApiUser.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiUser.Database;

public class ApiUserContext : DbContext
{
    public ApiUserContext(DbContextOptions<ApiUserContext> options)
        : base(options)
    {

    }

    public DbSet<User> User { get; set; }
}
