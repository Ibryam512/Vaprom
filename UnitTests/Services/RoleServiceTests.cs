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
    public class RoleServiceTests
    {
        private Mock<IRoleRepository> _MockedRepository;
        private RoleService _RoleService;

        private Role sampleRole;
        private IQueryable<Role> roles;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            sampleRole = new Role
            {
                Id = "sampleId",
                Name = "role"
            };

            //Тази колекция служи за "заместител" на тази, която бива върната при достъпа на метода RoleRepository.GetRoles()
            roles = new List<Role>{ sampleRole }.AsQueryable();
        }

        [SetUp]
        public void Setup()
        {
            _MockedRepository = new Mock<IRoleRepository>();
            _MockedRepository.Setup(x => x.GetRoles()).Returns(roles);
            _MockedRepository.Setup(x => x.GetRole("sampleId")).Returns(sampleRole);

            _RoleService = new RoleService(_MockedRepository.Object);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_RoleService, Is.TypeOf<RoleService>());
            Assert.IsNotNull(_RoleService);
        }

        [Test]
        public void ThrowNullReferenceExceptionWhenRepositoryIsNull()
        {
            this._MockedRepository = null;

            Assert.Throws<NullReferenceException>(() => new RoleService(_MockedRepository.Object));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenRepositoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RoleService(null));
        }

        [Test]
        public void GetRolesTest()
        {
            var result = _RoleService.GetRoles();

            Assert.IsInstanceOf<List<Role>>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<Role>>());
        }

        [Test]
        public void GetRoleTest()
        {
            string roleId = _RoleService.GetRoles().First().Id;
            var result = _RoleService.GetRole(roleId);

            Assert.IsInstanceOf<Role>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Role>());
        }

        [Test]
        public void AddRoleTest()
        {
            _RoleService.AddRole(sampleRole);
            var role = _RoleService.GetRoles().Last();

            Assert.IsNotNull(role);
            Assert.IsInstanceOf<Role>(role);
            Assert.That(role, Is.TypeOf<Role>());
            Assert.That(role.Name, Is.EqualTo("role"));
        }

        [Test]
        public void EditRoleTest()
        {
            sampleRole.Name = "editedRoleName";
            _RoleService.EditRole(sampleRole);
            var role = _RoleService.GetRoles().SingleOrDefault(x => x.Id == sampleRole.Id);

            Assert.IsInstanceOf<Role>(role);
            Assert.IsNotNull(role);
            Assert.That(role, Is.TypeOf<Role>());
            Assert.That(role.Name, Is.EqualTo("editedRoleName"));
        }

        [Test]
        public void DeleteRoleTest()
        {
            var roles = _RoleService.GetRoles();
            int count = roles.Count;
            //Разписан е код, подобен на този в метода _RoleService.DeleteRole(), поради причината че не може да се достигне до колекцията, инициализирана в метода OneTimeSetup
            roles.Remove(sampleRole);

            Assert.IsNotNull(roles);
            Assert.IsInstanceOf<List<Role>>(roles);
            Assert.That(roles, Is.TypeOf<List<Role>>());
            Assert.That(roles.Count, Is.Not.EqualTo(count));
        }
    }
}
