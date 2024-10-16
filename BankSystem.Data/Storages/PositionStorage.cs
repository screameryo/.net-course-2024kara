using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankSystem.Data.Storages
{
    public class PositionStorage : IStorage<Position>
    {
        private readonly BankSystemDbContext _dbContext;

        public PositionStorage(BankSystemDbContext bankSystemDbContext)
        {
            _dbContext = bankSystemDbContext;
        }

        public void Add(Position item)
        {
            try
            {
                _dbContext.Positions.Add(new Position() { Name = item.Name });
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ошибка при добавлении должности.", ex);
            }
        }

        public void Delete(Guid id)
        {
            var positionToUpdate = _dbContext.Positions.Find(id);

            if (positionToUpdate == null)
            {
                throw new KeyNotFoundException("Должность не найдена.");
            }

            try
            {
                _dbContext.Positions.Remove(positionToUpdate);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ошибка при удалении должности.", ex);
            }
        }

        public List<Position> Get(
            Expression<Func<Position, bool>>? filter = null,
            Func<IQueryable<Position>, IOrderedQueryable<Position>>? orderBy = null,
            int page = 1,
            int pageSize = 10)
        {
            var query = _dbContext.Positions.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public void Update(Guid id, Position item)
        {
            var positionToUpdate = _dbContext.Positions.Find(id);

            if (positionToUpdate == null)
            {
                throw new KeyNotFoundException("Должность не найдена.");
            }

            try
            {
                positionToUpdate.Name = item.Name;
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ошибка при обновлении должности.", ex);
            }
        }

        public Position? GetById(Guid id)
        {
            return _dbContext.Positions.Find(id);
        }
    }
}
