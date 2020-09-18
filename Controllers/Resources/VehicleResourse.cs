using System.Collections;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Vega.Core.Models;

namespace Vega.Controllers.Resources
{
    public class VehicleResourse
    {
         public int Id { get; set; }

        public KeyValuePairResource Model { get; set; }

        

        public bool IsRegistered { get; set; }

        public KeyValuePairResource Make { get; set; }
        public ContactResource Contact { get; set; }
        public DateTime LastUpdate { get; set; }

        public ICollection<KeyValuePairResource> Features { get; set; }

        public VehicleResourse()
        {
            Features =new Collection<KeyValuePairResource>();
        }
    }
}