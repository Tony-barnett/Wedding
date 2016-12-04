using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WeddingPlanning.GuestStore;

namespace WeddingPlanning.Models
{
    public class GuestViewModel : IGuest
    {
        public int? Id { get; set; }

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

        public int? AddedBy { get; set; }
    }

    public class ChildrenViewModel: IChild
    {
        public int? Id { get; set; }
    
        [Display(Name = "Under 12?")]
        public bool IsBaby { get; set; }

        [Display(Name = "Allergies")]
        public string Allergies { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        public int? AddedBy { get; set; }
    }
}