﻿using System.ComponentModel.DataAnnotations;

namespace ASP_Net_MVC.Models
{
    public class SignUpForm
    {
        public SignUpForm()
        {
            FirstName = "";
            LastName = "";
            Email = "";
            Password = "";
            ConfirmPassword = "";
            AddressLine = "";
            PostalCode = "";
            City = "";
            ReturnUrl = "/";
            RoleName = "user";
        }

        [Display(Name = "First name")]
        [Required(ErrorMessage ="Please enter your first name")]
        [StringLength(256, ErrorMessage = "First name must contain at least två characters",  MinimumLength = 2)]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Please enter your last name")]
        [StringLength(256, ErrorMessage = "Last name must contain at least två characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [Display(Name = "Email address")]
        [RegularExpression (@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid email")]
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(256, ErrorMessage = "First name must contain at least two characters", MinimumLength = 2)]
        public string Email { get; set; }

        [Display (Name = "Password")]
        [DataType(DataType.Password)]
        //[RegularExpression] must be added
        [Required (ErrorMessage = "Please enter a password")]
        public string Password { get; set; }

        [Display (Name = "Confirm password")]
        [DataType (DataType.Password)]
        [Required (ErrorMessage ="Please confirm your password")]
        [Compare ("Password", ErrorMessage = "The entered password does not match")]
        public string ConfirmPassword { get; set; }

        [Display (Name = "Street address")]
        [Required (ErrorMessage = "Please enter your street address")]
        [StringLength ( 256, ErrorMessage = "Street address must contain at least two characters", MinimumLength = 2)]
        public string AddressLine { get; set; }

        [Display (Name = "Postal code")]
        [Required (ErrorMessage = "Please enter your postal code")]
        [StringLength (256, ErrorMessage = "Postal must be at least 5 characters", MinimumLength = 5)]
        public string PostalCode { get; set; }

        [Display (Name = "City")]
        [Required (ErrorMessage = "Please enter your city")]
        [StringLength (256, ErrorMessage = "City must contain at least 2 characters", MinimumLength = 2)]
        public string City { get; set; }

        public string ReturnUrl { get; set; }

        public string RoleName { get; set; }
    }
}