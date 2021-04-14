using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using jobscontractors.Models;

namespace jobscontractors.Repositories
{
    public class WhiteboardsRepository
    {
        private readonly IDbConnection _db;

        public WhiteboardsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Whiteboard> Get()
        {
            string sql = @"
      SELECT
      p.*,
      pr.*
      FROM whiteboards p
      JOIN profiles pr ON p.creatorId = pr.id";
            return _db.Query<Whiteboard, Profile, Whiteboard>(sql, (whiteboard, profile) =>
            {
                whiteboard.Creator = profile;
                return whiteboard;
            }, splitOn: "id");
        }

        internal Whiteboard GetById(int id)
        {
            string sql = @"
      SELECT 
      whit.*,
      pr.*
      FROM whiteboards whit
      JOIN profiles pr ON whit.creatorId = pr.id
      WHERE whit.id = @id;";
            return _db.Query<Whiteboard, Profile, Whiteboard>(sql, (whiteboard, profile) =>
            {
                whiteboard.Creator = profile;
                return whiteboard;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Whiteboard Create(Whiteboard newWhiteboard)
        {
            string sql = @"
      INSERT INTO whiteboards
      (creatorId, name, public)
      VALUES
      (@CreatorId, @Name, @Public);
      SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newWhiteboard);
            newWhiteboard.Id = id;
            return newWhiteboard;
        }

        internal IEnumerable<WhiteboardStickynoteViewModel> GetWhiteboardsByProfileId(string id)
        {
            string sql = @"
      SELECT
      whiteboard.*,
      sn.id AS StickynoteId,
      pr.*
      FROM stickynotes sn
      JOIN whiteboards whiteboard ON sn.whiteboardId = whiteboard.id
      JOIN profiles pr ON pr.id = whiteboard.creatorId
      WHERE sn.memberId = @id;";

            return _db.Query<WhiteboardStickynoteViewModel, Profile, WhiteboardStickynoteViewModel>(sql, (whiteboard, profile) =>
            {
                whiteboard.Creator = profile;
                return whiteboard;
            }, new { id }, splitOn: "id");
        }

        internal Whiteboard Edit(Whiteboard updated)
        {
            string sql = @"
        UPDATE whiteboards
        SET
            name = @Name
        WHERE id = @Id;";
            _db.Execute(sql, updated);
            return updated;
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM whiteboards WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}