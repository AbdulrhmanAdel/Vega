using System.Data.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Core.Models;
using Vega.Core;
using Vega.Controllers.Resources;
using Microsoft.AspNetCore.Authorization;

namespace Vega.Controllers.Resources
{
    [Route("/api/vehicles")]
    public class VehicleController : Controller
    {
        
        private readonly IMapper mapper;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public VehicleController(IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork)
        {
            
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;
            

        }
        [HttpGet]
        public async Task<QueryResultResource<VehicleResourse>> GetVehicles(VehicleQueryResource filterResource){
            var filter = mapper.Map<VehicleQueryResource,VehicleQuery>(filterResource);
            var queryResult = await repository.GetVehicles(filter); 
            return  mapper.Map<QueryResult<Vehicle>,QueryResultResource<VehicleResourse>>(queryResult);   
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id);
            if (vehicle == null)
                return NotFound();

            return Ok(mapper.Map<Vehicle, VehicleResourse>(vehicle));

        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResourse vehicleResourse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var vehicle = mapper.Map<SaveVehicleResourse, Vehicle>(vehicleResourse);
            vehicle.LastUpdate = DateTime.Now;
            repository.Add(vehicle);
            await unitOfWork.CompleteAsync();


            vehicle = await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, SaveVehicleResourse>(vehicle);
            return Ok(result);


        }
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResourse vehicleResourse)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await repository.GetVehicle(id);

            vehicle.LastUpdate = DateTime.Now;


            if (vehicle == null)
                return NotFound();

            mapper.Map<SaveVehicleResourse, Vehicle>(vehicleResourse, vehicle);

            await unitOfWork.CompleteAsync();

            var result = mapper.Map<Vehicle, VehicleResourse>(vehicle);

            return Ok(result);

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteVehicle(int id)
        {

            var vehicle = await repository.GetVehicle(id, includeRelated: false);

            if (vehicle == null)
                return NotFound();

            repository.Remove(vehicle);
            await unitOfWork.CompleteAsync();




            return Ok(id);


        }
    }
}