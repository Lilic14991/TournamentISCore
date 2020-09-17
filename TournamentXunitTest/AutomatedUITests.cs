using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace TournamentXunitTest
{
    public class AutomatedUITests
    {
        private string _websiteUrl = "https://localhost:44331/";
        private ITestOutputHelper _output { get; set; }

       // private TournamentPage _page;
        private IWebDriver _driver;

        public AutomatedUITests(ITestOutputHelper output)
        {
            _output = output;
           
            
        }

        [Fact]
        public void Create_Club_ValidModelData_ReturnsIndex()
        {
            //Arange
            _driver = new ChromeDriver();

            _driver.Navigate().GoToUrl(_websiteUrl + "Clubs/Create");
            _driver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(10);



            _driver.FindElement(By.Name("ClubName"))
                .SendKeys("FK Vojvodina");

            _driver.FindElement(By.Name("ClubAddress"))
            .SendKeys("Novi Sad");

            _driver.FindElement(By.Id("ClubBudget"))
           .SendKeys(14000.ToString());

            _driver.FindElement(By.Name("ClubDesc"))
                .SendKeys("Test test test 2");

            _driver.FindElement(By.Name("GroupId")).SendKeys(2.ToString());

            _driver.FindElement(By.Name("TournamentId")).SendKeys(1.ToString());

            // Act
            _driver.FindElement(By.Id("create")).Click();
            _driver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(5);

            // Screenshoot
            ITakesScreenshot screenshotDriver = _driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile("C:/AutoTest/test.png", ScreenshotImageFormat.Png);

            //Assert

            Assert.Contains("Marakana", _driver.PageSource);
            Assert.Contains(140000.ToString(), _driver.PageSource);
            Assert.Contains("Test test test", _driver.PageSource);
            Assert.Contains(2.ToString(), _driver.PageSource);
            Assert.Contains(1.ToString(), _driver.PageSource);
        }


        [Theory]
        [InlineData(3)]
        public void Edit_FindClub(int clubID)
        {
            _driver = new ChromeDriver();

            _driver.Navigate().GoToUrl(_websiteUrl + $"Clubs/Edit/{clubID}");
            _driver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(10);

            _driver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(10);
            _driver.FindElement(By.Id("edit")).Click();


            ITakesScreenshot screenshotDriver = _driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            var fileName = $"{"Find Club" + clubID + "Edit"}";
            screenshot.SaveAsFile($"C:/AutoTest/{fileName}.png", ScreenshotImageFormat.Png);


            Assert.Contains(clubID.ToString(), _driver.PageSource);

            if (_driver.PageSource.Contains(clubID.ToString()))
            {
                _output.WriteLine("Success");
            }
            else
            {
                _output.WriteLine("failed");
            }
            //_driver.Quit();
        }

        [Theory]
        [InlineData("Manchester City", "London", 140000, "testclubdesc")]
        public void EditClub(string clubName, string clubAddress, int clubBudget, string clubDesc)
        {
            var clubID = 12;

            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(_websiteUrl + $"Clubs/Edit/{clubID}");
            _driver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(10);

            _driver.FindElement(By.Name("ClubName")).Clear();
            _driver.FindElement(By.Name("ClubName")).SendKeys(clubName);
            _driver.FindElement(By.Name("ClubAddress")).Clear();
            _driver.FindElement(By.Name("ClubAddress")).SendKeys(clubAddress);
            _driver.FindElement(By.Name("ClubBudget")).Clear();
            _driver.FindElement(By.Name("ClubBudget")).SendKeys(clubBudget.ToString());
            _driver.FindElement(By.Name("ClubDesc")).Clear();
            _driver.FindElement(By.Name("ClubDesc")).SendKeys(clubDesc);
            //_driver.FindElement(By.Id("group")).Clear();
            //_driver.FindElement(By.Id("group")).SendKeys(group.ToString());
            //_driver.FindElement(By.Id("tournament")).Clear();
            //_driver.FindElement(By.Id("tournament")).SendKeys(tournament.ToString());

            _driver.FindElement(By.Id("edit")).Click();

            // screenshoot
            ITakesScreenshot screenshotDriver = _driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            var fileName = $"{clubID} Change data";
            screenshot.SaveAsFile($"C:/AutoTest/{fileName}.png", ScreenshotImageFormat.Png);


            Assert.Contains(clubName, _driver.PageSource);
            Assert.Contains(clubAddress, _driver.PageSource);
            Assert.Contains(clubBudget.ToString(), _driver.PageSource);
            Assert.Contains(clubDesc, _driver.PageSource);
            //Assert.Contains(group.ToString(), _driver.PageSource);
            //Assert.Contains(tournament.ToString(), _driver.PageSource);
        }


        [Theory]
        [InlineData(3)]
        public void Delete_FindClub(int clubID)
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(_websiteUrl + $"Clubs/Delete/{clubID}");
            _driver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(10);

            // screenshoot
            ITakesScreenshot screenshotDriver = _driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            var fileName = $"{clubID} Delete data";
            screenshot.SaveAsFile($"C:/AutoTest/{fileName}.png", ScreenshotImageFormat.Png);

            _driver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(10);
            _driver.FindElement(By.Id("delete")).Click();

            Assert.Contains(clubID.ToString(), _driver.PageSource);

            if (_driver.PageSource.Contains(clubID.ToString()))
            {
                _output.WriteLine("test success!");
            }
            else
            {
                _output.WriteLine("test failed");
            }
        }

        [Theory]
        [InlineData(5)]
        public void Delete_Club(int clubID)
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(_websiteUrl + $"Clubs/Delete/{clubID}");
            _driver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(10);

            // screenshoot
            ITakesScreenshot screenshotDriver = _driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            var fileName = $"{clubID} Deleted data";
            screenshot.SaveAsFile($"C:/AutoTest/{fileName}.png", ScreenshotImageFormat.Png);

            _driver.FindElement(By.Id("delete")).Click();
            _driver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(10);

            Assert.DoesNotContain(clubID.ToString(), _driver.PageSource);

            if (!_driver.PageSource.Contains(clubID.ToString()))
            {
                _output.WriteLine("test success");
            }
            else
            {
                _output.WriteLine("test failed");
            }

        }

        [Theory]
        [InlineData(1)]
        public void Details_FindClub(int clubID)
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(_websiteUrl + $"Clubs/Details/{clubID}");
            _driver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(10);

            // screenshoot
            ITakesScreenshot screenshotDriver = _driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            var fileName = $"{clubID} DisplayDetailsData";
            screenshot.SaveAsFile($"C:/AutoTest/{fileName}.png", ScreenshotImageFormat.Png);

            _driver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(200);
            _driver.FindElement(By.Id("seconary")).Click();

            Assert.Contains(clubID.ToString(), _driver.PageSource);

            if (_driver.PageSource.Contains(clubID.ToString()))
            {
                _output.WriteLine("test success");
            }
            else
            {
                _output.WriteLine("test failed");
            }
        }


        //[fact]
        //public void create_tournament_validmodeldata()
        //{
        //    datetime now = datetime.now;

        //    _page.populatename("barselona");
        //    _page.populatestartdate(now + timespan.fromdays(5));
        //    _page.populateenddate(now.adddays(10));
        //    _page.populatelocation("barselona");

        //    _page.clickcreate();

        //    assert.equal("index", _page.title);
        //    assert.contains("barselona", _page.source);
        //    assert.contains(now.tostring() , _page.source);
        //    assert.contains(now.tostring(), _page.source);
        //    assert.contains("location", _page.source);
        //}
            

            












































    }
}
