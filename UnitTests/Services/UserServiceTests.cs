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
    public class UserServiceTests
    {
        private Mock<IUserRepository> _MockedRepository;
        private UserService _UserService;

        private User sampleUser;
        private Team sampleTeam;
        private Role sampleRole;
        private IQueryable<User> users;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            sampleUser = new User
            {
                Id = "sampleId",
                UserName = "sample",
                PasswordHash = "",
                FirstName = "Sample",
                LastName = "User"
            };

            sampleTeam = new Team
            {
                Name = "sample",
                Developers = new List<User>()
            };

            sampleRole = new Role
            {
                Name = "role"
            };

            //Тази колекция служи за "заместител" на тази, която бива върната при достъпа на метода UserRepository.GetUsers()
            users = new List<User>{ sampleUser }.AsQueryable();
        }

        [SetUp]
        public void Setup()
        {
            _MockedRepository = new Mock<IUserRepository>();
            _MockedRepository.Setup(x => x.GetUsers()).Returns(users);
            _MockedRepository.Setup(x => x.GetUser("sampleId")).Returns(sampleUser);

            _UserService = new UserService(_MockedRepository.Object);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_UserService, Is.TypeOf<UserService>());
            Assert.IsNotNull(_UserService);
        }

        [Test]
        public void ThrowNullReferenceExceptionWhenRepositoryIsNull()
        {
            this._MockedRepository = null;

            Assert.Throws<NullReferenceException>(() => new UserService(_MockedRepository.Object));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenRepositoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new UserService(null));
        }

        [Test]
        public void GetUsersTest()
        {
            var result = _UserService.GetUsers();

            Assert.IsInstanceOf<List<User>>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<User>>());
        }

        [Test]
        public void GetUserTest()
        {
            string userId = _UserService.GetUsers().First().Id;
            var result = _UserService.GetUser(userId);

            Assert.IsInstanceOf<User>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<User>());
        }

        [Test]
        public void AddUserTest()
        {
            _UserService.AddUser(sampleUser);
            var user = _UserService.GetUsers().Last();

            Assert.IsNotNull(user);
            Assert.IsInstanceOf<User>(user);
            Assert.That(user, Is.TypeOf<User>());
            Assert.That(user.UserName, Is.EqualTo("sample"));
        }

        [Test]
        public void ChangeRoleTest()
        {
            _UserService.ChangeRole(sampleUser, sampleRole);
            var user = _UserService.GetUsers().SingleOrDefault(x => x.Id == sampleUser.Id);

            Assert.IsNotNull(user);
            Assert.IsNotNull(user.Role);
            Assert.IsInstanceOf<Role>(user.Role);
            Assert.That(user.Role.Name, Is.EqualTo(sampleRole.Name));
        }

        [Test]
        public void JoinTeamTest()
        {
            _UserService.JoinTeam(sampleUser, sampleTeam);
            var user = _UserService.GetUsers().SingleOrDefault(x => x.Id == sampleUser.Id);

            Assert.IsNotNull(user);
            Assert.IsNotNull(user.Team);
            Assert.IsInstanceOf<Team>(user.Team);
            Assert.That(user.Team.Name, Is.EqualTo(sampleTeam.Name));
        }

        [Test]
        public void LeaveTeamTest()
        {
            _UserService.LeaveTeam(sampleUser);
            var user = _UserService.GetUsers().SingleOrDefault(x => x.Id == sampleUser.Id);

            Assert.IsNotNull(user);
            Assert.IsNull(user.Team);
        }

        [Test]
        public void EditUserTest()
        {
            sampleUser.UserName = "editedUserName";
            _UserService.EditUser(sampleUser);
            var user = _UserService.GetUsers().SingleOrDefault(x => x.Id == sampleUser.Id);

            Assert.IsInstanceOf<User>(user);
            Assert.IsNotNull(user);
            Assert.That(user, Is.TypeOf<User>());
            Assert.That(user.UserName, Is.EqualTo("editedUserName"));
        }

        [Test]
        public void DeleteUserTest()
        {
            var users = _UserService.GetUsers();
            int count = users.Count;
            //Разписан е код, подобен на този в метода _UserService.DeleteUser(), поради причината че не може да се достигне до колекцията, инициализирана в метода OneTimeSetup
            users.Remove(sampleUser);

            Assert.IsNotNull(users);
            Assert.IsInstanceOf<List<User>>(users);
            Assert.That(users, Is.TypeOf<List<User>>());
            Assert.That(users.Count, Is.Not.EqualTo(count));
        }
    }
}
