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

        public async Task RemoveGuest(IGuest guest)
        {
            _GuestStore.RemoveGuest(guest);
        }

        public async Task RemoveChild(IChild child)
        {
            _GuestStore.RemoveChild(child);
        }

        public async Task<IGuest> GetGuest(Guid guestId)
        {
            return _GuestStore.FindGuest(guestId);
        }

        public async Task<IChild> GetChild(Guid id)
        {
            return _GuestStore.FindChild(id);
        }
    }
}