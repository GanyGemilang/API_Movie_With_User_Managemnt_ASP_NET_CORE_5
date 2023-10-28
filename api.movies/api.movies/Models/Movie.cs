using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace api.movies.Models
{
    public partial class Movie
    {
        public Movie()
        {
            MovieReviews = new HashSet<MovieReview>();
        }

        public int MovieId { get; set; }
        public string Judul { get; set; }
        public int? TahunRilis { get; set; }

        [JsonIgnore]
        public virtual ICollection<MovieReview> MovieReviews { get; set; }
    }
}
