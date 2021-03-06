﻿using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Controls;
using Desktop.ViewModels;
using Desktop.Views.Pages;
using Desktop.Services;
using System;
using System.Windows.Navigation;

namespace Desktop.Views.Windows
{
    public partial class MainWindow : Window
    {
        private Medicines _medicines;

        private Recipes _recipes;

        private SellMedicines _sellMedicines;

        private CreateRecipe _createRecipe;

        private HospitalAdminApprovals _hospitalAdminApprovals;

        private PharmacyAdminApprovals _pharmacyAdminApprovals;

        private MinistryWorkerApprovals _ministryWorkerApprovals;

        private readonly NavigateService _navigationService;

        private object _contentHolder;

        private readonly MainWindowViewModel _mainWindowVM;

        public MainWindowViewModel Vm => this._mainWindowVM;

        public MainWindow()
        {
            // initializing components
            InitializeComponent();

            // setting loading content
            this._contentHolder = this.Content;
            this.Content = new LoadPage();

            // setting fields
            this._mainWindowVM = new MainWindowViewModel(this);
            this.DataContext = this._mainWindowVM;
            this._navigationService = new NavigateService(this.frame);

        }

        private void Navigate(object sender, NavigationEventArgs e)
        {
            var sellMedicinesPageType = typeof(SellMedicines);

            var currentPageType = this.frame.Content.GetType();

            if (currentPageType == sellMedicinesPageType)
                return;

            var qrDecoder = ((App)App.Current).QrDecoderService;

            var pageType = e.Content.GetType();                        

            if (pageType == sellMedicinesPageType)
                qrDecoder?.Start();
            else
                qrDecoder?.Stop();
        }

        private async void Medicines_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this._navigationService.Navigate(ref this._medicines);
            var loadMedicinesService = new LoadMedicinesService(this._medicines.MedicinesViewModel);
            await loadMedicinesService.Load();
        }

        private void SellMedicinesButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._sellMedicines);
        }

        private async void CreateRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._createRecipe);
            var loadMedicinesService = new LoadMedicinesService(this._createRecipe.ViewModel);
            await loadMedicinesService.Load();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.frame.NavigationService.Navigate(new Recipes());
        }

        private async void Main_Loaded(object sender, RoutedEventArgs e)
        {
            await this._mainWindowVM.LoadService.Execute();

            var mapPage = ((App)App.Current).MapPage;

            this.frame.Navigate(mapPage);

            this.Content = this._contentHolder;
        }

        private async void HospitalAdminApprovalsButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._hospitalAdminApprovals);
            var loadHospitalApprovalsService = new LoadHospitalAdminApprovalsService(this._hospitalAdminApprovals.ViewModel);
            await loadHospitalApprovalsService.Load();
        }

        private async void RecipesButton_Click(object sender, RoutedEventArgs e)
        {
            this._navigationService.Navigate(ref this._recipes);
            var loadRecipesService = new LoadRecipesService(this._recipes.ViewModel);
            await loadRecipesService.Load();
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var mapPage = ((App)App.Current).MapPage;

            this.frame.Navigate(mapPage);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var menu = this.profiles.Items;

            var app = (App)App.Current;

            var manager = app.ProfilesMenuManager;

            foreach(var item in menu)
            {
                var menuItem = (MenuItem)item;

                menuItem.Click -= manager.ChangeProfileEventHandler;              
            }           

            app.ProfilesMenuManager = null;

            app.QrDecoderService?.Stop();

            base.OnClosing(e);
        }

        private async void MyApprovalsButton_Click(object sender, RoutedEventArgs e)
        {
            if(User.Default.CurrentProfile == "hospital_director")
            {
                this._navigationService.Navigate(ref this._hospitalAdminApprovals);

                var vm = (HospitalAdminApprovalViewModel)this._hospitalAdminApprovals.DataContext;

                var service = new LoadHospitalAdminApprovalsService(vm);

                await service.Load();
            }
            else if(User.Default.CurrentProfile == "pharmacy_admin")
            {
                this._navigationService.Navigate(ref this._pharmacyAdminApprovals);

                var vm = (PharmacyAdminApprovalsViewModel)this._pharmacyAdminApprovals.DataContext;

                var service = new LoadPharmacyAdminApprovalsService(vm);

                await service.Load();
            }
            else
            {
                this._navigationService.Navigate(ref this._ministryWorkerApprovals);

                var vm = (MinistryWorkerApprovalsViewModel)this._ministryWorkerApprovals.DataContext;

                var pharmacyAdminsService = new LoadUnapprovedPharmacyAdminsService(vm);

                await pharmacyAdminsService.Load();

                var hospitalAdminsService = new LoadUnapprovedHospitalAdminsService(vm);

                await hospitalAdminsService.Load();
            }
        }
    }
}