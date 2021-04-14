using System;
using System.Collections.Generic;
using System.Linq;
using jobscontractors.Models;
using jobscontractors.Repositories;

namespace jobscontractors.Services
{
    public class WhiteboardsService
    {
        private readonly WhiteboardsRepository _repo;

        public WhiteboardsService(WhiteboardsRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<Whiteboard> Get()
        {
            return _repo.Get();
        }

        internal Whiteboard GetById(int id)
        {
            Whiteboard data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid id");
            }
            return data;
        }

        internal Whiteboard Create(Whiteboard newWhiteboard)
        {
            return _repo.Create(newWhiteboard);
        }

        internal Whiteboard Edit(Whiteboard updated)
        {
            Whiteboard original = GetById(updated.Id);
            if (updated.CreatorId != original.CreatorId)
            {
                throw new Exception("You cannot edit this.");
            }
            updated.Name = updated.Name != null ? updated.Name : original.Name;
            return _repo.Edit(updated);
        }

        internal Whiteboard Delete(int id, string userId)
        {
            Whiteboard original = GetById(id);
            if (userId != original.CreatorId)
            {
                throw new Exception("You cannot delete this.");
            }
            _repo.Delete(id);
            return original;
        }

        internal IEnumerable<WhiteboardStickynoteViewModel> GetByProfileId(string id)
        {
            IEnumerable<WhiteboardStickynoteViewModel> parties = _repo.GetWhiteboardsByProfileId(id);
            return parties.ToList().FindAll(p => p.Public);
        }

        internal IEnumerable<WhiteboardStickynoteViewModel> GetByAccountId(string id)
        {
            return _repo.GetWhiteboardsByProfileId(id);
        }
    }
}