﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingPlanning.Models
{
    public class HomeModels
    {
    }

    public class RedirectModel
    {
        [Required]
        public string ReturnUrl { get; set; }

        [Required]
        public string Password { get; set; }
    }
}