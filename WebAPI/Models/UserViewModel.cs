using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class UserViewModel
    {
        [Required]
        [StringLength(maximumLength:100,MinimumLength =5)]
        public string loginName { get; set; }
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string loginPwd { get; set; }
    }
}