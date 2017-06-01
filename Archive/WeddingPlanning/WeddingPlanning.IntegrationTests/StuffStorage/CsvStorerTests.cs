using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlanning;

namespace WeddingPlanning.IntegrationTests.StuffStorage
{
    [TestFixture]
    class CsvStorerTests
    {
        [TearDown]
        public void TearDown()
        {
            var csvStorer = new WeddingPlanning.StuffStorage.CSVStorer();

            File.Delete(csvStorer._GuestLocation);
        }

        [Test]
        public void TestWritePersonToCsv_WithId_PersonAddedToCsv()
        {
            var id = Guid.NewGuid();
            var guestViewModel = new WeddingPlanning.Models.GuestViewModel
            {
                Id = id,
                FirstName = "foo",
                Surname = "bar",
                IsComing = true,
                Allergies = "Some"
            };

            var csvStorer = new WeddingPlanning.StuffStorage.CSVStorer();

            csvStorer.StoreGuest(guestViewModel);

            using (StreamReader reader = new StreamReader(csvStorer._GuestLocation))
            {
                var file = reader.ReadToEnd();

                Assert.AreEqual($"{id}", file.Substring(0, file.IndexOf(",")).Replace("\"", ""));
            }
        }

        [Test]
        public void TestWritePersonToCsv_TwoPeopleWithId_PeopleAddedToCsv()
        {
            var initialGuestId = Guid.NewGuid();
            var secondGuestId = Guid.NewGuid();
            var guestViewModel = new WeddingPlanning.Models.GuestViewModel
            {
                Id = initialGuestId,
                FirstName = "foo",
                Surname = "dillyDoo",
                IsComing = true,
                Allergies = "Some"
            };

            var guestViewModel2 = new WeddingPlanning.Models.GuestViewModel
            {
                Id = secondGuestId,
                FirstName = "bar",
                Surname = "baram",
                IsComing = true,
                Allergies = "none, guv",
                AddedBy = initialGuestId
            };

            var csvStorer = new WeddingPlanning.StuffStorage.CSVStorer();

            csvStorer.StoreGuest(guestViewModel);
            csvStorer.StoreGuest(guestViewModel2);

            Assert.AreEqual(initialGuestId, guestViewModel.Id);

            using (StreamReader reader = new StreamReader(csvStorer._GuestLocation))
            {
                var file = reader.ReadToEnd();

                var peopleInFile = file.Replace("\r", "").Split('\n');
                Assert.AreEqual($"{initialGuestId}", peopleInFile[0].Substring(0, peopleInFile[0].IndexOf(",")).Replace("\"", ""));
                Assert.AreEqual($"{secondGuestId}", peopleInFile[1].Substring(0, peopleInFile[1].IndexOf(",")).Replace("\"", ""));
            }
        }

        [Test]
        public void TestWritePersonToCsv_WithoutId_PersonAddedToCsv()
        {
            var guestViewModel = new WeddingPlanning.Models.GuestViewModel
            {
                FirstName = "foo",
                Surname = "dillyDoo",
                IsComing = true,
                Allergies = "Some"
            };

            var csvStorer = new WeddingPlanning.StuffStorage.CSVStorer();

            csvStorer.StoreGuest(guestViewModel);

            using (StreamReader reader = new StreamReader(csvStorer._GuestLocation))
            {
                var file = reader.ReadToEnd();

                var peopleInFile = file.Replace("\r", "").Split('\n');
                Assert.AreEqual($"{guestViewModel.Id}", peopleInFile[0].Substring(0, peopleInFile[0].IndexOf(",")).Replace("\"", ""));
            }
        }

        [Test]
        public void TestReadPersonFromCsv_OnePersonAdded_GuestReturned()
        {
            var firstName = "foo";
            var surname = "bar";
            var guestViewModel = new WeddingPlanning.Models.GuestViewModel
            {
                FirstName = firstName,
                Surname = surname,
                IsComing = true,
                Allergies = "Some"
            };

            var csvStorer = new WeddingPlanning.StuffStorage.CSVStorer();

            csvStorer.StoreGuest(guestViewModel);

            var foundUser = csvStorer.GetGuest(firstName, surname).Result;

            Assert.AreEqual(guestViewModel.Id, foundUser.Id);

        }

        [Test]
        public void TestReadPersonFromCsv_GetSecondPersonAdded_GuestReturned()
        {

            var guestViewModel = new WeddingPlanning.Models.GuestViewModel
            {
                FirstName = "bar",
                Surname = "baram",
                IsComing = true,
                Allergies = "none, guv"
            };

            var firstName = "foo";
            var surname = "bar";
            var id = Guid.NewGuid();
            var csvStorer = new WeddingPlanning.StuffStorage.CSVStorer();
            csvStorer.StoreGuest(guestViewModel);

            var guestViewModel2 = new WeddingPlanning.Models.GuestViewModel
            {
                Id = id,
                FirstName = firstName,
                Surname = surname,
                IsComing = true,
                AddedBy = guestViewModel.AddedBy,
                Allergies = "Some"
            };


            csvStorer.StoreGuest(guestViewModel2);

            var foundUser = csvStorer.GetGuest(firstName, surname).Result;

            Assert.AreEqual(id, foundUser.Id);
        }

        [Test]
        public void TestReadPersonFromCsv_CsvDoesntExist_ReturnsNull()
        {
            var csvStorer = new WeddingPlanning.StuffStorage.CSVStorer();
            var foundUser = csvStorer.GetGuest("no users exist", "so these don't matter");
            Assert.IsNull(foundUser.Result);
        }

        [Test]
        public void TestReadPersonFromCsv_PersonNotInCsv_ReturnsNull()
        {
            var csvStorer = new WeddingPlanning.StuffStorage.CSVStorer();

            var firstName = "foo";
            var surname = "bar";
            var guestViewModel = new WeddingPlanning.Models.GuestViewModel
            {
                FirstName = firstName,
                Surname = surname,
                IsComing = true,
                Allergies = "Some"
            };

            var foundUser = csvStorer.GetGuest("This is not the user", "You're looking for");
            Assert.IsNull(foundUser.Result);
        }

        //[Test]
        //public void TestDeleteGuestFromCsv_GuestIsOnlyPersonInCsv_EmptiesCsvFile()
        //{
        //    var firstName = "foo";
        //    var surname = "bar";
        //    var guestViewModel = new WeddingPlanning.Models.GuestViewModel
        //    {
        //        FirstName = firstName,
        //        Surname = surname,
        //        IsComing = true,
        //        Allergies = "Some"
        //    };

        //    var csvStorer = new WeddingPlanning.StuffStorage.CSVStorer();

        //    var id = csvStorer.StoreGuest(guestViewModel);

        //    csvStorer.RemoveGuest(guestViewModel);


        //    using (StreamReader reader = new StreamReader(csvStorer._Location))
        //    {
        //        var file = reader.ReadToEnd();

        //        Assert.That(file.Trim().Length == 0);
        //    }
        //}
    }
}