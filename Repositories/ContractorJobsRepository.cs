using System.Data;
using Dapper;
using jobscontractors.Models;

namespace jobscontractors.Repositories
{
    public class ContractorJobsRepository
    {
        private readonly IDbConnection _db;
        public ContractorJobsRepository(IDbConnection db)
        {
            _db = db;
        }
        internal ContractorJob Create(ContractorJob newCJ)
        {
            string sql = @"
      INSERT INTO contractorjobs 
      (jobId, contractorId, creatorId) 
      VALUES 
      (@JobId, @ContractorId, @CreatorId);
      SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newCJ);
            newCJ.Id = id;
            return newCJ;
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM contractorjobs WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });

        }
    }
}