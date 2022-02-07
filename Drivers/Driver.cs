using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
namespace specflow_lowell.Drivers
{
    public class Driver
    {
        private readonly Lazy<IWebDriver> _lazyDriver;
        private readonly Lazy<WebDriverWait> _lazyDriverWait;
        private readonly TimeSpan _waitDuration = TimeSpan.FromSeconds(10);
        public IWebDriver Instance => _lazyDriver.Value;
        public WebDriverWait Wait => _lazyDriverWait.Value;
        public Driver()
        {
            _lazyDriver = new Lazy<IWebDriver>(GetWebDriver);
            _lazyDriverWait = new Lazy<WebDriverWait>(GetWebDriverWait);
        }

        private WebDriverWait GetWebDriverWait()
        {
            return new WebDriverWait(Instance, _waitDuration);
        }
        private IWebDriver GetWebDriver()
        {
            String username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
            String access_key = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
            String remoteUrl = "https://" + username + ":" + access_key + "@hub-ft.browserstack.com/wd/hub";
            DesiredCapabilities caps = new DesiredCapabilities();
            caps.SetCapability("browserName", "chrome");
            caps.SetCapability("buildName", "High Scale Testing Demo");
            caps.SetCapability("project","Lowell Demo");
            caps.SetCapability("browserVersion","latest");
            return new RemoteWebDriver(new Uri(remoteUrl), caps);

        }
    }
}
