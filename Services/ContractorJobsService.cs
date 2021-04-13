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

        internal ContractorJob Create(ContractorJob newCJ)
        {
            //TODO if they are creating a ContractorJob, make sure they are the creator of the list
            return _repo.Create(newCJ);

        }

        internal void Delete(int id)
        {
            //NOTE getbyid to validate its valid and you are the creator
            _repo.Delete(id);
        }
    }
}