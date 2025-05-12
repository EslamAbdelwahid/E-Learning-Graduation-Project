using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Repository
{
    public static class SpecificationsEvaluator<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        // Query Generation
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecifications<TEntity,TKey> spec )
        {
            var query = inputQuery;

            // Generate the queries

            if(spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }
            if(spec.OrderBy is not null )
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if(spec.OrderByDesc is not null )
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            // add includes if exist
            query = spec.Includes.Aggregate(query,(currQuery,includeExpression) => currQuery.Include(includeExpression));

            return query;
        }
    }
}
