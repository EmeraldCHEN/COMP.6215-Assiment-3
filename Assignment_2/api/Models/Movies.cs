using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class Movies
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Picture { get; set; }
        public string Genre { get; set; }
        public double Rating { get; set; }
    }
}
