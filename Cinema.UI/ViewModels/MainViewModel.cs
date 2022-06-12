using Cinema.BLL.DTO;
using Cinema.BLL.Managers;
using Cinema.UI.Infrastructure;
using Cinema.UI.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cinema.UI.ViewModels
{
    public class MainViewModel : BaseNotifyPropertyChanged
    {
        #region private fields

        private bool userDataIsReadOnly = true;
        private ObservableCollection<FilmDTO> whatsOnFilms;
        private ObservableCollection<FilmDTO> whatsComingFilms;
        private UserDTO currentUser;
        private FilmDTO selectedFilm;

        #endregion

        #region public properties

        public ObservableCollection<FilmDTO> WhatsOnFilms
        {
            get { return whatsOnFilms; }
            set 
            { 
                whatsOnFilms = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<FilmDTO> WhatsComingFilms
        {
            get { return whatsComingFilms; }
            set 
            { 
                whatsComingFilms = value; 
                NotifyPropertyChanged(); 
            }
        }

        public UserDTO CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                NotifyPropertyChanged();
            }
        }

        public FilmDTO SelectedFilm
        {
            get => selectedFilm;
            set
            {
                selectedFilm = value;
                NotifyPropertyChanged();
            }
        }

        public bool UserDataIsReadOnly
        {
            get { return userDataIsReadOnly; }
            set
            {
                userDataIsReadOnly = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region commands

        public ICommand EditCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    UserDataIsReadOnly = false;
                    MessageBox.Show("You can now edit your personal info.");
                });
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MessageBox.Show("Information saved.");
                });
            }
        }

        public ICommand OpenFilmCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedFilm.FilmTitle != null)
                    {
                        var openFilmWindow = new OpenFilmWindow(SelectedFilm);
                        openFilmWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("You need to select film to open.");
                    }
                });
            }
        }

        #endregion

        #region methods

        public MainViewModel(UserDTO user)
        {
            LoadData();

            CurrentUser = user;

            SelectedFilm = new FilmDTO();
        }

        private void LoadData()
        {
            Task.Run(() =>
            {
                var imdbManager = new ImdbManager();
                var whatOn = imdbManager.GetTop250Films(0, 25);
                var whatsComing = imdbManager.GetTop250Films(50, 25);

                WhatsOnFilms = new ObservableCollection<FilmDTO>(whatOn);
                WhatsComingFilms = new ObservableCollection<FilmDTO>(whatsComing);
            });
        }

        #endregion
    }
}
