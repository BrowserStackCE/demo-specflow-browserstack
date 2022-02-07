using TechTalk.SpecFlow;
using specflow_lowell.Drivers;
using OpenQA.Selenium;
using FluentAssertions;
namespace specflow_lowell.Steps
{
    [Binding]
    public sealed class SearchStepDefinitions
    {
       
       // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

       private readonly ScenarioContext _scenarioContext;
       private readonly Driver _driver;

       public SearchStepDefinitions(Driver driver,ScenarioContext scenarioContext){
           this._driver = driver;
           this._scenarioContext = scenarioContext;
       }

       [Given(@"Duckduckgo is open")]
       public void DuckduckGoIsOpen()
       {
           _driver.Instance.Navigate().GoToUrl("https://duckduckgo.com");
       }
        
       [Given(@"I search for (.*)")]
       public void ISearchFor(string search)
       {
           _driver.Instance.FindElement(By.Id("search_form_input_homepage")).SendKeys(search);
       }

       [Given(@"Search value should be (.*)")]
       public void SearchValueShouldBe(string value){
           string inputValue = _driver.Instance.FindElement(By.Id("search_form_input_homepage")).Text;
           CustomAssertionAttribute.Equals(inputValue,value);
       }
       [Then(@"Perform Search")]
       public void PerformSearch(){
           _driver.Instance.FindElement(By.Id("search_button_homepage")).Click();
           _driver.Instance.Quit();
       }
    }
}
