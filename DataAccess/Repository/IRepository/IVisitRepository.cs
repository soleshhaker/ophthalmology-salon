using Ophthalmology.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccess.Repository.IRepository
{
    public interface IVisitRepository : IRepository<Visit>
    {
        void Update(Visit obj);
    }
}
