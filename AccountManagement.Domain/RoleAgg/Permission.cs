namespace AccountManagement.Domain.RoleAgg
{
    public class Permission 
    {
        public long Id { get; private set; }
        public int Code { get; private set; }
        public string Name { get; private set; } //---> This Property is ignored in RoleMapping.cs . So this property is removed from database.
        public long RoleId { get; private set; }
        public Role Role { get; private set; }

        public Permission(int code)
        {
            Code = code;
        }

        public Permission(int code, string name , long roleId)
        {
            Code = code;
            Name = name;
            RoleId = roleId;
        }
    }
}