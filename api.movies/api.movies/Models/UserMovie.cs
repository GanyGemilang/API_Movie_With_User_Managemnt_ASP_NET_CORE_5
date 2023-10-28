using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace api.movies.Models
{
    public partial class UserMovie
    {
        public int? UserMovieID { get; set; }
        public int? UserId { get; set; }
        public int? MovieId { get; set; }

        [JsonIgnore]
        public virtual Movie Movie { get; set; }
        [JsonIgnore]
        public virtual UserAkun User { get; set; }
    }
}
