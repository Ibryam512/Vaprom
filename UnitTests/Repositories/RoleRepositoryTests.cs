using DataAccess;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    public class RoleRepositoryTests
    {
        private VacationManagerDbContext _Context;
        private RoleRepository _RoleRepository;

        private Role sampleRole =>
            new Role
            {
                Name = "sample"
            };

        private Role anotherRole =>
            new Role
            {
                Name = "another"
            };
        
        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VacationManagerDbContext>();
            optionsBuilder.UseInMemoryDatabase("VacationManager");
            _Context = new VacationManagerDbContext(optionsBuilder.Options);
            _RoleRepository = new RoleRepository(_Context);
            if (!_Context.Roles.Contains(sampleRole)) 
            {
                _Context.Roles.Add(sampleRole);
                _Context.SaveChanges();
            }
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_RoleRepository, Is.TypeOf<RoleRepository>());
            Assert.IsNotNull(_RoleRepository);
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RoleRepository(null));
        }

        [Test]
        public void GetRolesTest()
        {
            var result = _RoleRepository.GetRoles().ToList();

            Assert.IsInstanceOf<List<Role>>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<Role>>());
        }

        [Test]
        public void GetRoleTest()
        {
            string roleName = _Context.Roles.First().Name;
            var result = _RoleRepository.GetRole(roleName);

            Assert.IsInstanceOf<Role>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Role>());
        }

        [Test]
        public void AddUserTest()
        {
            _RoleRepository.AddRole(anotherRole);
            var role = _Context.Roles.Last();

            Assert.IsNotNull(role);
            Assert.IsInstanceOf<Role>(role);
            Assert.That(role, Is.TypeOf<Role>());
            Assert.That(role.Name, Is.EqualTo("another"));
        }

        [Test]
        public void DeleteRoleTest()
        {
            int count = _Context.Roles.ToList().Count;
            var role = _Context.Roles.First();
            _RoleRepository.DeleteRole(role);
            var roles = _Context.Roles.ToList();

            Assert.IsNotNull(roles);
            Assert.IsInstanceOf<List<Role>>(roles);
            Assert.That(roles, Is.TypeOf<List<Role>>());
            Assert.That(roles.Count, Is.Not.EqualTo(count));
        }
    }
}