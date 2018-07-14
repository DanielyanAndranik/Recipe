﻿using System.Collections.Generic;
using IdentityServer4.Models;

namespace AuthAPI
{
    /// <summary>
    /// Class for configuration data.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Gets api resources.
        /// </summary>
        /// <returns>api resources</returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("UserManagementAPI"),
<<<<<<< HEAD
                new ApiResource("RecipeApi"),
                new ApiResource("InstitutionAPI")

=======
                new ApiResource("InstitutionAPI"),
                new ApiResource("RecipeApi"),
				new ApiResource("MedicineApi")
>>>>>>> db4dfa138e161961c90417a135f2102f0c39bba6
            };
        }

        /// <summary>
        /// Gets clients.
        /// </summary>
        /// <returns>clients</returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "RecipeConsoleUI",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
<<<<<<< HEAD

                    AllowedScopes = {"UserManagementAPI", "RecipeApi", "InstitutionAPI" }

=======
                    AllowedScopes = {"UserManagementAPI", "InstitutionAPI", "RecipeApi", "MedicineApi"}
>>>>>>> db4dfa138e161961c90417a135f2102f0c39bba6
                }
            };
        }

        /// <summary>
        /// Gets identity resources.
        /// </summary>
        /// <returns>identity resources</returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResource
                {
                    Name = "Username",
                    UserClaims = new List<string> {"Username"}
                }
            };
        }
    }
}
