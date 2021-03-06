﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeClient
{
    /// <summary>
    /// Describes a recipe history.
    /// </summary>
    public class RecipeHistory
    {
        /// <summary>
        /// An identifier of the recipe history.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// An identifier of the recipe which history is current.
        /// </summary>
        public string RecipeId { get; set; }

        /// <summary>
        /// An identifier of the pharmacy which sold some medicines.
        /// </summary>
        public int PharmacyId { get; set; }

        /// <summary>
        /// The date of the creation of the cuurent history.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// A collection of recipeItemHistory instances.
        /// </summary>
        public List<RecipeHistoryItem> Sold { get; set; }
    }
}
