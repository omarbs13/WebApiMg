namespace MdbApi.Application.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class UserModelAdd
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Rol Rol { get; set; }
    }

    public class Rol
    {
        public string RolName { get; set; }
    }

}