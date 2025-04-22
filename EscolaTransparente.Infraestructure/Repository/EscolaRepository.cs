using EscolaTransparente.Domain.Interfaces.Repositories;
using EscolaTransparente.Infraestructure.Context;
using EscolaTransparente.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EscolaTransparente.Infraestructure.Repository
{
    public class EscolaRepository : IEscolaRepository
    {
        private readonly AppDbContext _context;

        public EscolaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EscolaModel>> GetAllAsync() => await _context.Set<EscolaModel>().ToListAsync();
        public async Task<EscolaModel?> GetByIdAsync(int id) => await _context.Set<EscolaModel>().FirstOrDefaultAsync(e => e.EscolaId == id);
        public async Task AddAsync(EscolaModel entity) => await _context.Set<EscolaModel>().AddAsync(entity);
        public void Delete(int id) => _context.Set<EscolaModel>().Remove(new EscolaModel() { EscolaId = id });
        public void Update(EscolaModel entity) => _context.Set<EscolaModel>().Update(entity);

    }
}
