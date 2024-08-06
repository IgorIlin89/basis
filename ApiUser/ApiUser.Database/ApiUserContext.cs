using Microsoft.EntityFrameworkCore;
using ApiUser.Domain;

namespace ApiUser.Database;

internal class ApiUserContext : DbContext
{
    public ApiUserContext(DbContextOptions<ApiUserContext> options)
        : base(options)
    {

    }

    public DbSet<User> User { get; set; }
}
