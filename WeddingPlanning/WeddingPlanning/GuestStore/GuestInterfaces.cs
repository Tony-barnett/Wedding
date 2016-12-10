using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingPlanning.GuestStore
{
    public interface IGuest: IPerson
    {
        bool IsComing { get; set; }
    }

    public interface IChild: IPerson
    {
        bool IsBaby { get; set; }
    }

    public interface IPerson
    {
        Guid? AddedBy { get; set; }
        Guid? Id { get; set; }
        string Allergies { get; set; }
        string FirstName { get; set; }
        string Surname { get; set; }
    }
}
