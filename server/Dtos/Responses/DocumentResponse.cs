using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dtos.Responses
{
    public record DocumentResponse
    (
        int Id,
        string Type,
        DateTime Date,
        string FirstName,
        string LastName,
        string City
    );
}