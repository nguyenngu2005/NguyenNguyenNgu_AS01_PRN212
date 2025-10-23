using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<RoomInformation> GetAll();
        RoomInformation? GetById(int id);
        void Add(RoomInformation room);
        void Update(RoomInformation room);
        void Delete(int id);
        IEnumerable<RoomInformation> Search(string keyword);
    }
}

