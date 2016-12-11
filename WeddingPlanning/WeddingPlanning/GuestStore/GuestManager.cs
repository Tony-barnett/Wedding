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
            _GuestStore.AddGuest(guest, storerId);
        }

        public async Task<IEnumerable<IGuest>> GetGuests(Guid? storedBy = null)
        {
            return _GuestStore.GetGuests(storedBy);
        }

        public async Task AddChild(IChild child, Guid storerId)
        {
            _GuestStore.AddChild(child, storerId);
        }

        public async Task<IEnumerable<IChild>> GetChildren(Guid? storedBy = null)
        {
            return _GuestStore.GetChildren(storedBy);
        }
    }
}