using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WeddingPlanning.GuestStore;

namespace WeddingPlanning.Models
{
    public class Guests
    {
        public Guid? storerId { get; set; }

        public IEnumerable<IPerson> AllGuests { get; set; }

        public IEnumerable<IPerson> Adults { get { return AllGuests.Where(g => g is IGuest && storerId.HasValue && g.AddedBy == storerId); } }

        public IEnumerable<IPerson> Children { get { return AllGuests.Where(g => g is IChild && storerId.HasValue && g.AddedBy == storerId); } }
    }

    public class GuestViewModel : IGuest
    {
        public Guid? Id { get; set; }

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

        public Guid? AddedBy { get; set; }
    }

    public class ChildrenViewModel: IChild
    {
        public Guid? Id { get; set; }

        [Required]
        public bool IsComing { get; set; }
    
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

        public Guid? AddedBy { get; set; }
    }
}