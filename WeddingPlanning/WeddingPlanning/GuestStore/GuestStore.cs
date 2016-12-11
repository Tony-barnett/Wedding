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
        IEnumerable<IGuest> GetGuests(Guid? inserterId = null);

        IEnumerable<IChild> GetChildren(Guid? inserterId = null);

        void AddGuest(IGuest guest, Guid? StorerId = null);

        void AddChild(IChild child, Guid StorerId);

    }

    public class GuestStore: IGuestStore
    {
        private IStorageProvider _StorageProvider;

        public GuestStore(IStorageProvider storageProvider = null)
        {
            _StorageProvider = storageProvider ?? new CSVStorer();
        }
        public IEnumerable<IGuest> GetGuests(Guid? inserterId = null)
        {
            return _StorageProvider.GetGuests(inserterId);
        }

        public IEnumerable<IChild> GetChildren(Guid? inserterId = null)
        {
            return _StorageProvider.GetChildren(inserterId);
        }

        public void AddGuest(IGuest guest, Guid? storerId = null)
        {
            _StorageProvider.StoreGuest(guest, storerId);
        }

        public void AddChild(IChild child, Guid storerId)
        {
            _StorageProvider.StoreChild(child, storerId);
        }

    }
}