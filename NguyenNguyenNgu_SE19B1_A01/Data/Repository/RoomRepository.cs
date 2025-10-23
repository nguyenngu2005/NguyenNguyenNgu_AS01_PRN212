using Data.Database;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class RoomRepository : IGenericRepository<RoomInformation>
    {
        private readonly InMemoryDatabase _db = InMemoryDatabase.Instance;

        public IEnumerable<RoomInformation> GetAll() => _db.Rooms;

        public RoomInformation? GetById(Func<RoomInformation, bool> predicate)
            => _db.Rooms.FirstOrDefault(predicate);

        public void Add(RoomInformation entity)
        {
            entity.RoomID = _db.Rooms.Max(r => r.RoomID) + 1;
            _db.Rooms.Add(entity);
        }

        public void Update(RoomInformation entity)
        {
            var existing = _db.Rooms.FirstOrDefault(r => r.RoomID == entity.RoomID);
            if (existing == null) return;
            existing.RoomNumber = entity.RoomNumber;
            existing.RoomDetailDescription = entity.RoomDetailDescription;
            existing.RoomMaxCapacity = entity.RoomMaxCapacity;
            existing.RoomTypeID = entity.RoomTypeID;
            existing.RoomStatus = entity.RoomStatus;
            existing.RoomPricePerDay = entity.RoomPricePerDay;
        }
        public IEnumerable<RoomInformation> Search(string keyword)
        {
            return _db.Rooms.Where(r =>
                r.RoomNumber.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            );
        }
        public void Delete(Func<RoomInformation, bool> predicate)
        {
            var item = _db.Rooms.FirstOrDefault(predicate);
            if (item != null)
                _db.Rooms.Remove(item);
        }

        public IEnumerable<RoomInformation> Search(Expression<Func<RoomInformation, bool>> predicate)
            => _db.Rooms.AsQueryable().Where(predicate);
    }
}
