using System.Linq;
using AutoMapper;
using Vega.Controllers.Resources;
using Vega.Core.Models;

namespace Vega.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            // Domin to Resource
            CreateMap<Photo,PhotoResource>();
            CreateMap(typeof(QueryResult<>),typeof(QueryResultResource<>));
            CreateMap<Make,MakeResource>();
            CreateMap<Make,KeyValuePairResource>();

            
            CreateMap<Model,KeyValuePairResource>();
             
            CreateMap<Feature,KeyValuePairResource>();

            CreateMap<Vehicle,SaveVehicleResourse>()
                .ForMember(vr => vr.Contact,opt => opt.MapFrom(v =>new ContactResource {Name=v.ContactName,Phone=v.ContactPhone,Email=v.ContactEmail}))
                .ForMember(vr => vr.Features,opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            CreateMap<Vehicle ,VehicleResourse>()
                        .ForMember( vr => vr.Model ,opt => opt.MapFrom(v => new KeyValuePairResource{Id=v.Model.Id,Name=v.Model.Name}))
                        .ForMember(vr => vr.Make ,opt => opt.MapFrom(vr => new KeyValuePairResource{Id=vr.Model.Make.Id,Name=vr.Model.Make.Name}))
                        .ForMember(vr => vr.Contact,opt => opt.MapFrom(v => new ContactResource {Name =v.ContactName ,Phone =v.ContactPhone,Email=v.ContactName}))
                        .ForMember(vr => vr.Features,opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource {Id =vf.FeatureId,Name=vf.Feature.Name})));  

            CreateMap<VehicleQueryResource,VehicleQuery>();      
        

            // Resource to Domin
            CreateMap<SaveVehicleResourse, Vehicle>()
              .ForMember(v => v.Id, opt => opt.Ignore())
              .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
              .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
              .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
              .ForMember(v => v.Features, opt => opt.Ignore())
              .AfterMap((vr, v) => {
                // Remove unselected features
                var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();
                foreach (var f in removedFeatures)
                  v.Features.Remove(f);

                // Add new features
                var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id }).ToList();   
                foreach (var f in addedFeatures)
                    v.Features.Add(f);
            });
            CreateMap<KeyValuePairResource,Feature>();
                
        }
    }
}