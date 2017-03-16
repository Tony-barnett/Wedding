using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeddingPlanning.Models;

namespace WeddingPlanning.StuffStorage
{
    public interface IPasswordMapperRepository
    {
        void AddPasswordMap(Guid id, GuestType guestType);

        GuestType GetGuestTypeFromId(Guid id);

        void ChangePasswordMap(Guid id, GuestType guestType);
    }
}