using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;


namespace TournamentXunitTest
{
    public class PMOTournamentTest
    {
        private readonly ChromeDriver _driver;
        private readonly TournamentPage _page;

        public PMOTournamentTest()
        {
            _driver = new ChromeDriver();
            _page = new TournamentPage(_driver);
            _page.Navigate();
        }

        [Fact]
        public void Create_WhenSuccessefullyExecuted_ReturnToIndexViewWithNewTournament()
        {
            _page.PopulateName("Tournament 2021");
            _driver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(10);
            _page.PopulateStartDate("18/02/2021");
            _driver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(10);
            _page.PopulateEndDate("23/02/2021");
            _driver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(10);
            _page.PopulateLocation("Berlin");
            _driver.Manage().Timeouts().ImplicitWait.TotalSeconds.Equals(10);
            _page.ClickCreate();

          //  Assert.Equal("Create", _page.Title);
            Assert.Contains("Tournament 2021", _page.Source);
            Assert.Contains("18/02/2021".ToString(), _page.Source);
            Assert.Contains("23/02/2021".ToString(), _page.Source);
            Assert.Contains("Berlin", _page.Source);
        }

        [Fact]
        public void Create_WrongModelData_ReturnsErrorMessage()
        {
            _page.PopulateName("Tournament Game");
            _page.PopulateLocation("Gameland");
            _page.PopulateStartDate("dd/mm/yyyy".ToString());
            _page.PopulateEndDate("dd/mm/yyyy".ToString());
            _page.ClickCreate();

            Assert.Equal("The Start Date field is required.", _page.ErrorStartDate);
            Assert.Equal("The End Date field is required.", _page.ErrorEndDate);
        }






    }
}
