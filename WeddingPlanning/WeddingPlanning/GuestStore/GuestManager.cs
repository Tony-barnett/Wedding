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

        public async Task<Guid> AddGuest(GuestViewModel guest, Guid? storerId = null)
        {
            return _GuestStore.AddGuest(guest, storerId);
        }

        public async Task<IEnumerable<GuestViewModel>> GetGuests(Guid? storedBy = null)
        {
            return _GuestStore.GetGuests(storedBy);
        }

        public async Task AddChild(ChildrenViewModel child, Guid storerId)
        {
            _GuestStore.AddChild(child, storerId);
        }

        public async Task<IEnumerable<ChildrenViewModel>> GetChildren(Guid? storedBy = null)
        {
            return _GuestStore.GetChildren(storedBy);
        }
    }
}