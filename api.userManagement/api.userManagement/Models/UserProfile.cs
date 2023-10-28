using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace api.userManagement.Models
{
    public partial class UserProfile
    {
        public int ProfileId { get; set; }
        public int? UserId { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public virtual UserAkun User { get; set; }
    }
}
