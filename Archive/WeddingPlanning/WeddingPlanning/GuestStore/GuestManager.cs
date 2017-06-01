using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WeddingPlanning.Models;

namespace WeddingPlanning.GuestStore
{
    public class GuestManager
    {
        private IGuestStore _GuestStore {get;set;}

        public GuestManager(IGuestStore guestStore = null)
        {
            _GuestStore = guestStore ?? new GuestStore();
        }

        public async Task AddGuest(IGuest guest, Guid? storerId = null)
        {
            await _GuestStore.AddGuest(guest, storerId);
        }

        public IEnumerable<IGuest> GetGuests(Guid? storedBy = null)
        {
            return _GuestStore.GetGuests(storedBy);
        }
        
        public async Task RemoveGuest(IGuest guest)
        {
            await _GuestStore.RemoveGuest(guest);
        }

        public async Task<IGuest> GetGuest(Guid guestId)
        {
            return await _GuestStore.FindGuest(guestId);
        }
    }
}