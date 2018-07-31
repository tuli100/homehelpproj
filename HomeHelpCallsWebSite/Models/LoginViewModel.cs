using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeHelpCallsWebSite.Models
{
    public class LoginViewModel
    {
        [Required]
        public string USER_NAME { get; set; }
        public bool IsApproved { get; set; }
        [Required]
        public string SmsPassword { get; set; }
        [Required]
        public int UserId { get; set; }

        public string[] Warnings { get; set; }
    }
}