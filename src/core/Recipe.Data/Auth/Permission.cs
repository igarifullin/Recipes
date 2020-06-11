using System.Collections.Generic;

namespace Recipe.Data.Auth
{
    public class Permission
    {
        public Permission()
        {
            UserRoles = new List<UserRole>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}