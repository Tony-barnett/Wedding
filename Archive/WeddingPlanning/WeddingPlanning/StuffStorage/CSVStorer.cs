﻿using Csv;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        internal string _Location { get; }
        internal string _GuestLocation { get { return $"{_Location}\\{_GuestFileName}"; } }
        internal string _GuestFileName { get; }
        internal string _PasswordMapperLocation { get { return $"{_Location}\\{_PasswordMapperFileName}"; } }
        internal string _PasswordMapperFileName { get; }

        /// <summary>
        /// Of the form:
        /// Id,firstName,Surname,Allergies,Coming,Child,Young,AddedBy
        /// </summary>
        internal CsvWriter _GuestWriter { get; set; }

        internal CsvReader _GuestReader { get; set; }

        public CSVStorer()
        {
            _Location = ConfigurationManager.AppSettings["CsvFileLocation"];
            _GuestFileName = "Guest.csv";
            _PasswordMapperFileName = "passwordMapper.csv";

            //_GuestWriter =
        }

        private List<string> SerializePerson(IGuest guest)
        {
            var row = new List<string> { $"{guest.Id}", $"{guest.FirstName}", $"{guest.Surname}", $"{guest.Allergies}", $"{guest.IsComing}", $"{guest.AgeGroup}" };
            if (guest.AddedBy != null)
            {
                row.Add($"{guest.AddedBy}");
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
        public async Task StoreGuest(IGuest guest, Guid? storedBy = null)
        {
            if (guest.Id == null)
            {
                guest.Id = GetNextId();
            }
            else
            {
                // If they already have an Id then they already exist so this is an update.
                await UpdateGuest(guest);
                return;
            }
            if (guest.AddedBy == null)
            {
                // We assume they stored themself
                guest.AddedBy = guest.Id;
            }
            var row = SerializePerson(guest);
            using (_GuestWriter = new CsvWriter(_GuestLocation))
            {
                _GuestWriter.WriteLine(row);
            }
        }

        public async Task<IGuest> GetGuest(string firstName, string surname)
        {
            if (!File.Exists(_GuestLocation)) // We assume that the file only exists if there's something in it...
            {
                return null;
            }
            using (_GuestReader = new CsvReader(_GuestLocation))
            {
                while (!_GuestReader.EndOfStream)
                {
                    var records = _GuestReader.GetLine();

                    if (records.Count >= 7 && records[1].ToLower() == firstName.ToLower() && records[2].ToLower() == surname.ToLower())
                    {
                        return await records.ToGuestViewModel();
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
        public IEnumerable<IGuest> GetGuests(Guid? inserterId = null)
        {
            if (!File.Exists(_GuestLocation)) // We assume that the file only exists if there's something in it...
            {
                yield break;
            }
            using (_GuestReader = new CsvReader(_GuestLocation))
            {
                while (!_GuestReader.EndOfStream)
                {
                    var records = _GuestReader.GetLine();
                    var addedById = records.Count > 7 ? Guid.Parse(records[7]) : (Guid?)null;
                    var id = Guid.Parse(records[0]);
                    yield return records.ToGuestViewModel().Result;
                }
            }
        }


        internal IEnumerable<List<string>> GetAllRecords()
        {
            using (_GuestReader = new CsvReader(_GuestLocation))
            {
                while (!_GuestReader.EndOfStream)
                {
                    yield return _GuestReader.GetLine();
                }
            }
        }

        internal void WriteAllRecords(IEnumerable<List<string>> records)
        {
            using (_GuestWriter = new CsvWriter(_GuestLocation, false))
            {
                foreach (var record in records)
                {
                    _GuestWriter.WriteLine(record);
                }
            }
        }

        public async Task RemoveGuest(IGuest guest)
        {
            var records = GetAllRecords().ToList();

            records.Remove(records.First(x => x[0] == guest.Id.ToString()));

            WriteAllRecords(records);
        }

        public async Task UpdateGuest(IGuest guest)
        {
            var records = GetAllRecords().ToList();
            var outputRecords = new List<List<string>>();

            foreach(var record in records)
            {
                var row = record;
                var storedGuest = await record.ToGuestViewModel();
                if(storedGuest.Id == guest.Id)
                {
                    row = SerializePerson(guest);
                }
                outputRecords.Add(row);
            }

            WriteAllRecords(outputRecords);
        }

        public async Task<IGuest> GetGuest(Guid guestId)
        {
            using (_GuestReader = new CsvReader(_GuestLocation))
            {
                while (!_GuestReader.EndOfStream)
                {
                    var line = _GuestReader.GetLine();
                    if (guestId.ToString() == line[0])
                    {
                        return await line.ToGuestViewModel();
                    }
                }
            }

            return null;
        }

        public void AddPasswordMap(Guid id, GuestType guestType)
        {
            using (var cw = new CsvWriter(_PasswordMapperLocation, true))
            {
                cw.WriteLine(new List<string> { id.ToString(), guestType.ToString() });
            }
        }

        public GuestType GetGuestTypeFromId(Guid id)
        {
            using (var cr = new CsvReader(_PasswordMapperLocation))
            {
                var line = new List<string>();

                while (line != null)
                {
                    line = cr.GetLine();

                    Guid fileId;
                    if (Guid.TryParse(line[0], out fileId) && id == fileId) {
                        return (GuestType)Enum.Parse(typeof(GuestType), line[1], true);
                    }
                }
            }
            return GuestType.None;
        }

        public void ChangePasswordMap(Guid id, GuestType guestType)
        {
            throw new NotImplementedException();
        }
    }

    internal static class GuestViewModelParser
    {
        public static async Task<IGuest> ToGuestViewModel(this List<string> record)
        {
            return new GuestViewModel
            {
                Id = Guid.Parse(record[0]),
                FirstName = record[1],
                Surname = record[2],
                Allergies = record[3],
                IsComing = bool.Parse(record[4]),
                AgeGroup = (AgeGroup)Enum.Parse(typeof(AgeGroup), record[5]),
                AddedBy = record.Count >= 7 ? Guid.Parse(record[6]) : (Guid?)null
            };
        }
    }
}
