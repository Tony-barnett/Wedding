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
        IEnumerable<GuestViewModel> GetGuests(Guid? inserterId = null);

        IEnumerable<ChildrenViewModel> GetChildren();

        Guid AddGuest(GuestViewModel guest, Guid? StorerId = null);

        void AddChild(ChildrenViewModel child, Guid StorerId);

    }

    public class GuestStore: IGuestStore
    {
        private IStorageProvider _StorageProvider;

        public GuestStore(IStorageProvider storageProvider = null)
        {
            _StorageProvider = storageProvider ?? new CSVStorer();
        }
        public IEnumerable<GuestViewModel> GetGuests(Guid? inserterId = null)
        {
            return _StorageProvider.GetGuests(inserterId);
        }

        public IEnumerable<ChildrenViewModel> GetChildren()
        {
            return new List<ChildrenViewModel>();
        }

        public Guid AddGuest(GuestViewModel guest, Guid? storerId = null)
        {
            return _StorageProvider.StoreGuest(guest, storerId);
        }

        public void AddChild(ChildrenViewModel child, Guid storerId)
        {
            _StorageProvider.StoreChild(child, storerId);
        }

    }
}