﻿using course.Models;
using course.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace course
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window     
    {
        public static ModelDATABASE modelDATABASE = new ModelDATABASE();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
            
            
            //foreach(User i in modelDATABASE.Users)
            //{
            //    MessageBox.Show($"{i.e_maeil}   {i.Логин}    {i.Пароль}     {i.Роль}");
            //}
        }



        private void OpenWinReg(object sender, RoutedEventArgs e)
        {
            Recording recording = new Recording();
            recording.ShowDialog();
        }
    }
}
