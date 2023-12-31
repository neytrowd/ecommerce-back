﻿namespace Ecommerce.Web.Contract.Models.AppUser
{
    public class AppUserModel
    {
        public long Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }
        
        public bool IsEmailConfirmed { get; set; }
    }
}