using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Commons;
using Newtonsoft.Json;

namespace NewSectorRequest;
public class Program
{
    private static readonly string _URL = "http://197.255.51.104:9035";
    public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

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
                ClickNewRequest(driver);
                Utils.Sleep(2000);
                ClickRequestType(driver);
                Utils.Sleep(2000);
                SelectCheckBoxes(driver);
                Utils.Sleep(2000);
                data.RequestInfBox(driver);
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

    #region New Sector Request
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
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }
    public static string ClickRequestType(IWebDriver driver)
    {
        try
        {
            JsonFileReader lx = new();
            var newReqObj = new NewRequestObj(driver);
            var retVals = lx.ReadJsonFileSelectCheckBoxes();
            var reqType = retVals.CheckBoxNumbers.RequestType;
            Utils.Sleep(3000);
            IWebElement btn = null;
            switch (reqType)
            {
                case (int)RequestType.AuthorizationRequest:
                    btn = driver.FindElement(By.LinkText("Authorize"));
                    break;
                case (int)RequestType.UnAuthorization:
                    btn = driver.FindElement(By.LinkText("Unauthorize"));
                    break;
                case (int)RequestType.ArchiveRequest:
                    btn = driver.FindElement(By.LinkText("Archive"));
                    break;
                case (int)RequestType.UnarchiveRequest:
                    btn = driver.FindElement(By.LinkText("Unarchive"));
                    break;
                case (int)RequestType.ModificationRequest:
                    btn = driver.FindElement(By.LinkText("Modify"));
                    break;
                case (int)RequestType.PublicationRequest:
                    btn = driver.FindElement(By.LinkText("Publish"));
                    break;
                case (int)RequestType.UnpublicationRequest:
                    btn = driver.FindElement(By.LinkText("Unpublish"));
                    break;
                default:
                    btn = driver.FindElement(By.LinkText("Authorize"));
                    break;
            }
            if (btn != null)
            {
                btn.Click();
            }
            Utils.Sleep(2000);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("table")));
            var table = newReqObj.table;
            var rowCount = 0;
            if (table != null)
            {
                var rows = newReqObj.rows;
                var rowIndexes = retVals.CheckBoxNumbers.GetIndexArray();
                rowCount = rows.Count() - 1;
                foreach (var item in rowIndexes)
                {
                    IWebElement checkbox = newReqObj.rows[item].FindElement(By.Name("SelItemIds"));
                    checkbox.Click();
                    rowCount--;
                    if (rowCount <= 0)
                    {
                        break;
                    }
                }
            }
            Utils.Sleep(3000);
            return rowCount.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return string.Empty;
        }
    }
    public static string SelectCheckBoxes(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var newReqObj = new NewRequestObj(driver);
            for (int i = 1; i < newReqObj.rows.Count; i++)
            {
                IWebElement checkbox = newReqObj.rows[i].FindElement(By.Name("SelItemIds"));
                checkbox.Click();
            }
            Utils.Sleep(3000);
            return newReqObj.rows.Count.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return string.Empty;
        }
    }
    public bool RequestInfBox(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var loginVal = jsonFileReader.ReadJsonFileSelectCheckBoxes();
            var newReqObj = new NewRequestObj(driver);
            var RequestInforVal = ReadJsonFileForSelectCheckBoxesProcessNewRequest();
            Utils.Sleep(3000);
            IWebElement overlappingDiv = driver.FindElement(By.CssSelector(".col-7.text-right"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", overlappingDiv);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
            Utils.Sleep(3000);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var btn = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("btnReqSelect")));
            //The Utils.Sleep is Inportant so the Pop-Div is loaded to the  DOM
            Utils.Sleep(4000);
            btn.Click();
            Utils.Sleep(3000);
            var waitbox = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var bttn = waitbox.Until(ExpectedConditions.ElementIsVisible(By.Id("txtTitle")));
            newReqObj.EnterRequestInfo(RequestInforVal.RequestInformation.Title, RequestInforVal.RequestInformation.Reason);
            Utils.Sleep(3000);
            newReqObj.ClickSubmit();
            Utils.Sleep(6000);
            newReqObj.ClickOk();
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
    public static Request ReadJsonFileForSelectCheckBoxesProcessNewRequest()
    {
        try
        {
            string jsonFileNamex = "Sector.json";
            string jsonFilePathx = Path.Combine(desktopPath,
                "SeleniumTest", jsonFileNamex);

            if (File.Exists(jsonFilePathx))
            {
                var jsonContent = File.ReadAllText(jsonFilePathx);
                var retVal = JsonConvert.DeserializeObject<Request>(jsonContent);
                return retVal;
            }
            return new Request();
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return new Request();
        }
    }

    #endregion
}