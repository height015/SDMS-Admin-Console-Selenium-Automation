using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SuccessLogin;
using SuccessLogin.Utils;


namespace NewUnit;

public class Program
{
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
                ClickUnit(driver);
                Utils.Sleep(3000);
                ClickUnitNewRequest(driver);
                Utils.Sleep(3000);
                data.UnitDataEntry(driver);
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
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    #region Unit
    public static void ClickUnit(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Units"));
            dataSetLink.Click();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static bool ClickUnitNewRequest(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a[href^='/dictionary/units/add']"));
            dataSetLinkNewReq.Click();
            Utils.Sleep(3000);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    public bool UnitDataEntry(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var freqVal = jsonFileReader.ReadJsonFileDataUnit();
            var createSec = new NewRequest(driver);
            Utils.Sleep(3000);
            createSec.NewDataUnitEntery(freqVal.DataUnit.Name, freqVal.DataUnit.ShortName);
            Utils.Sleep(3000);
            createSec.ClickSubmit();
            Utils.Sleep(3000);
            createSec.ClickOk();
            return true;
        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }
    #endregion

}