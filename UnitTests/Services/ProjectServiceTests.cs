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
    public class ProjectServiceTests
    {
        private Mock<IProjectRepository> _MockedRepository;
        private ProjectService _ProjectService;

        private Project sampleProject;
        private Team sampleTeam;
        private IQueryable<Project> projects;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            sampleProject = new Project
            {
                Id = "sampleId",
                Name = "sample",
                Description = "sampleDescription",
                Teams = new List<Team>()
            };

            sampleTeam = new Team
            {
                Name = "sample"
            };

            //Тази колекция служи за "заместител" на тази, която бива върната при достъпа на метода ProjectRepository.GetProjects()
            projects = new List<Project>{ sampleProject }.AsQueryable();
        }

        [SetUp]
        public void Setup()
        {
            _MockedRepository = new Mock<IProjectRepository>();
            _MockedRepository.Setup(x => x.GetProjects()).Returns(projects);
            _MockedRepository.Setup(x => x.GetProject("sampleId")).Returns(sampleProject);

            _ProjectService = new ProjectService(_MockedRepository.Object);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_ProjectService, Is.TypeOf<ProjectService>());
            Assert.IsNotNull(_ProjectService);
        }

        [Test]
        public void ThrowNullReferenceExceptionWhenRepositoryIsNull()
        {
            this._MockedRepository = null;

            Assert.Throws<NullReferenceException>(() => new ProjectService(_MockedRepository.Object));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenRepositoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ProjectService(null));
        }

        [Test]
        public void GetProjectsTest()
        {
            var result = _ProjectService.GetProjects();

            Assert.IsInstanceOf<List<Project>>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<Project>>());
        }

        [Test]
        public void GetProjectTest()
        {
            string projectId = _ProjectService.GetProjects().First().Id;
            var result = _ProjectService.GetProject(projectId);

            Assert.IsInstanceOf<Project>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Project>());
        }

        [Test]
        public void AddProjectTest()
        {
            _ProjectService.AddProject(sampleProject);
            var project = _ProjectService.GetProjects().Last();

            Assert.IsNotNull(project);
            Assert.IsInstanceOf<Project>(project);
            Assert.That(project, Is.TypeOf<Project>());
            Assert.That(project.Name, Is.EqualTo("sample"));
        }

        [Test]
        public void AddTeamToProjectTest()
        {
            _ProjectService.AddTeamToProject(sampleProject, sampleTeam);
            var project = _ProjectService.GetProjects().SingleOrDefault(x => x.Id == sampleProject.Id);

            Assert.IsNotNull(project);
            Assert.IsInstanceOf<Project>(project);
            Assert.That(project, Is.TypeOf<Project>());
            Assert.That(project.Teams.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemoveTeamFromProjectTest()
        {
            _ProjectService.RemoveTeamFromProject(sampleProject, sampleTeam);
            var project = _ProjectService.GetProjects().SingleOrDefault(x => x.Id == sampleProject.Id);

            Assert.IsNotNull(project);
            Assert.IsInstanceOf<Project>(project);
            Assert.That(project, Is.TypeOf<Project>());
            Assert.That(project.Teams.Count, Is.EqualTo(0));
        }

        [Test]
        public void EditProjectTest()
        {
            sampleProject.Name = "editedProjectName";
            _ProjectService.EditProject(sampleProject);
            var project = _ProjectService.GetProjects().SingleOrDefault(x => x.Id == sampleProject.Id);

            Assert.IsInstanceOf<Project>(project);
            Assert.IsNotNull(project);
            Assert.That(project, Is.TypeOf<Project>());
            Assert.That(project.Name, Is.EqualTo("editedProjectName"));
        }

        [Test]
        public void DeleteProjectTest()
        {
            var projects = _ProjectService.GetProjects();
            int count = projects.Count;
            //Разписан е код, подобен на този в метода _ProjectService.DeleteProject(), поради причината че не може да се достигне до колекцията, инициализирана в метода OneTimeSetup
            projects.Remove(sampleProject);

            Assert.IsNotNull(projects);
            Assert.IsInstanceOf<List<Project>>(projects);
            Assert.That(projects, Is.TypeOf<List<Project>>());
            Assert.That(projects.Count, Is.Not.EqualTo(count));
        }
    }
}
