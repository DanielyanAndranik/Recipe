﻿using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Desktop.Commands;
using Desktop.Services;
using Desktop.Views.Windows;
using Desktop.Views.Pages;

namespace Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _username;

        private string _currentProfile;

        private string _fullName;

        private string _photoUrl;

        private Visibility _myRecipesVisibility;

        private Visibility _createRecipeVisibility;

        private Visibility _myApprovalsVisibility;

        private Visibility _addMedicineVisibility;

        private Visibility _addInstitutionVisibility;

        private Visibility _sellMedicinesVisibility;

        private Visibility _deleteVisibility;

        private readonly HyperLinkService _hyperLinkService;

        private readonly MainWindow _mainWindow;

        private readonly SignOutCommand _signOutCommand;

        private readonly LoadService _loadService;

        private readonly DeleteCommand _deleteCommand;

        private readonly ChangeLangCommand _changeLangCommand;

        private readonly NavigateCommand<AddMedicine> _addMed;

        private readonly NavigateCommand<AddPatientProfile> _addPatient;

        private readonly NavigateCommand<AddPharmacistProfile> _addPharmacist;

        private readonly NavigateCommand<AddInstitution> _addInst;

        private readonly NavigateCommand<AddDoctorProfile> _addDoc;

        private readonly NavigateCommand<AddHospitalAdministartorProfile> _addHospAdmin;

        private readonly NavigateCommand<AddPharmacyAdminProfile> _addPharmacyAdmin;

        private readonly NavigateCommand<Hospitals> _showHospitals;

        private readonly NavigateCommand<Pharmacies> _showPharmacies;

        public string Username
        {
            get => this._username;

            set => this.Set("Username", ref this._username, value);
        }

        public string CurrentProfile
        {
            get => this._currentProfile;

            set => this.Set("CurrentProfile",ref this._currentProfile,value);
        }

        public string FullName
        {
            get => this._fullName;

            set => this.Set("FullName", ref this._fullName, value);
        }

        public Visibility MyRecipesVisibility
        {
            get => this._myRecipesVisibility;

            set => this.Set("MyRecipesVisibility", ref this._myRecipesVisibility, value);
        }

        public Visibility CreateRecipeVisibility
        {
            get => this._createRecipeVisibility;

            set => this.Set("CreateRecipeVisibility", ref this._createRecipeVisibility, value);
        }

        public Visibility MyApprovalsVisibility
        {
            get => this._myApprovalsVisibility;

            set => this.Set("MyApprovalsVisibility", ref this._myApprovalsVisibility, value);
        }

        public Visibility AddInstitutionVisibility
        {
            get => this._addInstitutionVisibility;

            set => this.Set("AddInstitutionVisibility", ref this._addInstitutionVisibility, value);
        }

        public Visibility AddMedicineVisibility
        {
            get => this._addMedicineVisibility;

            set => this.Set("AddMedicineVisibility", ref this._addMedicineVisibility, value);
        }

        public Visibility SellMedicinesVisibility
        {
            get => this._sellMedicinesVisibility;

            set => this.Set("SellMedicinesVisibility", ref this._sellMedicinesVisibility, value);
        }

        public Visibility DeleteVisibility
        {
            get => this._deleteVisibility;

            set => this.Set("DeleteVisibility", ref this._deleteVisibility, value);
        }

        public string PhotoUrl
        {
            get => this._photoUrl;

            set => this.Set("PhotoUrl", ref this._photoUrl, value);
        }

        public LoadService LoadService => this._loadService;

        public ICommand SignOutCommand => this._signOutCommand;

        public ICommand DeleteCommand => this._deleteCommand;

        public ICommand ChangeLangCommand => this._changeLangCommand;

        public ICommand AddDoc => this._addDoc;

        public ICommand AddPatient => this._addPatient;

        public ICommand AddPharmacist => this._addPharmacist;

        public ICommand AddPharmacyAdmin => this._addPharmacyAdmin;

        public ICommand AddHospitalAdmin => this._addHospAdmin;

        public ICommand AddMed => this._addMed;

        public ICommand AddInst => this._addInst;

        public ICommand ShowHospitals => this._showHospitals;

        public ICommand ShowPharmacies => this._showPharmacies;

        public MainWindowViewModel(MainWindow mainWindow)
        {
            this.SetDefaultVisibilities();

            this._mainWindow = mainWindow;
            this._signOutCommand = new SignOutCommand();
            this._hyperLinkService = new HyperLinkService();
            this._loadService = new LoadService(this._mainWindow.profiles, this);
            this._deleteCommand = new DeleteCommand();
            this._changeLangCommand = new ChangeLangCommand(this);
            this._addDoc = new NavigateCommand<AddDoctorProfile>(this._mainWindow.frame);
            this._addPatient = new NavigateCommand<AddPatientProfile>(this._mainWindow.frame);
            this._addPharmacist = new NavigateCommand<AddPharmacistProfile>(this._mainWindow.frame);
            this._addPharmacyAdmin = new NavigateCommand<AddPharmacyAdminProfile>(this._mainWindow.frame);
            this._addHospAdmin = new NavigateCommand<AddHospitalAdministartorProfile>(this._mainWindow.frame);
            this._addMed = new NavigateCommand<AddMedicine>(this._mainWindow.frame);
            this._addInst = new NavigateCommand<AddInstitution>(this._mainWindow.frame);
            this._showHospitals = new NavigateCommand<Hospitals>(this._mainWindow.frame);
            this._showPharmacies = new NavigateCommand<Pharmacies>(this._mainWindow.frame);
        }

        private void SetDefaultVisibilities()
        {
            this.AddInstitutionVisibility = Visibility.Collapsed;
            this.AddMedicineVisibility = Visibility.Collapsed;
            this.CreateRecipeVisibility = Visibility.Collapsed;
            this.MyApprovalsVisibility = Visibility.Collapsed;
            this.MyRecipesVisibility = Visibility.Collapsed;
            this.SellMedicinesVisibility = Visibility.Collapsed;
            this.DeleteVisibility = Visibility.Visible;
            this.CreateRecipeVisibility = Visibility.Collapsed;
        }
    }
}
