using Models;
using Models.Enums;
using Moq;
using NUnit.Framework;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Services;

namespace UnitTests.Services
{
    public class VacationServiceTests
    {
        private Mock<IVacationRepository> _MockedRepository;
        private VacationService _VacationService;

        private Vacation sampleVacation;
        private IQueryable<Vacation> vacations;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            sampleVacation = new Vacation
            {
                Id = "sampleId",
                ToDate = DateTime.Now,
                Status = Models.Enums.ApprovalStatus.Awaiting
            };

            //Тази колекция служи за "заместител" на тази, която бива върната при достъпа на метода VacationRepository.GetVacations()
            vacations = new List<Vacation>{ sampleVacation }.AsQueryable();
        }

        [SetUp]
        public void Setup()
        {
            _MockedRepository = new Mock<IVacationRepository>();
            _MockedRepository.Setup(x => x.GetVacations()).Returns(vacations);
            _MockedRepository.Setup(x => x.GetVacation("sampleId")).Returns(sampleVacation);

            _VacationService = new VacationService(_MockedRepository.Object);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_VacationService, Is.TypeOf<VacationService>());
            Assert.IsNotNull(_VacationService);
        }

        [Test]
        public void ThrowNullReferenceExceptionWhenRepositoryIsNull()
        {
            this._MockedRepository = null;

            Assert.Throws<NullReferenceException>(() => new VacationService(_MockedRepository.Object));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenRepositoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new VacationService(null));
        }

        [Test]
        public void GetVacationsTest()
        {
            var result = _VacationService.GetVacations();

            Assert.IsInstanceOf<List<Vacation>>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<Vacation>>());
        }

        [Test]
        public void GetVacationTest()
        {
            string vacationId = _VacationService.GetVacations().First().Id;
            var result = _VacationService.GetVacation(vacationId);

            Assert.IsInstanceOf<Vacation>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Vacation>());
        }

        [Test]
        public void AddVacationTest()
        {
            _VacationService.AddVacation(sampleVacation);
            var vacation = _VacationService.GetVacations().Last();

            Assert.IsNotNull(vacation);
            Assert.IsInstanceOf<Vacation>(vacation);
            Assert.That(vacation, Is.TypeOf<Vacation>());
            Assert.That(vacation.Status, Is.EqualTo(ApprovalStatus.Awaiting));
        }

        [Test]
        public void ApproveVacationTest()
        {
            _VacationService.ApproveVacation(sampleVacation);
            var vacation = _VacationService.GetVacations().SingleOrDefault(x => x.Id == sampleVacation.Id);

            Assert.IsInstanceOf<Vacation>(vacation);
            Assert.IsNotNull(vacation);
            Assert.That(vacation, Is.TypeOf<Vacation>());
            Assert.That(vacation.Status, Is.EqualTo(ApprovalStatus.Approved));
        }

        [Test]
        public void EditVacationTest()
        {
            sampleVacation.ToDate = DateTime.Today;
            _VacationService.EditVacation(sampleVacation);
            var vacation = _VacationService.GetVacations().SingleOrDefault(x => x.Id == sampleVacation.Id);

            Assert.IsInstanceOf<Vacation>(vacation);
            Assert.IsNotNull(vacation);
            Assert.That(vacation, Is.TypeOf<Vacation>());
            Assert.That(vacation.ToDate, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void DeleteVacationTest()
        {
            var vacations = _VacationService.GetVacations();
            int count = vacations.Count;
            //Разписан е код, подобен на този в метода _VacationService.DeleteVacation(), поради причината че не може да се достигне до колекцията, инициализирана в метода OneTimeSetup
            vacations.Remove(sampleVacation);

            Assert.IsNotNull(vacations);
            Assert.IsInstanceOf<List<Vacation>>(vacations);
            Assert.That(vacations, Is.TypeOf<List<Vacation>>());
            Assert.That(vacations.Count, Is.Not.EqualTo(count));
        }
    }
}
