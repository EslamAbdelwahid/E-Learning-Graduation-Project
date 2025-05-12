using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Repository.Contract;
using E_Learning.GraduationProject.Repository.Data.Context;
using E_Learning.GraduationProject.Repository.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(
            AppDbContext context
            )
        {
            _context = context;
            _repositories = new Hashtable();
        }

        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type = typeof(TEntity).Name;
            
            //if not exist create one and hash it 
            if (! _repositories.ContainsKey(type))
            {
                var repo = new GenericRepository<TEntity, TKey>(_context);
                _repositories.Add(type, repo);
            }

            return (IGenericRepository<TEntity,TKey>) _repositories[type];
        }
    }
}
