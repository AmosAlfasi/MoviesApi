using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Response
{
    public class ValidationFailureRespone
    {
        public required IEnumerable<ValidationRespone> Errors { get; set; }
    }

    public class ValidationRespone
    {
        public required string PropertyName { get; init; }
        public required string Message { get; init; }
    }
}
