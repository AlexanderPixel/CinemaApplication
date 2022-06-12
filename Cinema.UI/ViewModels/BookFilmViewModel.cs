using Cinema.BLL.DTO;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Cinema.UI.ViewModels
{
    public class BookFilmViewModel
    {
        public FilmDTO Film { get; set; }
        public ObservableCollection<DateTime> FilmDates { get; set; }
        public BookFilmViewModel(FilmDTO film)
        {
            Film = film;

            Task.Run(() =>
            {
                // some logic to get film dates
            });
        }
    }
}
