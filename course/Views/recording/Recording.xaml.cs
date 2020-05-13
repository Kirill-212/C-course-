﻿using course.ViewModels;
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

namespace course
{
    /// <summary>
    /// Логика взаимодействия для Recording.xaml
    /// </summary>
    public partial class Recording : Window
    {
        public Recording()
        {
            DataContext = new ApplicationViewModel();
            InitializeComponent();
        }
    }
}
