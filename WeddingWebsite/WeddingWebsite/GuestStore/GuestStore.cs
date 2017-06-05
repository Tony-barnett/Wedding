using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingWebsite.Models;
using WeddingWebsite;

namespace WeddingWebsite.GuestStore
{
    using Storage;
    using WeddingWebsite.DB;

    //public interface IGuestStore
    //{ 
    //    IEnumerable<IGuest> GetGuests(Guid? inserterId = null);
    //    Task AddGuest(IGuest guest, Guid? StorerId = null);
    //    Task RemoveGuest(IGuest guest);
    //    Task<IGuest> FindGuest(Guid id);
    //}
    public interface IGuestRepository: IRepostitoryAsync<Guid, GuestViewModel>
    {
        IEnumerable<GuestViewModel> GetStoredBy(Guid storer);
    }

    public class GuestRepostory: IGuestRepository
    {
        private DbContext _DbContext;

        public GuestRepostory(DbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task CreateAsync(GuestViewModel entity)
        {
            await _DbContext.Guest.AddAsync(entity);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(GuestViewModel entity)
        {
            _DbContext.Guest.Remove(entity);
            await _DbContext.SaveChangesAsync();
        }

        public async Task<GuestViewModel> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GuestViewModel> GetAsync(Guid id)
        {
            return _DbContext.Guest.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<GuestViewModel> GetStoredBy(Guid storer)
        {
            return _DbContext.Guest
                .Where(x => x.AddedBy == storer);
        }

        public async Task UpdateAsync(GuestViewModel entity)
        {
            _DbContext.Guest.Attach(entity);
            await _DbContext.SaveChangesAsync();
        }
    }
}