using NUnit.Framework;
using UITests.Pages;
using Xamarin.UITest;

namespace UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public abstract class BaseTest
    {
        protected IApp app;
        protected Platform platform;

        protected RegistrationPage registrationPage;
        protected WelcomePage welcomePage;

        public BaseTest(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);

            registrationPage = new RegistrationPage(app, platform);
            welcomePage = new WelcomePage(app, platform);
        }
    }
}
