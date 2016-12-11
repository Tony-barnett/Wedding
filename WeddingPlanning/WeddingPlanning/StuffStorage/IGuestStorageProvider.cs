using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlanning.GuestStore;
using WeddingPlanning.Models;

namespace WeddingPlanning.StuffStorage
{
    public interface IGuestStorageProvider
    {
        void StoreGuest(IGuest guest, Guid? storedBy);

        IGuest GetGuest(string firstName, string surname);

        IGuest GetGuest(Guid guestId);

        IEnumerable<IGuest> GetGuests(Guid? inserterId = null);

        void StoreChild(IChild child, Guid storedBy);

        IChild GetChild(string firstName, string surname);

        IEnumerable<IChild> GetChildren(Guid? insertId = null);

        void RemoveGuest(IGuest guest);

        void RemoveChild(IChild child);

        void UpdateGuest(IGuest guest);

        void UpdateChild(IChild child);
    }
}
