using System;
using NUnit.Framework;
using Moq;
using WeddingPlanning.GuestStore;
using WeddingPlanning.StuffStorage;
using WeddingPlanning.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeddingPlanning.UnitTests
{
    [TestFixture]
    public class TestGetGuests
    {
        private Mock<IGuestStore> GetFakeGuestStore()
        {
            return new Mock<IGuestStore>();
        }

        private Mock<IStorageProvider> GetFakeStorageProvider()
        {
            return new Mock<IStorageProvider>();
        }

        private void AddGuestToFakeStorer(Mock<IStorageProvider> fakeStorer, Guid id, string firstName, string surname, bool isComing)
        {
            var data = new List<GuestViewModel> { new GuestViewModel { Id = id, FirstName = firstName, Surname = surname, IsComing = isComing } };
            fakeStorer
                .Setup(x => x.GetGuests(null))
                .Returns<IEnumerable<GuestViewModel>>(foo => data );
        }

        [Test]
        public void TestGetGuests_OneGuest_GuestReturned()
        {
            var fakeStorer = GetFakeStorageProvider();

            var id = Guid.NewGuid();
            var firstName = "somebody";
            var surname =  "boring";
            var isComing = true;

            AddGuestToFakeStorer(fakeStorer, id, firstName, surname, isComing);

            var guestStore = new GuestStore.GuestStore(fakeStorer.Object);

            var guests = guestStore.GetGuests();

            Assert.That((guests as List<GuestViewModel>).Count == 1);
            var guest = guests.First();
            Assert.AreEqual(id, guest.Id);
            Assert.AreEqual(firstName, guest.FirstName);
            Assert.AreEqual(surname, guest.Surname);
            Assert.AreEqual(true, guest.IsComing);
        }
    }
}
