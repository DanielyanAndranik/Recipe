﻿using Desktop.ViewModels;
using Desktop.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    /// <summary>
    /// Load unapproved pharmacy admins service.
    /// </summary>
    class LoadUnapprovedPharmacyAdminsService
    {
        /// <summary>
        /// Mininstry worker approvals viewmodel.
        /// </summary>
        private readonly MinistryWorkerApprovalsViewModel viewModel;

        /// <summary>
        /// User management api client.
        /// </summary>
        private readonly UserManagementApiClient client;

        /// <summary>
        /// Creates new instance of <see cref="MinistryWorkerApprovalsViewModel"/>
        /// </summary>
        /// <param name="viewModel">Ministry worker approvals view model</param>
        public LoadUnapprovedPharmacyAdminsService(MinistryWorkerApprovalsViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.client = ((App)App.Current).UserApiClient;
        }

        /// <summary>
        /// Loads unapproved  pharmacy admins.
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            var unapprovedPharmacyAdminsResponse = await this.client.GetUnapprovedPharmacyAdmins();

            if (unapprovedPharmacyAdminsResponse.Status == Status.Error)
            {
                RecipeMessageBox.Show("Couldn't get unapproved hospital admins.");
                return;
            }

            this.viewModel.PharmacyAdmins = new ObservableCollection<UnapprovedPharmacyAdmin>(unapprovedPharmacyAdminsResponse.Result);
        }
    }
}
