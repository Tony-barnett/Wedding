using System;
using System.Collections.Generic;
using System.Linq;

namespace WeddingWebsite.Models
{
    public enum GuestType
    {
        None = 0,
        Day = 1,
        Evening = 2
    }

    public class GuestModel
    {
        Guid GuestId { get; set; }
        GuestType GuestType { get; set; }
    }
}