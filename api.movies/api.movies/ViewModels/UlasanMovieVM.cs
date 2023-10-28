using System;

namespace api.movies.ViewModels
{
    public class UlasanMovieVM
    {
        public int? MovieId { get; set; }
        public int? UserId { get; set; }
        public int? Rating { get; set; }
        public string Komentar { get; set; }
        public DateTime? TanggalReview { get; set; }
    }
}
