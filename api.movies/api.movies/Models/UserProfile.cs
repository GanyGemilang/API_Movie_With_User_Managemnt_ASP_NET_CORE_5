using System;
using System.Collections.Generic;

#nullable disable

namespace api.movies.Models
{
    public partial class UserProfile
    {
        public int ProfileId { get; set; }
        public int? UserId { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Email { get; set; }

        public virtual UserAkun User { get; set; }
    }
}
