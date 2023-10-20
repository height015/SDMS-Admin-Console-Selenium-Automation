using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SuccessLogin;
using Commons;
using Newtonsoft.Json;

namespace NewDataEntity;
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
                Utils.Sleep(300);
                ClickDictionary(driver);
                Utils.Sleep(3000);
                ClickDataEntity(driver);
                Utils.Sleep(3000);
                ClickDataEntityNewRequest(driver);
                Utils.Sleep(3000);
                data.DataEntityEntry(driver);
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
            Utils.Sleep(3000);
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
            var freqVal = ReadJsonFileDataEntity();
            var createSec = new DataEntities(driver);
            Utils.Sleep(3000);
            createSec.NewDatadataEntityEntry(freqVal.DataEntities.Name, freqVal.DataEntities.ShortName);
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



    #region Utility
    public static DataEntitiesContainer ReadJsonFileDataEntity()
    {
        try
        {
              string jsonFileName = "DataEntity.json";
              string jsonFilePath = Path.Combine(desktopPath, "SeleniumTest", jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataEntitiesContainer retVal = JsonConvert.DeserializeObject<DataEntitiesContainer>(jsonContent);
                return retVal;
            }

            return new DataEntitiesContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataEntitiesContainer();
        }
    }

    #endregion
}
