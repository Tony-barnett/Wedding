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

        [Display(Name = "Allergies")]
        public string Allergies { get; set; }

        public Guid? AddedBy { get; set; }

        [Display(Name = "Under 12")]
        public bool IsChild { get; set; }

        [Display(Name = "Under 5")]
        public bool IsBaby { get; set; }

        internal AgeGroup? _AgeGroup;

        public AgeGroup AgeGroup
        {
            get
            {
                if (!_AgeGroup.HasValue)
                {
                    if (IsBaby)
                    {
                        _AgeGroup = AgeGroup.Baby;
                    }

                    else if (IsChild)
                    {
                        _AgeGroup = AgeGroup.Child;
                    }
                    else
                    {
                        _AgeGroup = AgeGroup.Adult;
                    }
                }
                return _AgeGroup.Value;
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