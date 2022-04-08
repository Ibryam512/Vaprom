using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Interfaces;
using Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Services 
{
    public class VacationService : IVacationService
    {
        private readonly IVacationRepository _vacationRepository;

        public VacationService(IVacationRepository vacationRepository)
        {
            this._vacationRepository = vacationRepository ?? throw new ArgumentNullException(nameof(vacationRepository));
        }

        public List<Vacation> GetVacations() => this._vacationRepository.GetVacations().Include(x => x.Applicant).ToList();

        public Vacation GetVacation(string id) => GetVacations().Find(x => x.Id == id);

        public void AddVacation(Vacation vacation) => this._vacationRepository.AddVacation(vacation);

        public void ApproveVacation(Vacation vacation)
        {
            vacation.Status = Models.Enums.ApprovalStatus.Approved;
            this._vacationRepository.EditVacation(vacation);
        }

        public void EditVacation(Vacation vacation) => this._vacationRepository.EditVacation(vacation);

        public void DeleteVacation(Vacation vacation) => this._vacationRepository.DeleteVacation(vacation);
    }
}