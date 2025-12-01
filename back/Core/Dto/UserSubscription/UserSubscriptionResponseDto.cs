using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.UserSubscription
{
    public class UserSubscriptionResponseDto
    {
        public int Id { get; set; }
        public string Plan { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CurrentPeriodEnd { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }

        
    }
}
