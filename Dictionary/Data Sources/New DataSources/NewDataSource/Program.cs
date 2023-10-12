using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SuccessLogin;

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
                Sleep(3000);
                ClickDataSource(driver);
                Sleep(4000);
                ClickDataSourceNewRequest(driver);
                Sleep(3000);
                data.DataSourceEntry(driver);
                Sleep(3000);
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

            //var newReqBtn = driver.FindElement(By.CssSelector("a[href^='dictionary/data-sources/add']"));
            newReqBtn.Click();
            Sleep(3000);
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

            Sleep(5000);

            createSec.NewDatadataSourceEntry(freqVal.DataSource.Name, freqVal.DataSource.ShortName);

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
