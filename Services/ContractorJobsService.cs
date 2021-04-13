using System;
using System.Collections.Generic;
using jobscontractors.Models;
using jobscontractors.Repositories;

namespace jobscontractors.Services
{
    public class ContractorJobsService
    {
        private readonly ContractorJobsRepository _repo;

        public ContractorJobsService(ContractorJobsRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<ContractorJob> GetAll()
        {
            return _repo.GetAll();
        }

        internal ContractorJob GetById(int id)
        {
            var data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            return data;
        }

        internal ContractorJob Create(ContractorJob newProd)
        {
            return _repo.Create(newProd);
        }

        internal ContractorJob Edit(ContractorJob updated)
        {
            var original = GetById(updated.Id);
            if (original.CreatorId != updated.CreatorId)
            {
                throw new Exception("Invalid Edit Permissions");
            }
            updated.Title = updated.Title != null ? updated.Title : original.Title;
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
    }
}