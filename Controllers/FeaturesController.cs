using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Controllers.Resources;
using Vega.Core.Models;
using Vega.Persistence;
using Vega.Core;
using Microsoft.AspNetCore.Authorization;

namespace Vega.Controllers {
    public class FeaturesController : Controller {
        private readonly VegaDbContext db;
        private readonly IMapper mapper;
        public FeaturesController (VegaDbContext db, IMapper mapper) {
            this.mapper = mapper;
            this.db = db;

        }

        
        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures(){
            var features = await db.Features.ToListAsync();
            return  mapper.Map<List<Feature>,List<KeyValuePairResource>>(features);
            
        }
    }
}