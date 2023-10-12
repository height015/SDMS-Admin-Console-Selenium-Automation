using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SuccessLogin;

namespace NewDataEntity;



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
                Sleep(300);
                ClickDictionary(driver);
                Sleep(3000);
                ClickDataEntity(driver);
                Sleep(3000);
                ClickDataEntityNewRequest(driver);
                Sleep(3000);
                data.DataEntityEntry(driver);
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

    #region Data Entities
    public static void ClickDataEntity(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Data Entities"));
            dataSetLink.Click();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static bool ClickDataEntityNewRequest(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a[href^='/dictionary/data-entities/add']"));
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
    public bool DataEntityEntry(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var freqVal = jsonFileReader.ReadJsonFileDataEntity();
            var createSec = new DataEntities(driver);

            Sleep(3000);

            createSec.NewDatadataEntityEntry(freqVal.DataEntities.Name, freqVal.DataEntities.ShortName);

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
