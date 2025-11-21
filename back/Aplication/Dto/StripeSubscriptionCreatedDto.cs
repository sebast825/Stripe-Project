using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Dto
{
    public class StripeSubscriptionCreatedDto
    {
        public string SubscriptionId { get; set; }
        public string CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
