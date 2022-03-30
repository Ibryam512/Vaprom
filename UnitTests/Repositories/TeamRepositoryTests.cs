using DataAccess;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.Repositories
{
    public class TeamRepositoryTests
    {
        private VacationManagerDbContext _Context;
        private TeamRepository _TeamRepository;

        private Team sampleTeam =>
            new Team
            {
                Name = "sample"
            };

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VacationManagerDbContext>();
            optionsBuilder.UseInMemoryDatabase("VacationManager");
            _Context = new VacationManagerDbContext(optionsBuilder.Options);
            _TeamRepository = new TeamRepository(_Context);
            _Context.Teams.Add(sampleTeam);
            _Context.SaveChanges();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_TeamRepository, Is.TypeOf<TeamRepository>());
            Assert.IsNotNull(_TeamRepository);
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TeamRepository(null));
        }

        [Test]
        public void GetTeamsTest()
        {
            var result = _TeamRepository.GetTeams().ToList();

            Assert.IsInstanceOf<List<Team>>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<Team>>());
        }

        [Test]
        public void GetTeamTest()
        {
            string TeamId = _Context.Teams.First().Id;
            var result = _TeamRepository.GetTeam(TeamId);

            Assert.IsInstanceOf<Team>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Team>());
        }

        [Test]
        public void AddTeamTest()
        {
            _TeamRepository.AddTeam(sampleTeam);
            var Team = _Context.Teams.Last();

            Assert.IsNotNull(Team);
            Assert.IsInstanceOf<Team>(Team);
            Assert.That(Team, Is.TypeOf<Team>());
            Assert.That(Team.Name, Is.EqualTo("sample"));
        }

        [Test]
        public void EditTeamTest()
        {
            var count = _Context.Teams.ToList().Count;
            var Team = _Context.Teams.First();
            Team.Name = "editedTeamName";
            _TeamRepository.EditTeam(Team);
            var editedTeam = _Context.Teams.First();
            var Teams = _Context.Teams.ToList();

            Assert.IsNotNull(editedTeam);
            Assert.IsInstanceOf<Team>(editedTeam);
            Assert.That(editedTeam, Is.TypeOf<Team>());
            Assert.That(editedTeam.Name, Is.EqualTo("editedTeamName"));
            Assert.That(Teams.Count, Is.EqualTo(count));
        }

        [Test]
        public void DeleteTeamTest()
        {
            int count = _Context.Teams.ToList().Count;
            var Team = _Context.Teams.First();
            _TeamRepository.DeleteTeam(Team);
            var Teams = _Context.Teams.ToList();

            Assert.IsNotNull(Teams);
            Assert.IsInstanceOf<List<Team>>(Teams);
            Assert.That(Teams, Is.TypeOf<List<Team>>());
            Assert.That(Teams.Count, Is.Not.EqualTo(count));
        }
    }
}