using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using WeddingPlanning.Models;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using Csv;

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
            bool IsChild { get; set; }
            bool IsYoungChild { get; set; }
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

        private string SerializeGuestModel(GuestViewModel guest)
        {
            return $"\"{guest.Id}\",\"{guest.FirstName.Replace("\"", "\"\"")}\",\"{guest.Surname.Replace("\"", "\"\"")}\",\"{guest.IsComing}\",false,false,\"{guest.AddedBy}\"";
        }

        private int GetNextId()
        {
            if (!File.Exists(_Location)) {
                return 1;
            }

            using (_GuestReader = new CsvReader(_Location))
            {
                var nextId = 0;
                while (!_GuestReader.EndOfStream)
                {
                    var line = _GuestReader.ReadLine();
                    var id = int.Parse(Regex.Split(line, "\",\"")[0].Substring(1));
                    if (id > nextId)
                    {
                        nextId = id;
                    }
                }

                return ++nextId;
            }
        }

        /// <summary>
        /// we write down the id of the person who stored this guest, i.e. the first person that was stored
        /// </summary>
        /// <param name="guest"></param>
        /// <param name="storedBy"></param>
        /// <returns>The id of the guest who stored this guest.</returns>
        public int StoreGuest(GuestViewModel guest, int? storedBy = null)
        {
            if (guest.Id == null)
            {
                guest.Id = GetNextId();
            }
            var row = SerializeGuestModel(guest);
            using (_GuestWriter = new CsvWriter(_Location))
            {
                _GuestWriter.WriteLine(row);
            }
            return storedBy ?? guest.Id.Value;
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
        public IEnumerable<GuestViewModel> GetGuests(int inserterId)
        {
            throw new NotImplementedException();
        }

        public void StoreChild(ChildrenViewModel child, int StoredBy)
        {
            throw new NotImplementedException();
        }

        public ChildrenViewModel GetChild(string firstName, string surname)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChildrenViewModel> GetChildren(int inserterId)
        {
            throw new NotImplementedException();
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

    internal static class GuestViewModelParser{

        public static GuestViewModel ToGuestViewModel(this List<string> records)
        {
            return new GuestViewModel
            {
                Id = int.Parse(records[0]),
                FirstName = records[1],
                Surname = records[2],
                Allergies = records[3],
                IsComing = bool.Parse(records[4]),
                AddedBy = records.Count >= 8 ? int.Parse(records[7]): (int?)null
            };
        }
    }
}