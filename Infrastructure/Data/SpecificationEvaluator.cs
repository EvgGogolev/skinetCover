using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity :BaseEntity
    {
        /// <summary>
        /// способ динамического составления запросов на основе спецификаций,
        /// </summary>
        /// <param name="inputQuery">запрашиваемый источник сущностей</param>
        /// <param name="spec">спецификация: определяет критерии и включает в себя запросы к сущностям.</param>
        /// <returns>запросов на основе спецификации</returns>
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity>  inputQuery, ISpecification<TEntity>spec)  
        {
            var query = inputQuery;
            if(spec.Criteria != null) 
            {
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query,(current,include) => current.Include(include));
            return query;
        }
    }
}