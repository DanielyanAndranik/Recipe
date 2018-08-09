﻿using Desktop.Services;
using Desktop.ViewModels;
using Desktop.Views.Windows;
using RecipeClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace Desktop.Commands
{
    public class FindRecipeCommand : AsyncCommand<string, ResponseMessage<RecipeClient.Recipe>>
    {
        private SellMedicinesViewModel ViewModel;

        public FindRecipeCommand(SellMedicinesViewModel viewModel, Func<string, Task<ResponseMessage<RecipeClient.Recipe>>> executeMethod, Func<string, bool> canExecuteMethod)
            : base(executeMethod, canExecuteMethod)
        {
            this.ViewModel = viewModel;
        }

        public async override void Execute(object parameter)
        {
            var response = await this.ExecuteAsync((string)parameter);

            if (!response.IsSuccessStatusCode)
            {
                RecipeMessageBox.Show((string)App.Current.Resources["recipe_find_fail"]);
                return;
            }

            var recipe = response.Content;

            var loadService = new LoadRecipesService();

            this.ViewModel.Recipe = new ObservableCollection<Models.Recipe>();
            this.ViewModel.Recipe.Add(await loadService.Map(recipe));

            this.ViewModel.HistoryItems = this.Map(this.ViewModel.Recipe.First());
        }

        private ObservableCollection<RecipeHistoryItem> Map(Models.Recipe recipe)
        {
            var historyItems = new ObservableCollection<RecipeHistoryItem>();
            foreach(var item in recipe.RecipeItems)
            {
                historyItems.Add(new RecipeHistoryItem { MedicineId = item.Medicine.Id });
            }

            return historyItems;
        }
    }
}
