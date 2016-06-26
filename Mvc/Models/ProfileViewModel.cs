﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        [Display(Name = "FirstName")]
        [DisplayFormat(NullDisplayText = "null")]
        public string FirstName { get; set; }
        [Display(Name = "LastName")]
        [DisplayFormat(NullDisplayText = "null")]
        public string LastName { get; set; }
        [Display(Name = "Description")]
        [DisplayFormat(NullDisplayText = "null")]
        public string Description { get; set; }
        [Display(Name = "Birthday")]
        [DisplayFormat(NullDisplayText = "null")]
        public DateTime? Birthday { get; set; }
        public IEnumerable<string> Areas { get; set; }
    }
}