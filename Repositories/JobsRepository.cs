using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using jobscontractors.Models;

namespace jobscontractors.Repositories
{
    public class JobsRepository
    {
        private readonly IDbConnection _db;
        public JobsRepository(IDbConnection db)
        {
            _db = db;
        }


        internal IEnumerable<Job> GetAll()
        {
            string sql = @"
      SELECT 
      prod.*,
      prof.*
      FROM jobs prod
      JOIN profiles prof ON prod.creatorId = prof.id";
            return _db.Query<Job, Profile, Job>(sql, (job, profile) =>
            {
                job.Creator = profile;
                return job;
            }, splitOn: "id");
        }

        internal Job GetById(int id)
        {
            string sql = @" 
      SELECT 
      j.*,
      prof.*
      FROM jobs j
      JOIN profiles prof ON j.creatorId = prof.id
      WHERE j.id = @id";
            return _db.Query<Job, Profile, Job>(sql, (job, profile) =>
            {
                job.Creator = profile;
                return job;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Job Create(Job newProd)
        {
            string sql = @"
      INSERT INTO jobs 
      (title, description, price, creatorId) 
      VALUES 
      (@Title, @Description, @Price, @creatorId);
      SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newProd);
            newProd.Id = id;
            return newProd;
        }

        internal Job Edit(Job updated)
        {
            string sql = @"
        UPDATE jobs
        SET
         title = @Title,
         description = @Description,
         price = @Price
        WHERE id = @Id;";
            _db.Execute(sql, updated);
            return updated;
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM jobs WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }


        internal IEnumerable<WishListJobViewModel> GetJobsByListId(int id)
        {
            string sql = @"SELECT 
      j.*,
      wlp.id AS WishListJobId
      FROM wishlistjobs wlp
      JOIN jobs j ON p.id = wlp.jobId
      WHERE wishlistId = @id;";
            return _db.Query<WishListJobViewModel>(sql, new { id });
        }
    }
}