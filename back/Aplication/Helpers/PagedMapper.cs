using Aplication.Dto;
using Core.Dto;
using Core.Dto.SubscriptionPaymentRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Helpers
{
    public static class PagedMapper
    {
        public static PagedResponseDto<T> ToResponse<T>(int page, int pageSize, int totalItems, IReadOnlyList<T> dataMapped)
        {
            //clear null data
            var cleanData = dataMapped?.Where(x => x != null).ToList() ?? new List<T>();

            return new PagedResponseDto<T>
            {
                Data = cleanData,
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                // Round up in case the division has decimals 
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)

            };
        }
    }
}
