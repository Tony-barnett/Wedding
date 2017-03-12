using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingPlanning.Models
{
    public enum GuestType
    {
        None,
        Day,
        Evening
    }

    internal class GuestModel
    {
        Guid GuestId { get; set; }
        GuestType GuestType { get; set; }
    }
}