﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Response
{
    public  class MovieResponse
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public string Slug { get; set; }
        public required int YearOfRelease { get; init; }
        public required IEnumerable<string> Genres { get; init; } = Enumerable.Empty<string>();
    }
}
