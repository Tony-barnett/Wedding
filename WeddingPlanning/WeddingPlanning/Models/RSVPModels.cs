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

        public IEnumerable<IGuest> AllGuests { get; set; }

        public IEnumerable<IGuest> Adults { get { return AllGuests.Where(g => g is IGuest && storerId.HasValue && g.AddedBy == storerId); } }
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

        [Display(Name = "Dietary Requirements")]
        public string Allergies { get; set; }

        public Guid? AddedBy { get; set; }

        [Display(Name = "Under 12")]
        public bool IsChild { get; set; }

        [Display(Name = "Under 5")]
        public bool IsBaby { get; set; }

        public AgeGroup AgeGroup
        {
            get
            {
                if (IsBaby)
                {
                    return AgeGroup.Baby;
                }
                else if (IsChild)
                {
                    return AgeGroup.Child;
                }

                return AgeGroup.Adult;
            }
            set
            {
                switch (value)
                {
                    case AgeGroup.Baby:
                        IsBaby = true;
                        break;
                    case AgeGroup.Child:
                        IsChild = true;
                        break;
                }
            }
        }
    }

    public enum AgeGroup
    {
        Adult,
        /// <summary>
        /// Under 12
        /// </summary>
        Child,
        /// <summary>
        /// Under 5 or under 3
        /// </summary>
        Baby
    }
}