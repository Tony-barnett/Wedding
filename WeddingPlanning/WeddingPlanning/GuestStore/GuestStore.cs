using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WeddingPlanning.Models;
using WeddingPlanning.StuffStorage;

namespace WeddingPlanning.GuestStore
{
    public interface IGuestStore
    { 
        IEnumerable<IGuest> GetGuests(Guid? inserterId = null);
        IEnumerable<IChild> GetChildren(Guid? inserterId = null);
        Task AddGuest(IGuest guest, Guid? StorerId = null);
        Task AddChild(IChild child, Guid StorerId);
        Task RemoveGuest(IGuest guest);
        Task RemoveChild(IChild child);
        Task<IGuest> FindGuest(Guid id);
        Task<IChild> FindChild(Guid id);
    }

    public class GuestStore: IGuestStore
    {
        private IGuestStorageProvider _StorageProvider;

        public GuestStore(IGuestStorageProvider storageProvider = null)
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

        public Task AddGuest(IGuest guest, Guid? storerId = null)
        {
            return _StorageProvider.StoreGuest(guest, storerId);
        }

        public Task AddChild(IChild child, Guid storerId)
        {
            return _StorageProvider.StoreChild(child, storerId);
        }

        public Task RemoveGuest(IGuest guest)
        {
            return _StorageProvider.RemoveGuest(guest);
        }

        public Task RemoveChild(IChild child)
        {
            return _StorageProvider.RemoveChild(child);
        }

        public Task<IGuest> FindGuest(Guid id)
        {
            return _StorageProvider.GetGuest(id);
        }

        public Task<IChild> FindChild(Guid id)
        {
            return _StorageProvider.GetChild(id);
        }
    }
}