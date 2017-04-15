using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingWebsite.Models;

namespace WeddingWebsite.GuestStore
{
    public interface IGuestManager
    {
        Task AddGuest(IGuest guest);
        IEnumerable<IGuest> GetGuestsStoredBy(Guid storedBy);
        Task RemoveGuest(IGuest guest);
        Task<IGuest> GetGuest(Guid guestId);
    }

    public class GuestManager: IGuestManager
    {
        private IGuestRepository _GuestStore {get;set;}

        public GuestManager(IGuestRepository guestStore)
        {
        }

        public async Task AddGuest(IGuest guest)
        {
            await _GuestStore.CreateAsync(guest);
        }

        public IEnumerable<IGuest> GetGuestsStoredBy(Guid storedBy)
        {
            return _GuestStore.GetStoredBy(storedBy);
        }
        
        public async Task RemoveGuest(IGuest guest)
        {
            await _GuestStore.DeleteAsync(guest);
        }

        public async Task<IGuest> GetGuest(Guid guestId)
        {
            return await _GuestStore.GetAsync(guestId);
        }
    }
}