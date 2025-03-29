using CCP.Repositori.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>()
            where TEntity : BaseEntity;

        Task<int> Complete();
        int CompleteV2();
        Task<int> SaveChangesAsync();
    }
}
