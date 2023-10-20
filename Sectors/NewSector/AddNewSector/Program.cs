using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Commons;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AddNewSector;
public class Program
{
    public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    private static readonly string _URL = "http://197.255.51.104:9035";

    //http://197.255.51.104:9008
    //http://197.255.51.104:9035
    public static void Main(string[] args)
    {
        using (var driver = new ChromeDriver())
        {
            var loginObj = new SuccessLogin.Program();
            var data = new Program();
            bool login = loginObj.LoginSuccess(driver);
            if (login)
            {
                #region Sector
                Utils.Sleep(3000);
                data.ClickDataSet(driver);
                Utils.Sleep(3000);
                data.ClickSector(driver);
                Utils.Sleep(3000);
                data.CreateNewDataSectorSuccess(driver);
                Utils.Sleep(3000);
                #endregion
            }
        }
    }

    public bool ClickDataSet(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Datasets"));
            dataSetLink.Click();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    #region Sector Creation
    public bool ClickSector(IWebDriver driver)
    {
        try
        {
            var dataSetLinkSec = driver.FindElement(By.LinkText("Sectors"));
            dataSetLinkSec.Click();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }
    public string CreateNewDataSectorSuccess(IWebDriver driver)
    {
        try
        {
            var secVal = ReadJsonFileCreateSector();
            var createSec = new Sector(driver);
            createSec.ClickNew();
            Utils.Sleep(2000);
            createSec.EnterNameAndTitle(secVal.SectorField.Name, secVal.SectorField.Title);
            createSec.ClickSubmit();
            Utils.Sleep(3000);
            var teaxVal = createSec.textMsgRes.Text;
            Utils.Sleep(3000);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By
                .CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']")));
            createSec.ClickOk();
            return teaxVal;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.StackTrace} and {ex.InnerException} and {ex.Message}");
            return string.Empty;
        }
    }
    #endregion

    #region Utility
    public static DataSector ReadJsonFileCreateSector()
    {
        try
        {
            string jsonFileNameSec = "Sector.json";
            string jsonFilePathSec = Path.Combine(desktopPath,
                 "SeleniumTest", jsonFileNameSec);
            if (File.Exists(jsonFilePathSec))
            {
                var jsonContent = File.ReadAllText(jsonFilePathSec);
                var retVal = JsonConvert.DeserializeObject<DataSector>(jsonContent);
                return retVal;
            }
            return new DataSector();
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return new DataSector();
        }
    }
    #endregion
}