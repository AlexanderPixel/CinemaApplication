using Cinema.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cinema.UI.ViewModels
{
    public class AdminViewModel
    {
        #region commands
        public ICommand AddAdminCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MessageBox.Show("AddAdminCommand");
                });
            }
        }
        public ICommand DeleteAdminCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MessageBox.Show("DeleteAdminCommand");
                });
            }
        }
        public ICommand EditAdminCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MessageBox.Show("EditAdminCommand");
                });
            }
        }
        public ICommand AddFilmCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MessageBox.Show("AddFilmCommand");
                });
            }
        }
        public ICommand DeleteFilmCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MessageBox.Show("DeleteFilmCommand");
                });
            }
        }
        public ICommand EditFilmCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MessageBox.Show("EditFilmCommand");
                });
            }
        }
        public ICommand AddMovieCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MessageBox.Show("AddMovieCommand");
                });
            }
        }
        public ICommand DeleteMovieCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MessageBox.Show("DeleteMovieCommand");
                });
            }
        }
        public ICommand EditMovieCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MessageBox.Show("EditMovieCommand");
                });
            }
        }
        #endregion
    }
}
