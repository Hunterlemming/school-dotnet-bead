using netcore_gyakorlas.Repository;
using Microsoft.EntityFrameworkCore;
using netcore_gyakorlas.Models;
using System;
using System.Threading.Tasks;

namespace netcore_gyakorlas.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : AbstractEntity;

        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : AbstractEntity;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        DbContext Context();
    }
}
