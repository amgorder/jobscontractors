using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using jobscontractors.Models;

namespace jobscontractors.Repositories
{
    public class ContractorsRepository
    {
        private readonly IDbConnection _db;
        public ContractorsRepository(IDbConnection db)
        {
            _db = db;
        }


        internal IEnumerable<Contractor> GetAll()
        {
            string sql = @"
      SELECT 
      cont.*,
      prof.*
      FROM contractors cont
      JOIN profiles prof ON cont.creatorId = prof.id";
            return _db.Query<Contractor, Profile, Contractor>(sql, (contractor, profile) =>
            {
                contractor.Creator = profile;
                return contractor;
            }, splitOn: "id");
        }
        //      1 SELECT 
        //      4 cont.*,
        //      6 prof.*
        //      2 FROM contractors cont
        //      5 JOIN profiles prof ON cont.creatorId = prof.id
        //      3 WHERE cont.id = @id";
        internal Contractor GetById(int id)
        {
            string sql = @" 
      SELECT 
      cont.*,
      prof.*
      FROM contractors cont
      JOIN profiles prof ON cont.creatorId = prof.id
      WHERE cont.id = @id";
            return _db.Query<Contractor, Profile, Contractor>(sql, (contractor, profile) =>
            {
                contractor.Creator = profile;
                return contractor;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Contractor Create(Contractor newWList)
        {
            string sql = @"
      INSERT INTO contractors 
      (title, creatorId) 
      VALUES 
      (@Title, @creatorId);
      SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newWList);
            newWList.Id = id;
            return newWList;
        }

        internal Contractor Edit(Contractor updated)
        {
            string sql = @"
        UPDATE contractors
        SET
         title = @Title
        WHERE id = @Id;";
            _db.Execute(sql, updated);
            return updated;
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM contractors WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}