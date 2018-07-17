﻿using System;

namespace UserManagementAPI.Models
{
    /// <summary>
    /// Class for profile
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Gets or sets type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets IsApproved value
        /// </summary>
        public bool IsApproved { get; set; }
    }
}
