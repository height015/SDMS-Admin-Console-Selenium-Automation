using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SuccessLogin;
using SuccessLogin.Utils;


namespace NewDataSource;

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
                ClickDictionary(driver);
                Utils.Sleep(3000);
                ClickDataSource(driver);
                Utils.Sleep(4000);
                ClickDataSourceNewRequest(driver);
                Utils.Sleep(3000);
                data.DataSourceEntry(driver);
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

    #region Data Source
    public static void ClickDataSource(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Data Sources"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static bool ClickDataSourceNewRequest(IWebDriver driver)
    {
        try
        {
            var newReqBtn = driver.FindElement(By.PartialLinkText("New"));
            newReqBtn.Click();
            Utils.Sleep(3000);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }
    public bool DataSourceEntry(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var freqVal = jsonFileReader.ReadJsonFileDataSource();
            var createSec = new DataSources(driver);
            Utils.Sleep(5000);
            createSec.NewDatadataSourceEntry(freqVal.DataSource.Name, freqVal.DataSource.ShortName);
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
