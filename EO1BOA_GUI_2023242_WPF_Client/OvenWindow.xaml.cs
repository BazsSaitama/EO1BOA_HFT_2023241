﻿using EO1BOA_HFT_2023241.Models;
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
using EO1BOA_GUI_2023242_WPF_Client.ViewModels;

namespace EO1BOA_GUI_2023242_WPF_Client
{
    /// <summary>
    /// Interaction logic for OvenWindow.xaml
    /// </summary>
    public partial class OvenWindow : Window
    {
        public OvenWindow(RestCollection<Oven> ovens,RestCollection<Bread> breads)
        {
            DataContext = new OvenViewModel(ovens,breads);
            InitializeComponent();
        }
    }
}
