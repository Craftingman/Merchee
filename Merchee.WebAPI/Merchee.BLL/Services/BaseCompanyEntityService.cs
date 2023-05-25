using AutoMapper;
using FluentResults;
using Merchee.BLL.Errors;
using Merchee.DataAccess;
using Merchee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Merchee.BLL.Services
{
    public class BaseCompanyEntityService<TEntity>
        where TEntity : BaseCompanyEntity
    {
        protected CompanyDbContext _dbContext;
        protected IMapper _mapper;

        public BaseCompanyEntityService(CompanyDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public virtual async Task<Result<IEnumerable<TEntity>>> FindAllAsync(
            Guid companyId,
            int pageNumber, int pageSize,
            Expression<Func<TEntity, object>> orderBy,
            Expression<Func<TEntity, bool>> predicate = null,
            IEnumerable<Expression<Func<TEntity, object>>> includes = null)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            query = query.Where(e => e.CompanyId == companyId);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.OrderByDescending(orderBy)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
        }

        public virtual async Task<Result<TEntity>> GetAsync(Guid companyId, Guid id, IEnumerable<Expression<Func<TEntity, object>>> includes = null)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var entity = await query
                .FirstOrDefaultAsync(e => e.CompanyId == companyId && e.Id == id);

            return entity is not null
                ? Result.Ok(entity)
                : Result.Fail(new NotFoundError());
        }

        public virtual async Task<Result<Guid>> AddAsync(Guid companyId, TEntity entity)
        {
            if (entity == null)
                return Result.Fail(new BadRequestError());

            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            entity.CompanyId = companyId;
            entity.Active = true;

            _dbContext.Add(entity);
            return await _dbContext.SaveChangesAsync() >= 1
                ? Result.Ok(entity.Id)
                : Result.Fail("Failed to add entity to DB");
        }

        public virtual async Task<Result> UpdateAsync(Guid companyId, Guid id, TEntity entity)
        {
            var originalEntity = await _dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.CompanyId == companyId && e.Id == id);
            if (originalEntity == null)
                return Result.Fail(new NotFoundError());

            _mapper.Map(entity, originalEntity);

            await _dbContext.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
