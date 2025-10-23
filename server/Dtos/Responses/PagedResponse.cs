using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dtos.Responses
{
    public record PagedResponse<T>(
        IEnumerable<T> Items,
        int TotalCount,
        int Page,
        int PageSize,
        int TotalPages
    );
}