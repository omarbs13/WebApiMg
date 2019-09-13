namespace MdbApi.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        //  public virtual UserProfile UserProfile { get; set; }

        // public virtual Rol Role { get; set; }
    }
}