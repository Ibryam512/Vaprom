using DataAccess;
using Models;
using Repositories.Interfaces;
using System;
using System.Linq;

namespace Repositories
{
    public class VacationRepository : IVacationRepository
    {
        private readonly VacationManagerDbContext _context;

        public VacationRepository(VacationManagerDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Vacation> GetVacations() => this._context.Vacations.AsQueryable();

        public Vacation GetVacation(string name) => this._context.Vacations.Find(name);

        public void AddVacation(Vacation vacation)
        {
            this._context.Vacations.Add(vacation);
            this._context.SaveChanges();
        }

        public void EditVacation(Vacation vacation)
        {
            this._context.Vacations.Update(vacation);
            this._context.SaveChanges();
        }

        public void DeleteVacation(Vacation vacation)
        {
            this._context.Vacations.Remove(vacation);
            this._context.SaveChanges();
        }
    }
}
