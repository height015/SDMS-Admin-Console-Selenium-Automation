using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SuccessLogin;
using Commons;
using Newtonsoft.Json;

namespace NewFrequency;

public class Program
{
    public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    private static readonly string _URL = "http://197.255.51.104:9035";
    public static void Main(string[] args)
    {
        using (var driver = new ChromeDriver())
        {
            var loginObj = new SuccessLogin.Program();
            var data = new Program();
            bool login = loginObj.LoginSuccess(driver);
            if (login)
            {
                Utils.Sleep(3000);
                ClickDictionary(driver);
                Utils.Sleep(3000);
                ClickFrequency(driver);
                Utils.Sleep(3000);
                ClickFrequencyNewRequest(driver);
                Utils.Sleep(3000);
                data.DataEntryFrequency(driver);
                Utils.Sleep(3000);
            }
        }
    }

    public static void ClickDictionary(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Dictionaries"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static bool ClickNewRequest(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/workflow/requests/requests?reqType=1'][onclick*='showLoader()']"));
            dataSetLinkNewReq.Click();
            Utils.Sleep(2000);
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }

    #region Frequency
    public static void ClickFrequency(IWebDriver driver)
    {
        try
        {
            var freqLink = driver.FindElement(By.LinkText("Frequencies"));
            freqLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static bool ClickFrequencyNewRequest(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a[href^='/dictionary/frequencies/add']"));
            dataSetLinkNewReq.Click();
            Utils.Sleep(3000);
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }

    public bool DataEntryFrequency(IWebDriver driver)
    {
        try
        {
                 

            var freqVal = ReadJsonFileDataFrequency();
            var createSec = new Frequency(driver);
            Utils.Sleep(3000);
            createSec.NewDataFrequencyEntery(freqVal.DataFrequency.Name, freqVal.DataFrequency.ShortName);
            Utils.Sleep(3000);
            createSec.ClickSubmit();
            Utils.Sleep(3000);
            createSec.ClickOk();
            Utils.LogSuccess($"Create {freqVal.DataFrequency.Name}", "Dictionary Frequency");
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }
    #endregion


    #region Utility
    public static DataFrequencyContainer ReadJsonFileDataFrequency()
    {
        try
        {
            string jsonFileName = "Frequency.json";
            string jsonFilePath = Path.Combine(desktopPath, "SeleniumTest", jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataFrequencyContainer retVal = JsonConvert.DeserializeObject<DataFrequencyContainer>(jsonContent);
                return retVal;
            }

            return new DataFrequencyContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new DataFrequencyContainer();
        }
    }

    #endregion
}
