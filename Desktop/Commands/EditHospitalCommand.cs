﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Desktop.Views.Windows;
using InstitutionClient.Models;

namespace Desktop.Commands
{
    /// <summary>
    /// Edit hospital command
    /// </summary>
    public class EditHospitalCommand : AsyncCommand<Institution, bool>
    {
        /// <summary>
        /// Hospitals
        /// </summary>
        private ObservableCollection<Institution> hospitals;

        /// <summary>
        /// Creates new instance of <see cref="EditHospitalCommand"/>
        /// </summary>
        /// <param name="hospitals">Hospitals</param>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">CanExecute method</param>
        public EditHospitalCommand(ObservableCollection<Institution> hospitals, Func<Institution, Task<bool>> executeMethod, Func<Institution, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        {
            this.hospitals = hospitals;
        }

        /// <summary>
        /// Executes command operation
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public async override void Execute(object parameter)
        {
            try
            {
                var isSucceed = await this.ExecuteAsync(parameter as Institution);

                if (isSucceed)
                {
                    RecipeMessageBox.Show("Hospital updated successfully");
                    
                    var response = await ((App)App.Current).InstitutionClient.GetAllHospitalsAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        this.hospitals = new ObservableCollection<Institution>(response.Content);
                    }
                }
                else
                {
                    RecipeMessageBox.Show("Unable to update hospital");
                }
            }
            catch (Exception)
            {
                RecipeMessageBox.Show("Server is not responding");
            }
        }
    }
}
