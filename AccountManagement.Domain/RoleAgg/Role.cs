using _0_Framework.Domain;
using AccountManagement.Domain.AccountAgg;
using System.Collections.Generic;

namespace AccountManagement.Domain.RoleAgg
{

    public class Role : EntityBase
    {
        public string Name { get; private set; }
        public List<Account> Accounts { get; private set; }
        public List<Permission> Permissions { get; private set; }

        protected Role()
        {

        } //-----> We add this because EFCore gives this error :
                   //No suitable constructor was found for entity type 'Role'.
                   //The following constructors had parameters that could not be bound to
                   //properties of the entity type:
                   //cannot bind 'permissions' in 'Role(string name, List<Permission> permissions)'.
        public Role(string name , List<Permission> permissions)
        {
            Name = name;
            Accounts = new List<Account>();
            Permissions = permissions;
        }

        public void Edit(string name , List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }
    }
}