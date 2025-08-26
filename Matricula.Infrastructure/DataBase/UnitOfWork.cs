using Matricula.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Infrastructure.DataBase
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext context)
        {
            _db = context;
        }
        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            return new GenericRepository<T>(_db);
        }
        public async Task BeginTransaction()
        {
            await _db.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            try
            {
                _db.SaveChanges();
                await _db.Database.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                await _db.Database.RollbackTransactionAsync();
            }
        }

        public async Task Rollback()
        {
            await _db.Database.RollbackTransactionAsync();
        }
    }
}
