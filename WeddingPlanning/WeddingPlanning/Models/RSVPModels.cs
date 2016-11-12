using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WeddingPlanning.Models
{
    public class GuestViewModel
    {

        [Required]
        public bool IsComing { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Allergies")]
        public string Allergies { get; set; }
    }

    public class ChildrenViewModel
    {
        public IEnumerable<SelectListItem> DropDownItems { get; set; }
        [Display(Name = "Young Children")]
        public int Babies { get; set; }

        [Display(Name = "Children")]
        public int Children { get; set; }
    }
}