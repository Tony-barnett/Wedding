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
        Task RemoveGuest(IGuest guest);
        Task UpdateGuest(IGuest guest);
    }
}
