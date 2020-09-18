using System.Threading.Tasks;
using Vega.Core;

namespace Vega.Persistence

{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext db;
        public UnitOfWork(VegaDbContext db)
        {
            this.db = db;

        }
        public async Task CompleteAsync()
        {
           await db.SaveChangesAsync();
        }
    }
}