﻿using Desktop.Interfaces;
using InstitutionClient;
using InstitutionClient.Models;
using System.Threading.Tasks;

namespace Desktop.Services
{
    public class AddInstitutionService : IService<bool>
    {
        /// <summary>
        /// User institution API client
        /// </summary>
        private readonly Client institutionClient;

        /// <summary>
        /// Creates new instance of <see cref="AddInstitutionService"/>
        /// </summary>
        public AddInstitutionService()
        {
            this.institutionClient = ((App)App.Current).InstitutionClient;
        }

        /// <summary>
        /// Executes the service
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>result</returns>
        public async Task<bool> Execute(object parameter)
        {
            return await this.institutionClient.CreateInstitutionAsync((Institution)parameter);
        }
    }
}
