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
        Task StoreGuest(IGuest guest, Guid? storedBy);
        Task<IGuest> GetGuest(string firstName, string surname);
        Task<IGuest> GetGuest(Guid id);
        IEnumerable<IGuest> GetGuests(Guid? inserterId = null);
        Task StoreChild(IChild child, Guid storedBy);
        Task<IChild> GetChild(string firstName, string surname);
        Task<IChild> GetChild(Guid id);
        IEnumerable<IChild> GetChildren(Guid? insertId = null);
        Task RemoveGuest(IGuest guest);
        Task RemoveChild(IChild child);
        Task UpdateGuest(IGuest guest);
        Task UpdateChild(IChild child);
    }
}
