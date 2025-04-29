using EscolaTransparente.Domain.Interfaces.Repositories;
using EscolaTransparente.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EscolaTransparente.Infraestructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }

        public UnitOfWork(AppDbContext dbContext)
        {
            Context = dbContext;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }   

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
