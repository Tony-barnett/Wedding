using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WeddingWebsite.GuestStore;

namespace WeddingWebsite.Models
{
    public class Guests
    {
        public Guid? storerId { get; set; }

        public IEnumerable<GuestViewModel> AllGuests { get; set; }

        public IEnumerable<GuestViewModel> Adults { get { return AllGuests.Where(g => storerId.HasValue && g.AddedBy == storerId); } }
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

        [Display(Name = "Under 18")]
        public bool IsChild { get; set; }

        [Display(Name = "Under 10")]
        public bool IsYoungChild { get; set; }

        [Display(Name = "Under 3")]
        public bool IsBaby { get; set; }

        public AgeGroup AgeGroup
        {
            get
            {
                if (IsBaby)
                {
                    return AgeGroup.Baby;
                }
                else if (IsYoungChild)
                {
                    return AgeGroup.YoungChild;
                }
                else if (IsChild)
                {
                    return AgeGroup.Child;
                }

                return AgeGroup.Adult;
            }
        }

        public GuestType GuestType { get; set; }
    }

    public enum AgeGroup
    {
        Adult,
        /// <summary>
        /// Under 18
        /// </summary>
        Child,
        /// <summary>
        /// Under 10
        /// </summary>
        YoungChild,
        /// <summary>
        /// Under 3
        /// </summary>
        Baby
    }
}