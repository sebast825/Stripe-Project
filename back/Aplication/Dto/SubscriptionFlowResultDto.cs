using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Dto
{
    public enum SubscriptionFlowType
    {
        subscribed,
        checkout
    }
    public class SubscriptionFlowResultDto
    {
        public SubscriptionFlowType FlowType { get; set; }
        public StripeSubscriptionCreatedDto SubscriptionDto{ get; set; }
        public string CheckoutUrl { get; set; }

    }
}
