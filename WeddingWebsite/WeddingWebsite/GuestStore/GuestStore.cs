using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingWebsite.Models;
using WeddingWebsite;

namespace WeddingWebsite.GuestStore
{
    using Storage;
    //public interface IGuestStore
    //{ 
    //    IEnumerable<IGuest> GetGuests(Guid? inserterId = null);
    //    Task AddGuest(IGuest guest, Guid? StorerId = null);
    //    Task RemoveGuest(IGuest guest);
    //    Task<IGuest> FindGuest(Guid id);
    //}
    public interface IGuestRepository: IRepostitoryAsync<Guid, IGuest>
    {
        IEnumerable<IGuest> GetStoredBy(Guid storer);
    }

    public class GuestRepostory: IGuestRepository
    {
        public GuestRepostory()
        {
        }

        public async Task CreateAsync(IGuest entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(IGuest entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IGuest> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IGuest> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGuest> GetStoredBy(Guid storer)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(IGuest entity)
        {
            throw new NotImplementedException();
        }
    }
}