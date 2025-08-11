using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yako.Infrastructure.Entities
{
    public class Business
    {
        public Guid Id { get; set; }
        public string Title { get; set; }    
        public string Description { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }

        public string MapUrl { get; set; }
        public string ImageUrl { get; set; }


        public double? AverageRating { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
        
    }
}
