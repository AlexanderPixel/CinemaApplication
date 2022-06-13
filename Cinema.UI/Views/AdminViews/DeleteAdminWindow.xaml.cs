﻿using Cinema.UI.ViewModels.AdminViewModels;
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

namespace Cinema.UI.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for DeleteAdminWindow.xaml
    /// </summary>
    public partial class DeleteAdminWindow : Window
    {
        public DeleteAdminWindow()
        {
            InitializeComponent();
            DataContext = new DeleteAdminViewModel();
        }
    }
}
