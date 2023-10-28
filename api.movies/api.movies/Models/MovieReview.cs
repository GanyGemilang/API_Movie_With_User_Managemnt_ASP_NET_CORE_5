using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace api.movies.Models
{
    public partial class MovieReview
    {
        public int ReviewId { get; set; }
        public int? MovieId { get; set; }
        public int? UserId { get; set; }
        public int? Rating { get; set; }
        public string Komentar { get; set; }
        public DateTime? TanggalReview { get; set; }

        [JsonIgnore]
        public virtual Movie Movie { get; set; }
        [JsonIgnore]
        public virtual UserAkun User { get; set; }
    }
}
