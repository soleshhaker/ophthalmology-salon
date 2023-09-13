using DataAccess.Data;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _db;
        public IVisitRepository Visit { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public UnitOfWork(ApplicationDBContext db)
        {
            _db = db;
            Visit = new VisitRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
