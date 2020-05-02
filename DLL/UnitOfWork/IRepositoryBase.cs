using DLL.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.UnitOfWork
{
    public interface IRepositoryBase<T> where T:class
    {
        Task CreateAsync(T entry);
        void UpdateAsync(T entry);
        void DeleteAsync(T entry);
        Task<T> GetAAsync(Expression<Func<T,bool>> expression=null);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);

        Task<bool> ApplySaveChangesAsync();
    }

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ApplySaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task CreateAsync(T entry)
        {
            await _context.Set<T>().AddAsync(entry);
        }

        public void DeleteAsync(T entry)
        {
            _context.Set<T>().Remove(entry);
        }

        public async Task<T> GetAAsync(Expression<Func<T, bool>> expression = null)
        {
            if(expression == null)
            {
                return await _context.Set<T>().FirstOrDefaultAsync();
            }
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
        {
            if(expression == null)
            {
                return await _context.Set<T>().ToListAsync();
            }
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public void UpdateAsync(T entry)
        {
            _context.Set<T>().Update(entry);
        }
    }
}
