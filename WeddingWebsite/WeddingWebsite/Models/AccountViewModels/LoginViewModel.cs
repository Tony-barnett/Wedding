﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeddingWebsite.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
