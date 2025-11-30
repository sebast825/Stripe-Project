using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.User
{
    public class UserWithSubscriptionResponseDto
    {
       
            public int Id { get; set; }          
            public string FullName { get; set; }   
            public SubscriptionPlanType? Plan { get; set; }      
            public SubscriptionStatus? Status { get; set; }    
        
    }
}
