using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Core;
using Vega.Core.Models;

namespace Vega.Persistence
{
    public class PhotoRepository :IPhotoRepository
    {
        private readonly VegaDbContext db;
        public PhotoRepository(VegaDbContext db)
        {
            this.db = db;

        }
        public async Task<IEnumerable<Photo>> GetPhotos(int id)
        {
            return await db.Photos
                        .Where(p => p.VehicleId == id)
                        .ToListAsync();
        }
    }
}