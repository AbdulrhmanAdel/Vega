using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Controllers.Resources;
using Vega.Core.Models;
using Vega.Core;
using Vega.Persistence;
using System.Linq;

namespace Vega.Controllers
{
    public class MakesController : Controller
    {
        private readonly VegaDbContext db;
        private readonly IMapper mapper;
        public MakesController(VegaDbContext db, IMapper mapper)
        {
            this.mapper = mapper;
            this.db = db;

        }
        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await db.Makes.Include(m => m.Models).ToListAsync();

            return mapper.Map<List<Make>,List<MakeResource>>(makes);
        }

        [HttpGet("/api/charts")]
        public Chart GetChart(){
            
            
            var makes =  db.Makes.Select(n => n.Name).ToList();
            
          
            var num1 = db.Vehicles.Where(m => m.Model.Make.Name == "make1").Count();
            var num2 = db.Vehicles.Where(m => m.Model.Make.Name == "make2").Count();
            var num3 = db.Vehicles.Where(m => m.Model.Make.Name == "make3").Count();
                
                
            var makeVehicles = new int[]{};   
           

            return new Chart{
                Makes=makes,
                MakeVehicles=new int[]{num1,num2,num3}
            };
        }
    }
}