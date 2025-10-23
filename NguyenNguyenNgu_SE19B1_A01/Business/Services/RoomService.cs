using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Data.Entities;
using Data.Repository;


namespace Business.Services
{
    public class RoomService : IRoomService
    {
        private readonly RoomRepository _repo = new();

        public IEnumerable<RoomInformation> GetAll() => _repo.GetAll();

        public RoomInformation? GetById(int id)
            => _repo.GetById(r => r.RoomID == id);

        public void Add(RoomInformation room) => _repo.Add(room);

        public void Update(RoomInformation room) => _repo.Update(room);

        public void Delete(int id) => _repo.Delete(r => r.RoomID == id);
        public IEnumerable<RoomInformation> Search(string keyword)
        {
            keyword = keyword.ToLower();
            return _repo.GetAll().Where(r =>
                r.RoomNumber.ToLower().Contains(keyword) ||
                r.RoomDetailDescription.ToLower().Contains(keyword));
        }
    }
}

