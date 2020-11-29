using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    public class Tests : BaseTest
    {
        public Tests(Platform platform) : base(platform)
        {
        }

        [SetUp]
        public void BeforeEachTest()
        {
            base.BeforeEachTest();

            registrationPage.WaitForPageToLoad();
        }

        [Test]
        public void RegistrationPageIsVisible()
        {
            Assert.IsTrue(registrationPage.IsPageVisible);
        }

        [Test]
        public void CompletedForm_Should_Display_WelcomePage()
        {
            registrationPage.EnterFirstName("FirstName");
            registrationPage.EnterEmailAddress("email@com.com");
            registrationPage.EnterLastName("LastName");
            registrationPage.EnterPassword("Password");

            registrationPage.TapSignupButton();

            welcomePage.WaitForPageToLoad();

            Assert.IsTrue(welcomePage.IsPageVisible);
        }

        [Test]
        public void CompletingEmptyForm_Should_Not_Disply_Welcome_Page()
        {          
            registrationPage.TapSignupButton();

           
            Assert.IsFalse(welcomePage.IsPageVisible);
        }
    }
}


