using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

public interface IDbContextFactory<T> where T : DbContext
{
    T CreateDbContext();
}