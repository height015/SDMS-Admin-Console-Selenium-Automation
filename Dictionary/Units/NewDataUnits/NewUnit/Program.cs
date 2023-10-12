using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SuccessLogin;

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
                Sleep(3000);
                ClickDictionary(driver);
                Sleep(3000);
                ClickUnit(driver);
                Sleep(3000);
                ClickUnitNewRequest(driver);
                Sleep(3000);
                data.UnitDataEntry(driver);
                Sleep(3000);

                //ClickNewRequest(driver);
                //Sleep(3000);
                //ClickRequestType(driver);
                //CreateNewReqGenericPopUp(driver);
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

            Sleep(3000);

            //var freqLink = driver.FindElement(By.LinkText("Frequencies"));
            //freqLink.Click();

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
            Sleep(3000);
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

            Sleep(3000);

            createSec.NewDataUnitEntery(freqVal.DataUnit.Name, freqVal.DataUnit.ShortName);

            Sleep(3000);

            createSec.ClickSubmit();

            Sleep(3000);

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


    private static void Sleep(int timeVal)
    {
        Thread.Sleep(timeVal);
    }
}
