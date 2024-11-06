using Microsoft.EntityFrameworkCore;
using test_binance_api.Data;
using test_binance_api.Models.Base;

namespace test_binance_api.Repository.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly BinanceContext _binanceContext;
        protected readonly DbSet<TEntity> _table;

        public GenericRepository(BinanceContext binanceContext)
        {
            _binanceContext = binanceContext;
            _table = _binanceContext.Set<TEntity>();
        }

        //get
        public List<TEntity> GetAll()
        {
            return _table.AsNoTracking().ToList();
        }
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _table.AsNoTracking().ToListAsync();
        }

        //create

        public void Create(TEntity entity)
        {
            _table.Add(entity);
        }
        public async Task CreateAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
            await _binanceContext.SaveChangesAsync();
        }

        public void CreateRange(IEnumerable<TEntity> entities)
        {
            _table.AddRange(entities);
        }
        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _table.AddRangeAsync(entities);
        }

        //update

        public void Update(TEntity entity)
        {
            _table.Update(entity);
            _binanceContext.SaveChanges();
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _table.UpdateRange(entities);
        }

        //delete

        public void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }

        public bool DeleteById(Guid id)
        {
            var entity = _table.Find(id);
            if (entity == null) return false;
            _table.Remove(entity);
            _binanceContext.SaveChanges();
            return true;
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _table.RemoveRange(entities);
        }

        //find

        public TEntity FindById(Guid id)
        {
            return _table.Find(id);
        }

        public async Task<TEntity> FindByIdAsync(Guid id)
        {
            return await _table.FindAsync(id);
        }


        //save
        public bool Save()
        {
            return _binanceContext.SaveChanges() >= 0;
        }
        public async Task<bool> SaveAsync()
        {
            return await _binanceContext.SaveChangesAsync() > 0;
        }
    }
}
