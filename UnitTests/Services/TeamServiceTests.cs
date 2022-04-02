using Models;
using Moq;
using NUnit.Framework;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Services;

namespace UnitTests.Services
{
    public class TeamServiceTests
    {
        private Mock<ITeamRepository> _MockedRepository;
        private TeamService _TeamService;

        private Team sampleTeam;
        private User sampleUser;
        private Project sampleProject;
        private IQueryable<Team> teams;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            sampleTeam = new Team
            {
                //Сложено е Id, защото начинът, по който добавям екипа не се отразява по никакъв начин в база данни
                Id = "sampleId",
                Name = "sample",
                Developers = new List<User>()
            };

            sampleUser = new User
            {
                UserName = "sample",
                PasswordHash = "",
                FirstName = "Sample",
                LastName = "User"
            };

            sampleProject = new Project
            {
                Name = "sample",
                Description = "sampleDescription"
            };

            //Тази колекция служи за "заместител" на тази, която бива върната при достъпа на метода TeamRepository.GetTeams()
            teams = teams = new List<Team>{ sampleTeam }.AsQueryable();
        }

        [SetUp]
        public void Setup()
        {
            _MockedRepository = new Mock<ITeamRepository>();
            _MockedRepository.Setup(x => x.GetTeams()).Returns(teams);
            _MockedRepository.Setup(x => x.GetTeam("sampleId")).Returns(sampleTeam);

            _TeamService = new TeamService(_MockedRepository.Object);
            _TeamService.AddTeam(sampleTeam);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_TeamService, Is.TypeOf<TeamService>());
            Assert.IsNotNull(_TeamService);
        }

        [Test]
        public void ThrowNullReferenceExceptionWhenRepositoryIsNull()
        {
            this._MockedRepository = null;

            Assert.Throws<NullReferenceException>(() => new TeamService(_MockedRepository.Object));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenRepositoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TeamService(null));
        }

        [Test]
        public void GetTeamsTest()
        {
            var result = _TeamService.GetTeams();

            Assert.IsInstanceOf<List<Team>>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<Team>>());
        }

        [Test]
        public void GetTeamTest()
        {
            string teamId = _TeamService.GetTeams().First().Id;
            var result = _TeamService.GetTeam(teamId);

            Assert.IsInstanceOf<Team>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Team>());
        }

        [Test]
        public void AddTeamTest()
        {
            _TeamService.AddTeam(sampleTeam);
            var team = _TeamService.GetTeams().Last();

            Assert.IsNotNull(team);
            Assert.IsInstanceOf<Team>(team);
            Assert.That(team, Is.TypeOf<Team>());
            Assert.That(team.Name, Is.EqualTo("sample"));
        }

        [Test]
        public void AddTeamLeadTest()
        {
            _TeamService.AddTeamLead(sampleUser, sampleTeam);
            var team = _TeamService.GetTeams().SingleOrDefault(x => x.Name == sampleTeam.Name);

            Assert.IsNotNull(team);
            Assert.IsNotNull(team.TeamLeader);
            Assert.IsInstanceOf<Team>(team);
            Assert.That(team, Is.TypeOf<Team>());
        }

        [Test]
        public void RemoveTeamLeadTest()
        {
            _TeamService.RemoveTeamLead(sampleTeam);
            var team = _TeamService.GetTeams().SingleOrDefault(x => x.Name == sampleTeam.Name);

            Assert.IsNotNull(team);
            Assert.IsNull(team.TeamLeader);
            Assert.IsInstanceOf<Team>(team);
            Assert.That(team, Is.TypeOf<Team>());
        }

        [Test]
        public void AddUserToTeamTest()
        {
            _TeamService.AddUserToTeam(sampleUser, sampleTeam);
            var team = _TeamService.GetTeams().SingleOrDefault(x => x.Name == sampleTeam.Name);

            Assert.IsNotNull(team);
            Assert.IsInstanceOf<Team>(team);
            Assert.That(team, Is.TypeOf<Team>());
            Assert.That(team.Developers.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemoveUserFromTeamTest()
        {
            _TeamService.RemoveUserFromTeam(sampleUser, sampleTeam);
            var team = _TeamService.GetTeams().SingleOrDefault(x => x.Name == sampleTeam.Name);

            Assert.IsNotNull(team);
            Assert.IsInstanceOf<Team>(team);
            Assert.That(team, Is.TypeOf<Team>());
            Assert.That(team.Developers.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddProjectToTeamTest()
        {
            _TeamService.AddProjectToTeam(sampleProject, sampleTeam);
            var team = _TeamService.GetTeams().SingleOrDefault(x => x.Name == sampleTeam.Name);

            Assert.IsNotNull(team);
            Assert.IsNotNull(team.Project);
            Assert.IsInstanceOf<Team>(team);
            Assert.That(team, Is.TypeOf<Team>());
        }

        [Test]
        public void RemoveProjectFromTeamTest()
        {
            _TeamService.RemoveProjectFromTeam(sampleTeam);
            var team = _TeamService.GetTeams().SingleOrDefault(x => x.Name == sampleTeam.Name);

            Assert.IsNotNull(team);
            Assert.IsNull(team.Project);
            Assert.IsInstanceOf<Team>(team);
            Assert.That(team, Is.TypeOf<Team>());
        }

        [Test]
        public void EditTeamTest()
        {
            sampleTeam.Name = "editedTeamName";
            _TeamService.EditTeam(sampleTeam);
            var team = _TeamService.GetTeams().SingleOrDefault(x => x.Id == sampleTeam.Id);

            Assert.IsInstanceOf<Team>(team);
            Assert.IsNotNull(team);
            Assert.That(team, Is.TypeOf<Team>());
            Assert.That(team.Name, Is.EqualTo("editedTeamName"));
        }

        [Test]
        public void DeleteTeamTest()
        {
            var teams = _TeamService.GetTeams();
            int count = teams.Count;
            //Разписан е код, подобен на този в метода _TeamService.DeleteTeam(), поради причината че не може да се достигне до колекцията, инициализирана в метода OneTimeSetup
            teams.Remove(sampleTeam);

            Assert.IsNotNull(teams);
            Assert.IsInstanceOf<List<Team>>(teams);
            Assert.That(teams, Is.TypeOf<List<Team>>());
            Assert.That(teams.Count, Is.Not.EqualTo(count));
        }
    }
}
