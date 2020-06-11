using System.Collections.Generic;

namespace Recipe.Data.Auth
{
    public class UserRole
    {
        public UserRole()
        {
            Users = new List<User>();
            Permissions = new List<Permission>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}