using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Repository.Contract;
using E_Learning.GraduationProject.Core.Specifications;
using E_Learning.GraduationProject.Repository.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Repository.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly AppDbContext _context;

        public GenericRepository(
            AppDbContext context
            )
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>?> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec)
        {
            return  await SpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>() , spec).ToListAsync();

        }

        public async Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec).FirstOrDefaultAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        
    }
}
