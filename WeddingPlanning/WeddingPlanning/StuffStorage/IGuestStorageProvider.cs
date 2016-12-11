using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlanning.Models;

namespace WeddingPlanning.StuffStorage
{
    public interface IGuestStorageProvider
    {
        void StoreGuest(GuestViewModel guest, Guid? storedBy);

        GuestViewModel GetGuest(string firstName, string surname);

        IEnumerable<GuestViewModel> GetGuests(Guid? inserterId = null);

        void StoreChild(ChildrenViewModel child, Guid storedBy);

        ChildrenViewModel GetChild(string firstName, string surname);

        IEnumerable<ChildrenViewModel> GetChildren(Guid? insertId = null);

        void RemoveGuest(GuestViewModel guest);

        void RemoveChild(ChildrenViewModel child);

        void UpdateGuest(GuestViewModel guest);

        void UpdateChild(ChildrenViewModel child);
    }
}
