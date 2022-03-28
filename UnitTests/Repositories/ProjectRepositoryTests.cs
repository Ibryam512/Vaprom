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
    public class ProjectRepositoryTests
    {
        private VacationManagerDbContext _Context;
        private ProjectRepository _ProjectRepository;

        private Project sampleProject =>
            new Project
            {
                Name = "sample",
                Description = "sampleDescription"
            };

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VacationManagerDbContext>();
            optionsBuilder.UseInMemoryDatabase("VacationManager");
            _Context = new VacationManagerDbContext(optionsBuilder.Options);
            _ProjectRepository = new ProjectRepository(_Context);
            _Context.Projects.Add(sampleProject);
            _Context.SaveChanges();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_ProjectRepository, Is.TypeOf<ProjectRepository>());
            Assert.IsNotNull(_ProjectRepository);
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ProjectRepository(null));
        }

        [Test]
        public void GetProjectsTest()
        {
            var result = _ProjectRepository.GetProjects().ToList();

            Assert.IsInstanceOf<List<Project>>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<Project>>());
        }

        [Test]
        public void GetProjectTest()
        {
            string ProjectId = _Context.Projects.First().Id;
            var result = _ProjectRepository.GetProject(ProjectId);

            Assert.IsInstanceOf<Project>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Project>());
        }

        [Test]
        public void AddProjectTest()
        {
            _ProjectRepository.AddProject(sampleProject);
            var Project = _Context.Projects.Last();

            Assert.IsNotNull(Project);
            Assert.IsInstanceOf<Project>(Project);
            Assert.That(Project, Is.TypeOf<Project>());
            Assert.That(Project.Name, Is.EqualTo("sample"));
        }

        [Test]
        public void EditProjectTest()
        {
            var count = _Context.Projects.ToList().Count;
            var Project = _Context.Projects.First();
            Project.Name = "editedProjectName";
            _ProjectRepository.EditProject(Project);
            var editedProject = _Context.Projects.First();
            var Projects = _Context.Projects.ToList();

            Assert.IsNotNull(editedProject);
            Assert.IsInstanceOf<Project>(editedProject);
            Assert.That(editedProject, Is.TypeOf<Project>());
            Assert.That(editedProject.Name, Is.EqualTo("editedProjectName"));
            Assert.That(Projects.Count, Is.EqualTo(count));
        }

        [Test]
        public void DeleteProjectTest()
        {
            int count = _Context.Projects.ToList().Count;
            var Project = _Context.Projects.First();
            _ProjectRepository.DeleteProject(Project);
            var Projects = _Context.Projects.ToList();

            Assert.IsNotNull(Projects);
            Assert.IsInstanceOf<List<Project>>(Projects);
            Assert.That(Projects, Is.TypeOf<List<Project>>());
            Assert.That(Projects.Count, Is.Not.EqualTo(count));
        }
    }
}