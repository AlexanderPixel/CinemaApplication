using System;

namespace Cinema.BLL.DTO
{
    public class FilmDTO
    {
        public int FilmId { get; set; }
        public string FilmTitle { get; set; }
        public string FilmDescription { get; set; }
        public string FilmImage { get; set; }
        public int FilmReleaseYear { get; set; }
        public DateTime FilmDisplayStartDate { get; set; }
        public DateTime FilmDisplayEndDate { get; set; }
    }
}
