﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer.User
{
    public class UsersModel
    {
        [Required]
        [RegularExpression("[A-Z]{1}[a-z]{5,}", ErrorMessage = "Please Enter for Firstname Atleast 5 character with first letter capital")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression("[A-Z]{1}[a-z]{5,}", ErrorMessage = "Please Enter for LastName Atleast 5 character with first letter capital")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^([A-Za-z0-9]{3,20})([.][A-Za-z0-9]{1,10})*([@][A-Za-z]{2,5})+[.][A-Za-z]{2,3}([.][A-Za-z]{2,3})?$", ErrorMessage = "Please Enter Valid Email")]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$_])[a-zA-Z0-9@#$_]{8,}", ErrorMessage = "Please Enter Atleast 8 character with Alteast one numeric,special character")]
        public string Password { get; set; }

    }
}
