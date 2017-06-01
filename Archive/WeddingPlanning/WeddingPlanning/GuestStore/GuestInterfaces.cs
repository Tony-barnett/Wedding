using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlanning.Models;

namespace WeddingPlanning.GuestStore
{
    public interface IGuest
    {
        Guid? AddedBy { get; set; }
        Guid? Id { get; set; }
        string Allergies { get; set; }
        string FirstName { get; set; }
        string Surname { get; set; }
        bool IsComing { get; set; }
        AgeGroup AgeGroup { get; }
        GuestType GuestType { get; set; }
    }
}
