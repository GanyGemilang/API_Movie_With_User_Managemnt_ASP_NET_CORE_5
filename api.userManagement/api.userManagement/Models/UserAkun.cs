using System;
using System.Collections.Generic;

#nullable disable

namespace api.userManagement.Models
{
    public partial class UserAkun
    {
        public UserAkun()
        {
            UserProfiles = new HashSet<UserProfile>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
