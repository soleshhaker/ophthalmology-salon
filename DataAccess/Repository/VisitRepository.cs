using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Ophthalmology.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccess.Repository
{
    public class VisitRepository : Repository<Visit>, IVisitRepository
    {
        private ApplicationDBContext _db;
        public VisitRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Visit obj)
        {
            _db.Visits.Update(obj);
        }
    }
}
