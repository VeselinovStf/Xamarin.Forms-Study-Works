using NUnit.Framework;
using Wired_Brain_Coffee.Services;
using Wired_Brain_UserService;

namespace Shared_Unit_Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        [TestCase]
        public void Create_Registration_When_Correct_Params_Are_Passed()
        {
            var user = new User()
            {
                Id = 1,
                EmailAddress = "email@mail.com",
                FirstName = "FirstName",
                LastName = "LastName",
                Password = "password"
            };

            var dependencyServiceStub = new DependencyServiceStub();
            dependencyServiceStub.Register<IUserApiService>(new UserApiServiceMock());

            IUserService userService = new UserService(dependencyServiceStub);

            UserResponse response = userService.RegisterUser(user);

            Assert.AreEqual(Status.Created, response.EntityState);
        }

        [TestCase("a")]
        [TestCase("abv")]
        [TestCase("abv@")]
        [TestCase("abv@aaa")]
        [TestCase("/abv@aaa")]
        [TestCase("abv@aaa.")]
        public void Fail_Registration_When_Email_Is_Invalid(string email)
        {
            var user = new User()
            {
                Id = 1,
                EmailAddress = email,
                FirstName = "FirstName",
                LastName = "LastName",
                Password = "password"
            };

            var dependencyServiceStub = new DependencyServiceStub();
            dependencyServiceStub.Register<IUserApiService>(new UserApiServiceMock());

            IUserService userService = new UserService(dependencyServiceStub);

            UserResponse response = userService.RegisterUser(user);

            Assert.AreEqual(Status.Invalid, response.EntityState);
        }

        [TestCase("")]
        public void Fail_Registration_When_FirstName_Is_Invalid(string firstName)
        {
            var user = new User()
            {
                Id = 1,
                EmailAddress = "mail@mail.com",
                FirstName = firstName,
                LastName = "LastName",
                Password = "password"
            };

            var dependencyServiceStub = new DependencyServiceStub();
            dependencyServiceStub.Register<IUserApiService>(new UserApiServiceMock());

            IUserService userService = new UserService(dependencyServiceStub);

            UserResponse response = userService.RegisterUser(user);

            Assert.AreEqual(Status.Invalid, response.EntityState);
        }

        [TestCase("")]
        public void Fail_Registration_When_LastName_Is_Invalid(string lastName)
        {
            var user = new User()
            {
                Id = 1,
                EmailAddress = "mail@mail.com",
                FirstName = "FirstName",
                LastName = lastName,
                Password = "password"
            };

            var dependencyServiceStub = new DependencyServiceStub();
            dependencyServiceStub.Register<IUserApiService>(new UserApiServiceMock());

            IUserService userService = new UserService(dependencyServiceStub);

            UserResponse response = userService.RegisterUser(user);

            Assert.AreEqual(Status.Invalid, response.EntityState);
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("as")]
        [TestCase("asd")]
        [TestCase("asdd")]
        public void Fail_Registration_When_Password_Is_Invalid(string password)
        {
            var user = new User()
            {
                Id = 1,
                EmailAddress = "mail@mail.com",
                FirstName = "FirstName",
                LastName = "LastName",
                Password = password
            };

            var dependencyServiceStub = new DependencyServiceStub();
            dependencyServiceStub.Register<IUserApiService>(new UserApiServiceMock());

            IUserService userService = new UserService(dependencyServiceStub);

            UserResponse response = userService.RegisterUser(user);

            Assert.AreEqual(Status.Invalid, response.EntityState);
        }

    }
}
