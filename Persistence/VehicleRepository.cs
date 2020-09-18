using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Core.Models;
using Vega.Core;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System;
using Vega.Extensions;

namespace Vega.Persistence
{

    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext db;

        public VehicleRepository(VegaDbContext db)
        {
            this.db = db;
        }
        public async Task<Vehicle> GetVehicle(int id ,bool includeRelated =true)
        {
            if (!includeRelated)
                await db.Vehicles.FindAsync(id);

            return await db.Vehicles
                            .Include(m => m.Model).ThenInclude(m => m.Make)
                            .SingleOrDefaultAsync(i => i.Id == id);
        }


        public void Add(Vehicle vehicle){
            db.Vehicles.Add(vehicle);      
            
        }

        public void Remove(Vehicle vehicle)
        {
            db.Vehicles.Remove(vehicle);      
            
        } 
 
        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObject)
        { 
            var result =new QueryResult<Vehicle>();
            var query =  db.Vehicles.Include(m => m.Model)
                            .ThenInclude(m=>m.Make)
                            .Include(f => f.Features)
                            .ThenInclude(v => v.Feature)
                            .AsQueryable();

            query =query.ApplyFiltering(queryObject);

            
            var columnMap = new Dictionary<string , Expression<Func<Vehicle,object>>>(){
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
            };
            query = query.ApplyOrdering<Vehicle>(queryObject ,columnMap);
            
            result.TotalItems= await query.CountAsync();
            query = query.ApplyPaging(queryObject);

            result.Items = await query.ToListAsync();  

            return result;


        }

        
    }
}