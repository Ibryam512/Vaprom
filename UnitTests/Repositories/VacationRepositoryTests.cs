using DataAccess;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Models;
using Models.Enums;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.Repositories
{
    public class VacationRepositoryTests
    {
        private VacationManagerDbContext _Context;
        private VacationRepository _VacationRepository;

        private Vacation sampleVacation =>
            new Vacation
            {
                VacationType = VacationType.Paid,
                FromDate = new DateTime(),
                ToDate = new DateTime(),
                CreationDate = new DateTime(),
                IsHalfDay = false,
                Status = ApprovalStatus.Awaiting
            };

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VacationManagerDbContext>();
            optionsBuilder.UseInMemoryDatabase("VacationManager");
            _Context = new VacationManagerDbContext(optionsBuilder.Options);
            _VacationRepository = new VacationRepository(_Context);
            _Context.Vacations.Add(sampleVacation);
            _Context.SaveChanges();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_VacationRepository, Is.TypeOf<VacationRepository>());
            Assert.IsNotNull(_VacationRepository);
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new VacationRepository(null));
        }

        [Test]
        public void GetVacationsTest()
        {
            var result = _VacationRepository.GetVacations().ToList();

            Assert.IsInstanceOf<List<Vacation>>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<Vacation>>());
        }

        [Test]
        public void GetVacationTest()
        {
            string VacationId = _Context.Vacations.First().Id;
            var result = _VacationRepository.GetVacation(VacationId);

            Assert.IsInstanceOf<Vacation>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Vacation>());
        }

        [Test]
        public void AddVacationTest()
        {
            _VacationRepository.AddVacation(sampleVacation);
            var Vacation = _Context.Vacations.Last();

            Assert.IsNotNull(Vacation);
            Assert.IsInstanceOf<Vacation>(Vacation);
            Assert.That(Vacation, Is.TypeOf<Vacation>());
            Assert.That(Vacation.VacationType, Is.EqualTo(VacationType.Paid));
        }

        [Test]
        public void EditVacationTest()
        {
            var count = _Context.Vacations.ToList().Count;
            var Vacation = _Context.Vacations.First();
            Vacation.Status = ApprovalStatus.Approved;
            _VacationRepository.EditVacation(Vacation);
            var editedVacation = _Context.Vacations.First();
            var Vacations = _Context.Vacations.ToList();

            Assert.IsNotNull(editedVacation);
            Assert.IsInstanceOf<Vacation>(editedVacation);
            Assert.That(editedVacation, Is.TypeOf<Vacation>());
            Assert.That(editedVacation.Status, Is.EqualTo(ApprovalStatus.Approved));
            Assert.That(Vacations.Count, Is.EqualTo(count));
        }

        [Test]
        public void DeleteVacationTest()
        {
            int count = _Context.Vacations.ToList().Count;
            var Vacation = _Context.Vacations.First();
            _VacationRepository.DeleteVacation(Vacation);
            var Vacations = _Context.Vacations.ToList();

            Assert.IsNotNull(Vacations);
            Assert.IsInstanceOf<List<Vacation>>(Vacations);
            Assert.That(Vacations, Is.TypeOf<List<Vacation>>());
            Assert.That(Vacations.Count, Is.Not.EqualTo(count));
        }
    }
}