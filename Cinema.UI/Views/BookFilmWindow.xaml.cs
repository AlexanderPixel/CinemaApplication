﻿using Cinema.BLL.DTO;
using Cinema.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cinema.UI.Views
{
    /// <summary>
    /// Interaction logic for BookFilmWindow.xaml
    /// </summary>
    public partial class BookFilmWindow : Window
    {
        public BookFilmWindow(FilmDTO film)
        {
            InitializeComponent();
            DataContext = new BookFilmViewModel(film);
        }
    }
}
