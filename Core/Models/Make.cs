using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vega.Core.Models
{
    public class Make
    {
        
    
    
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; }


        public Make()
        {
            Models=new Collection<Model>();
            
        }    

        
    }
}