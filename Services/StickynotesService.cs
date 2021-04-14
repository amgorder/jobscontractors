using System;
using System.Collections.Generic;
using jobscontractors.Models;
using jobscontractors.Repositories;

namespace jobscontractors.Services
{
    public class StickynotesService
    {
        private readonly StickynotesRepository _repo;
        private readonly WhiteboardsRepository _whiteboardsrepo;

        public StickynotesService(StickynotesRepository repo, WhiteboardsRepository whiteboardsrepo)
        {
            _repo = repo;
            _whiteboardsrepo = whiteboardsrepo;
        }

        internal string Create(Stickynote pm)
        {
            Whiteboard whiteboard = _whiteboardsrepo.GetById(pm.WhiteboardId);
            if (whiteboard == null)
            {
                throw new Exception("Not a valid whiteboard");
            }
            if (whiteboard.CreatorId != pm.CreatorId)
            {
                throw new Exception("You are not the owner of this whiteboard");
            }
            _repo.Create(pm);
            return "Created";
        }

        internal void Delete(int id, string userId)
        {
            Stickynote member = _repo.GetById(id);
            if (member == null)
            {
                throw new Exception("Invalid member");
            }
            if (member.CreatorId != userId)
            {
                throw new Exception("Invalid User");
            }
            _repo.Delete(id);
        }

        internal IEnumerable<WhiteboardStickynoteViewModel> GetByWhiteboardId(int id)
        {
            return _repo.GetByWhitboardId(id);
        }
    }
}