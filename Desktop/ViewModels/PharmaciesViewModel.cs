﻿using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using Desktop.Commands;
using System.Collections.ObjectModel;
using InstitutionClient.Models;
using System.Windows;
using Desktop.Services;
using System;

namespace Desktop.ViewModels
{
    public class PharmaciesViewModel : ViewModelBase
    {

        private ObservableCollection<Institution> pharmacies;

        private Institution editablePharmacy;

        private Visibility visibility;

        private readonly FilterService<Institution> filterService;

        private readonly DeletePharmacyCommand deletePharmacyCommand;

        private readonly EditPharmacyCommand editPharmacyCommand;


        public ObservableCollection<Institution> Pharmacies
        {
            get
            {
                return this.pharmacies;
            }
            set
            {
                this.Set("Pharmacies", ref this.pharmacies, value);
            }
        }

        public Institution EditablePharmacy
        {
            get
            {
                return this.editablePharmacy;
            }
            set
            {
                this.Set("EditablePharmacy", ref this.editablePharmacy, value);
            }
        }

        public Visibility Visibility
        {
            get
            {
                return this.visibility;
            }
            set
            {
                this.Set("Visibility", ref this.visibility, value);
            }
        }

        public DeletePharmacyCommand DeletePharmacyCommand
        {
            get => this.deletePharmacyCommand;
        }

        public EditPharmacyCommand EditPharmacyCommand
        {
            get => this.editPharmacyCommand;
        }

        public PharmaciesViewModel()
        {
            this.Visibility = (User.Default.CurrentProfile == "ministry_worker") || (User.Default.CurrentProfile == "pharmacy_admin") ? Visibility.Visible : Visibility.Collapsed;
            this.deletePharmacyCommand = new DeletePharmacyCommand(this.pharmacies, this.deletePharmacy, _ => true);
            this.editPharmacyCommand = new EditPharmacyCommand(this.pharmacies, this.editPharmacy, _ => true);
            this.filterService = new FilterService<Institution>();
        }

        public async Task Filter(Func<Institution, bool> predicate)
        {
            var pharmacies = await this.filterService.FilterAsync(this.Pharmacies, predicate);

            this.Pharmacies = new ObservableCollection<Institution>(pharmacies);
        }

        private async Task<bool> deletePharmacy(int id)
        {
            return await ((App)App.Current).InstitutionClient.DeleteInstitutionAsync(id);
        }

        private async Task<bool> editPharmacy(Institution pharmacy)
        {
            return await ((App)App.Current).InstitutionClient.UpdateInstitutionAsync(pharmacy);
        }
    }
}
