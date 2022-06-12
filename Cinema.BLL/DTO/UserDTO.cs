using System;

namespace Cinema.BLL.DTO
{
    [Serializable]
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public bool IsActive { get; set; }
        public DateTime UserLastLoginDateTime { get; set; }
    }
}
