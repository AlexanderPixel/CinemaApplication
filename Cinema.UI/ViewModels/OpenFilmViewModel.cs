using Cinema.BLL.DTO;
using Cinema.UI.Infrastructure;
using Cinema.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cinema.UI.ViewModels
{
    public class OpenFilmViewModel
    {
        public FilmDTO CurrentFilm { get; set; }

        public OpenFilmViewModel(FilmDTO film)
        {
            CurrentFilm = film;
        }

        public ICommand BookFilmCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var bookFilmWindow = new BookFilmWindow(CurrentFilm);
                    bookFilmWindow.Show();
                });
            }
        }
    }
}
