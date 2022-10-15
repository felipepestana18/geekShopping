using Microsoft.EntityFrameworkCore;

namespace GeekShooping.IdentityServer.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }
    }
}
