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
        int StoreGuest(GuestViewModel guest, int? storedBy);

        GuestViewModel GetGuest(string firstName, string surname);

        IEnumerable<GuestViewModel> GetGuests(int inserterId);

        void StoreChild(ChildrenViewModel child, int storedBy);

        ChildrenViewModel GetChild(string firstName, string surname);

        IEnumerable<ChildrenViewModel> GetChildren(int insertId);

        void RemoveGuest(GuestViewModel guest);

        void RemoveChild(ChildrenViewModel child);

        void UpdateGuest(GuestViewModel guest);

        void UpdateChild(ChildrenViewModel child);
    }
}
