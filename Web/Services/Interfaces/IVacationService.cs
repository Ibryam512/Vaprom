using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Services.Interfaces
{
    public interface IVacationService
    {
        List<Vacation> GetVacations();
        Vacation GetVacation(string id);
        void AddVacation(Vacation vacation);
        void ApproveVacation(Vacation vacation);
        void EditVacation(Vacation vacation);
        void DeleteVacation(Vacation vacation);
    }
}