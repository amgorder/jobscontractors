using System;
using System.Collections.Generic;
using jobscontractors.Models;
using jobscontractors.Repositories;

namespace jobscontractors.Services
{
    public class JobsService
    {
        private readonly JobsRepository _repo;

        public JobsService(JobsRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<Job> GetAll()
        {
            return _repo.GetAll();
        }

        internal Job GetById(int id)
        {
            var data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            return data;
        }

        internal Job Create(Job newProd)
        {
            return _repo.Create(newProd);
        }

        internal Job Edit(Job updated)
        {
            var original = GetById(updated.Id);
            if (original.CreatorId != updated.CreatorId)
            {
                throw new Exception("Invalid Edit Permissions");
            }
            updated.Description = updated.Description != null ? updated.Description : original.Description;
            updated.Title = updated.Title != null && updated.Title.Length > 2 ? updated.Title : original.Title;
            return _repo.Edit(updated);
        }


        internal string Delete(int id, string userId)
        {
            var original = GetById(id);
            if (original.CreatorId != userId)
            {
                throw new Exception("Invalid Delete Permissions");
            }
            _repo.Delete(id);
            return "delorted";
        }

        internal IEnumerable<WishListJobViewModel> GetJobsByListId(int id)
        {
            return _repo.GetJobsByListId(id);
        }
    }
}