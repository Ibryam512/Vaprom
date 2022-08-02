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
    public class UserRepositoryTests
    {
        private VacationManagerDbContext _Context;
        private UserRepository _UserRepository;

        private User sampleUser =>
            new User
            {
                UserName = "sample",
                PasswordHash = "",
                FirstName = "Sample",
                LastName = "User"
            };

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VacationManagerDbContext>();
            optionsBuilder.UseInMemoryDatabase("VacationManager");
            _Context = new VacationManagerDbContext(optionsBuilder.Options);
            _UserRepository = new UserRepository(_Context);
            _Context.Users.Add(sampleUser);
            _Context.SaveChanges();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_UserRepository, Is.TypeOf<UserRepository>());
            Assert.IsNotNull(_UserRepository);
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new UserRepository(null));
        }

        [Test]
        public void GetUsersTest()
        {
            var result = _UserRepository.GetUsers().ToList();

            Assert.IsInstanceOf<List<User>>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<User>>());
        }

        [Test]
        public void GetUserTest()
        {
            string userName = _Context.Users.First().UserName;
            var result = _UserRepository.GetUser(userName);

            Assert.IsInstanceOf<User>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<User>());
        }

        [Test]
        public void GetUserByIdTest()
        {
            string userId = _Context.Users.First().Id;
            var result = _UserRepository.GetUserById(userId);

            Assert.IsInstanceOf<User>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<User>());
        }

        [Test]
        public void AddUserTest()
        {
            _UserRepository.AddUser(sampleUser);
            var user = _Context.Users.Last();

            Assert.IsNotNull(user);
            Assert.IsInstanceOf<User>(user);
            Assert.That(user, Is.TypeOf<User>());
            Assert.That(user.UserName, Is.EqualTo("sample"));
        }

        [Test]
        public void EditUserTest()
        {
            var count = _Context.Users.ToList().Count;
            var user = _Context.Users.First();
            user.UserName = "editedUserName";
            _UserRepository.EditUser(user);
            var editedUser = _Context.Users.First();
            var users = _Context.Users.ToList();

            Assert.IsNotNull(editedUser);
            Assert.IsInstanceOf<User>(editedUser);
            Assert.That(editedUser, Is.TypeOf<User>());
            Assert.That(editedUser.UserName, Is.EqualTo("editedUserName"));
            Assert.That(users.Count, Is.EqualTo(count));
        }

        [Test]
        public void DeleteUserTest()
        {
            int count = _Context.Users.ToList().Count;
            var user = _Context.Users.First();
            _UserRepository.DeleteUser(user);
            var users = _Context.Users.ToList();

            Assert.IsNotNull(users);
            Assert.IsInstanceOf<List<User>>(users);
            Assert.That(users, Is.TypeOf<List<User>>());
            Assert.That(users.Count, Is.Not.EqualTo(count));
        }
    }
}