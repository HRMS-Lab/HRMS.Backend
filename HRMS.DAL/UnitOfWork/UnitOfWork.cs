
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using HRMS.DAL.Data;

namespace HRMS.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;
        private bool _disposed = false;
        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public DbContext Context => context;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                _disposed = true;
            }
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
