using Csv;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using WeddingPlanning.GuestStore;
using WeddingPlanning.Models;

namespace WeddingPlanning.StuffStorage
{
    /// <summary>
    /// Store things in CSV Files.
    /// Crude but useful for before I get the database up and running.
    /// </summary>
    public partial class CSVStorer : IStorageProvider
    {
        private class StoredGuest : GuestViewModel
        {
            private bool IsChild { get; set; }
            private bool IsYoungChild { get; set; }
        }

        internal string _Location { get; set; }

        /// <summary>
        /// Of the form:
        /// Id,firstName,Surname,Allergies,Coming,Child,Young,AddedBy
        /// </summary>
        internal CsvWriter _GuestWriter { get; set; }

        internal CsvReader _GuestReader { get; set; }

        public CSVStorer()
        {
            _Location = ConfigurationManager.AppSettings["CsvFileLocation"] + "\\Guest.csv";
            //_GuestWriter =
        }

        private string SerializePerson(IPerson guest)
        {
            var isChild = guest is IChild && !((IChild)guest).IsBaby;
            var isYoungChild = guest is IChild && ((IChild)guest).IsBaby;

            var row = $"\"{guest.Id}\",\"{guest.FirstName.Replace("\"", "\"\"")}\",\"{guest.Surname.Replace("\"", "\"\"")}\",\"{guest.Allergies.Replace("\"", "\"\"")}\",{guest.IsComing},\"{isChild}\",\"{isYoungChild}\"";
            if (guest.AddedBy != null) {
                row += $",\"{guest.AddedBy}\"";
            }
            return row;
        }

        private Guid GetNextId()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// we write down the id of the person who stored this guest, i.e. the first person that was stored
        /// </summary>
        /// <param name="guest"></param>
        /// <param name="storedBy"></param>
        /// <returns>The id of the guest who stored this guest.</returns>
        public void StoreGuest(GuestViewModel guest, Guid? storedBy = null)
        {
            if (guest.Id == null)
            {
                guest.Id = GetNextId();
            }
            var row = SerializePerson(guest);
            using (_GuestWriter = new CsvWriter(_Location))
            {
                _GuestWriter.WriteLine(row);
            }
        }

        public GuestViewModel GetGuest(string firstName, string surname)
        {
            if (!File.Exists(_Location)) // We assume that the file only exists if there's something in it...
            {
                return null;
            }
            using (_GuestReader = new CsvReader(_Location))
            {
                while (!_GuestReader.EndOfStream)
                {
                    var records = _GuestReader.GetLine();

                    if (records.Count >= 7 && records[1].ToLower() == firstName.ToLower() && records[2].ToLower() == surname.ToLower())
                    {
                        return records.ToGuestViewModel();
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Get all the guests insterted by a specific person. I guess we store the id of the first person who does the inserting
        /// in a given session and call that the inserter?
        /// </summary>
        /// <param name="inserter"></param>
        /// <returns></returns>
        public IEnumerable<GuestViewModel> GetGuests(Guid? inserterId = null)
        {
            if (!File.Exists(_Location)) // We assume that the file only exists if there's something in it...
            {
                yield break;
            }
            using (_GuestReader = new CsvReader(_Location))
            {
                while (!_GuestReader.EndOfStream)
                {
                    var records = _GuestReader.GetLine();
                    var addedById = records.Count > 7? Guid.Parse(records[7]) : (Guid?) null;
                    var id = Guid.Parse(records[0]);
                    var isAdult = !bool.Parse(records[5]) && !bool.Parse(records[6]);

                    if (records.Count >= 7 && isAdult && (inserterId == null || addedById == inserterId || id == inserterId))
                    {
                        yield return records.ToGuestViewModel();
                    }
                }
            }
        }

        public void StoreChild(ChildrenViewModel child, Guid storedBy)
        {
            if (child.Id == null)
            {
                child.Id = GetNextId();
            }
            var row = SerializePerson(child);
            using (_GuestWriter = new CsvWriter(_Location))
            {
                _GuestWriter.WriteLine(row);
            }
        }

        public ChildrenViewModel GetChild(string firstName, string surname)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChildrenViewModel> GetChildren(Guid? inserterId = null)
        {
            if (!File.Exists(_Location)) // We assume that the file only exists if there's something in it...
            {
                yield break;
            }
            using (_GuestReader = new CsvReader(_Location))
            {
                while (!_GuestReader.EndOfStream)
                {
                    var records = _GuestReader.GetLine();
                    var addedById = records.Count > 7 ? Guid.Parse(records[7]) : (Guid?)null;
                    var id = Guid.Parse(records[0]);
                    var isChild = bool.Parse(records[5]) || bool.Parse(records[6]);

                    if (records.Count >= 7 && isChild && (inserterId == null || addedById == inserterId || id == inserterId))
                    {
                        yield return records.ToChildViewModel();
                    }
                }
            }
        }

        public void RemoveGuest(GuestViewModel guest)
        {
            throw new NotImplementedException();
        }

        public void RemoveChild(ChildrenViewModel child)
        {
            throw new NotImplementedException();
        }

        public void UpdateGuest(GuestViewModel guest)
        {
            throw new NotImplementedException();
        }

        public void UpdateChild(ChildrenViewModel child)
        {
            throw new NotImplementedException();
        }
    }

    internal static class GuestViewModelParser
    {
        public static GuestViewModel ToGuestViewModel(this List<string> record)
        {
            return new GuestViewModel
            {
                Id = Guid.Parse(record[0]),
                FirstName = record[1],
                Surname = record[2],
                Allergies = record[3],
                IsComing = bool.Parse(record[4]),
                AddedBy = record.Count >= 8 ? Guid.Parse(record[7]) : (Guid?)null
            };
        }

        public static ChildrenViewModel ToChildViewModel(this List<string> record)
        {
            return new ChildrenViewModel
            {
                Id = Guid.Parse(record[0]),
                FirstName = record[1],
                Surname = record[2],
                Allergies = record[3],
                IsComing = bool.Parse(record[4]),
                IsBaby = bool.Parse(record[6]),
                AddedBy = record.Count >= 8 ? Guid.Parse(record[7]) : (Guid?)null
            };
        }
    }
}