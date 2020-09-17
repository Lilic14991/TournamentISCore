using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentXunitTest
{
    public class TournamentPage
    {
        private readonly IWebDriver _driver;
        private const string Uri = "https://localhost:44331/Tournaments/Create";

        private IWebElement TournamentName => _driver.FindElement(By.Name("TournamentName"));
        private IWebElement StartDate => _driver.FindElement(By.Name("StartDate"));
        private IWebElement EndDate => _driver.FindElement(By.Name("EndDate"));
        private IWebElement TournamentLocation => _driver.FindElement(By.Name("TournamentLocation"));
        private IWebElement CreateElement => _driver.FindElement(By.Id("primary"));

        

       // public string Title => _driver.Title;
        public string Source => _driver.PageSource;
        public string ErrorStartDate => _driver.FindElement(By.Id("startDate-error")).Text;
        public string ErrorEndDate => _driver.FindElement(By.Id("endDate-error")).Text;


        public TournamentPage(IWebDriver driver)
        {
            _driver = driver;
        }


        public void Navigate()
        {
            _driver.Navigate().GoToUrl(Uri);
        }

        public void PopulateName(string name) => TournamentName.SendKeys(name);
        public void PopulateStartDate(string startDate) => StartDate.SendKeys(startDate.ToString());
        public void PopulateEndDate(string endDate) => EndDate.SendKeys(endDate.ToString());
        public void PopulateLocation(string location) => TournamentLocation.SendKeys(location);

        public void ClickCreate() => CreateElement.Click();


    }

}
