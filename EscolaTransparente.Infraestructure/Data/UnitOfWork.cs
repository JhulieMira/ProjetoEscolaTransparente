using EscolaTransparente.Domain.Entities;
using EscolaTransparente.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EscolaTransparente.Infraestructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _currentTransaction;

        public DbContext Context => _context;

        public DbSet<EscolaModel> Escolas => _context.Set<EscolaModel>();
        public DbSet<AvaliacaoModel> Avaliacoes => _context.Set<AvaliacaoModel>();
        public DbSet<CaracteristicaModel> Caracteristicas => _context.Set<CaracteristicaModel>();
        public DbSet<CaracteristicasEscolaModel> CaracteristicasEscola => _context.Set<CaracteristicasEscolaModel>();
        public DbSet<ContatoModel> Contatos => _context.Set<ContatoModel>();
        public DbSet<EnderecoModel> Enderecos => _context.Set<EnderecoModel>();
        public DbSet<RespostaAvaliacaoModel> RespostasAvaliacao => _context.Set<RespostaAvaliacaoModel>();

        public DbSet<CaracteristicasEscolaModel> CaracteristicasEscolas => _context.Set<CaracteristicasEscolaModel>();

        public DbSet<Usuario> Usuario => _context.Set<Usuario>();

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null)
                return _currentTransaction;

            _currentTransaction = await _context.Database.BeginTransactionAsync();
            return _currentTransaction;
        }

        public IDbContextTransaction BeginTransaction()
        {
            if (_currentTransaction != null)
            {
                return _currentTransaction;
            }

            _currentTransaction = _context.Database.BeginTransaction();
            return _currentTransaction;
        }

        public async Task CommitAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException("Transação não corresponde à transação atual");

            try
            {
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync(transaction);
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public void Commit(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException("Transação não corresponde à transação atual");

            try
            {
                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                Rollback(transaction);
                throw;
            }
            finally
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }

        public async Task RollbackAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));

            try
            {
                await transaction.RollbackAsync();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public void Rollback(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));

            try
            {
                transaction.Rollback();
            }
            finally
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }
    }
}
