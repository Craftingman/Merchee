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

        public BaseCompanyEntityService(CompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<TEntity>>> FindAll(
            Guid companyId,
            int pageNumber, int pageSize,
            Expression<Func<TEntity, object>> orderBy,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = _dbContext.Set<TEntity>()
                .Where(e => e.CompanyId == companyId);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.OrderBy(orderBy)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
        }

        public async Task<Result<TEntity>> Get(Guid companyId, Guid id)
        {
            var entity = await _dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.CompanyId == companyId && e.Id == id);

            return entity is not null
                ? Result.Ok(entity)
                : Result.Fail(new NotFoundError());
        }

        public async Task<Result<Guid>> Add(TEntity entity)
        {
            if (entity == null)
                return Result.Fail(new BadRequestError());

            entity.Id = Guid.NewGuid();

            _dbContext.Add(entity);
            return await _dbContext.SaveChangesAsync() >= 1
                ? Result.Ok(entity.Id)
                : Result.Fail("Failed to add entity to DB");
        }

        public async Task<Result> Update(Guid companyId, Guid id, TEntity entity)
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
