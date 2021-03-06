﻿using Desktop.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for SellMedicines.xaml
    /// </summary>
    public partial class SellMedicines : Page
    {
        private readonly SellMedicinesViewModel ViewModel; 

        public SellMedicines()
        {
            InitializeComponent();

            this.ViewModel = new SellMedicinesViewModel();
            this.DataContext = this.ViewModel;
        }
    }
}
