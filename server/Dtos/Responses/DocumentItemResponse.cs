using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dtos.Responses
{
    public record DocumentItemResponse
    (
        int Id,
        int DocumentId,
        int Ordinal,
        string Product,
        int Quantity,
        float Price,
        int TaxRate
    );
}