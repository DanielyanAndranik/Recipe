﻿using System.Linq;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    /// <summary>
    /// Pharmacy admin profile service
    /// </summary>
    public class PharmacyAdminProfileService : ProfileService
    {
        /// <summary>
        /// Executes profile service operation
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>response</returns>
        public async override Task<Response<string>> Execute(object parameter)
        {
            var pharmacyAdmin = (PharmacyAdmin)parameter;

            var institutionResponse = await institutionClient.GetInstitutionIdAsync(pharmacyAdmin.PharmacyName);

            if (!institutionResponse.IsSuccessStatusCode)
                return new Response<string>
                {
                    Result = institutionResponse.StatusCode.ToString(),
                    Status = Status.Error
                };

            int pharmacyId = institutionResponse.Content;

            var pharmacy = await this.institutionClient.GetInstitutionAsync(pharmacyId);
            pharmacyAdmin.PharmacyName = pharmacy.Content.Name;

            return await this.userManagementApiClient.PostPharmacyAdmin(pharmacyAdmin);
        }
    }
}
