using Framework.ComponentHelper.Congifurations;
using Framework.Pages.HomePage;
using Framework.Pages.LoginPage;
using Framework.Utilities.BaseUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.TestScripts.Sanity
{
    [TestFixture(Category = "Sanity Tests")]
    [Parallelizable(ParallelScope.Children)]
    public class Sanity : Base
    {
        private LoginPage loginpage;
        private HomePage homepage;

        [SetUp]
        public void SetUp()
        {
            loginpage = new LoginPage(GetDriver(), test);
            homepage = new HomePage(GetDriver(), test);
        }


        [Test]
        [Parallelizable(ParallelScope.Self)]
        [Category("01")]
        public void LoginCheck()
        {
            loginpage.EnterCredentials(AppConfigSettings.Username, AppConfigSettings.Password);
            Assert.Fail();
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        [Category("02")]
        public void LogoutCheck()
        {
            loginpage.EnterCredentials(AppConfigSettings.Username, AppConfigSettings.Password);
            homepage.ClickAndVerifyLogout();
        }
    }
}
