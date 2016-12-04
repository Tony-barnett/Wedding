using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeddingPlanning.Models;
using WeddingPlanning.StuffStorage;

namespace WeddingPlanning.GuestStore
{
    public interface IGuestStore
    {
        IEnumerable<GuestViewModel> GetGuests();

        IEnumerable<ChildrenViewModel> GetChildren();

        int AddGuest(GuestViewModel guest, int? StorerId = null);

        void AddChild(ChildrenViewModel child, int StorerId);

    }

    public class GuestStore: IGuestStore
    {
        private IStorageProvider _StorageProvider;

        public GuestStore(IStorageProvider storageProvider = null)
        {
            _StorageProvider = storageProvider ?? new CSVStorer();
        }
        public IEnumerable<GuestViewModel> GetGuests()
        {
            return new List<GuestViewModel>();
        }

        public IEnumerable<ChildrenViewModel> GetChildren()
        {
            return new List<ChildrenViewModel>();
        }

        public int AddGuest(GuestViewModel guest, int? storerId = null)
        {
            return _StorageProvider.StoreGuest(guest, storerId);
        }

        public void AddChild(ChildrenViewModel child, int storerId)
        {
            _StorageProvider.StoreChild(child, storerId);
        }

    }
}