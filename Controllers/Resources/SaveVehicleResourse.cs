using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Vega.Core.Models;

namespace Vega.Controllers.Resources
{
    public class SaveVehicleResourse
    {
        public int Id { get; set; }

         public int ModelId { get; set; }

        public bool IsRegistered { get; set; }

        public ContactResource Contact { get; set; }

        

        public ICollection<int> Features { get; set; }

        public SaveVehicleResourse()
        {
            Features =new Collection<int>();
        }

    }
}