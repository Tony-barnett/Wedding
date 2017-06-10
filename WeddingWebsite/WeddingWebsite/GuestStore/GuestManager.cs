using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingWebsite.Models;

namespace WeddingWebsite.GuestStore
{
    public interface IGuestManager
    {
        Task AddGuest(GuestViewModel guest);
        IEnumerable<GuestViewModel> GetGuestsStoredBy(Guid storedBy);
        Task RemoveGuest(GuestViewModel guest);
        Task<GuestViewModel> GetGuest(Guid guestId);
        Task EditGuest(GuestViewModel guest);
    }

    public class GuestManager : IGuestManager
    {
        private IGuestRepository _GuestStore {get;set;}

        public GuestManager(IGuestRepository guestStore)
        {
            _GuestStore = guestStore;
        }

        public async Task AddGuest(GuestViewModel guest)
        {
            await _GuestStore.CreateAsync(guest);
        }

        public IEnumerable<GuestViewModel> GetGuestsStoredBy(Guid storedBy)
        {
            return _GuestStore.GetStoredBy(storedBy);
        }
        
        public async Task RemoveGuest(GuestViewModel guest)
        {
            await _GuestStore.DeleteAsync(guest);
        }

        public async Task<GuestViewModel> GetGuest(Guid guestId)
        {
            return await _GuestStore.GetAsync(guestId);
        }

        public async Task EditGuest(GuestViewModel guest)
        {
            await _GuestStore.UpdateAsync(guest);
        }
    }
}