using System;

namespace Cinema.BLL.DTO
{
    public class ActorDTO
    {
        public int ActorId { get; set; }
        public string ActorFirstName { get; set; }
        public string ActorLastName { get; set; }
        public DateTime? ActorBirthDate { get; set; }
    }
}
