using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yako.Infrastructure.Entities
{
    public class BusinessRating
    {
        public Guid Id { get; set; }

        public Guid BusinessId { get; set; }
        public Business Business { get; set; }

        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public int Rating { get; set; }
    }
}
