using Microsoft.EntityFrameworkCore;
namespace EscolaTransparente.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        void Commit();
    }
}
