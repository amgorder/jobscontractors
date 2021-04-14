using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using jobscontractors.Models;

namespace jobscontractors.Repositories
{
    public class StickynotesRepository
    {
        private readonly IDbConnection _db;

        public StickynotesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal void Create(Stickynote pm)
        {
            string sql = @"
        INSERT INTO stickynotes
        (memberId, whiteboardId, creatorId)
        VALUES
        (@MemberId, @WhiteboardId, @CreatorId);";
            _db.Execute(sql, pm);
        }

        internal Stickynote GetById(int id)
        {
            string sql = "SELECT * FROM stickynotes WHERE id = @id;";
            return _db.QueryFirstOrDefault<Stickynote>(sql, new { id });
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM stickynotes WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }

        internal IEnumerable<WhiteboardStickynoteViewModel> GetByWhitboardId(int id)
        {
            string sql = @"
      SELECT
      whiteboard.*,
      sn.id AS StickynoteId,
      pr.*
      FROM stickynotes sn
      JOIN whiteboards whiteboard ON sn.whiteboardId = whiteboard.id
      JOIN profiles pr ON pr.id = whiteboard.creatorId
      WHERE sn.whiteboardId = @id;";

            return _db.Query<WhiteboardStickynoteViewModel, Profile, WhiteboardStickynoteViewModel>(sql, (whiteboard, profile) =>
            {
                whiteboard.Creator = profile;
                return whiteboard;
            }, new { id }, splitOn: "id");
        }
    }
}