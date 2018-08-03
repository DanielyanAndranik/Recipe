﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Desktop.Views.Windows;
using InstitutionClient.Models;

namespace Desktop.Commands
{
    /// <summary>
    /// Delete hospital command
    /// </summary>
    public class DeleteHospitalCommand : AsyncCommand<int, bool>
    {
        /// <summary>
        /// Hospitals
        /// </summary>
        private ObservableCollection<Institution> hospitals;

        /// <summary>
        /// Creates new instance of <see cref="DeleteHospitalCommand"/>
        /// </summary>
        /// <param name="hospitals">Hospitals</param>
        /// <param name="executeMethod">Execute method.</param>
        /// <param name="canExecuteMethod">Can execute method</param>
        public DeleteHospitalCommand(ObservableCollection<Institution> hospitals, Func<int, Task<bool>> executeMethod, Func<int, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        {
            this.hospitals = hospitals;
        }

        /// <summary>
        /// Execute the command operation.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public async override void Execute(object parameter)
        {
            try
            {
                var isSuccessed = await this.ExecuteAsync((int)parameter);
                if (isSuccessed)
                {
                    RecipeMessageBox.Show("Hospital removed successfully");

                    var response = await ((App)App.Current).InstitutionClient.GetAllHospitalsAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        this.hospitals = new ObservableCollection<Institution>(response.Content);
                    }
                }
                else
                {
                    RecipeMessageBox.Show("Unable to remove hospital");
                }
            }
            catch (Exception)
            {
                RecipeMessageBox.Show("Server is not responding");
            }
        }
    }
}
