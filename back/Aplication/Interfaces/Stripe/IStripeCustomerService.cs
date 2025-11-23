using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Stripe
{
    public interface IStripeCustomerService
    {
        /// <summary>
        /// Creates a new Stripe Customer using the user's identifying data.
        /// </summary>
        /// <returns>
        /// The Stripe Customer ID.
        /// </returns>
        Task<string> CreateCustomerAsync(int userId);
    }
}
