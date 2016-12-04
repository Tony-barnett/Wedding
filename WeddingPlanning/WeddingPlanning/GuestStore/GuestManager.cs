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

        public async Task<int> AddGuest(GuestViewModel guest, int? storerId = null)
        {
            return _GuestStore.AddGuest(guest, storerId);
        }

        public async Task<IEnumerable<GuestViewModel>> GetGuests()
        {
            return _GuestStore.GetGuests();
        }

        public async Task AddChild(ChildrenViewModel child, int storerId)
        {
            _GuestStore.AddChild(child, storerId);
        }

        public async Task<IEnumerable<ChildrenViewModel>> GetChildren()
        {
            return _GuestStore.GetChildren();
        }
    }
}