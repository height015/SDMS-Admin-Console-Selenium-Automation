using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SuccessLogin;
using SuccessLogin.Utils;


namespace NewRequest;

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
                ClickFrequency(driver);
                Utils.Sleep(3000);
                ClickNewRequest(driver);
                Utils.Sleep(3000);
                ClickRequestType(driver);
                CreateNewReqGenericPopUp(driver);
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
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static string ClickRequestType(IWebDriver driver)
    {
        try
        {
            JsonFileReader lx = new();
            var retVals = lx.ReadJsonFileSelectCheckBoxes();
            var create = new NewRequest(driver);
            var reqType = retVals.CheckBoxNumbers.RequestType;
            Utils.Sleep(3000);
            IWebElement btn;
            switch (reqType)
            {
                case (int)RequestType.AuthorizationRequest:
                    btn = driver.FindElement(By.LinkText("Authorize"));
                    btn.Click();
                    break;
                case (int)RequestType.UnAuthorization:
                    btn = driver.FindElement(By.LinkText("Unauthorize"));
                    btn.Click();
                    break;
                case (int)RequestType.ArchiveRequest:
                    btn = driver.FindElement(By.LinkText("Archive"));
                    btn.Click();
                    break;
                case (int)RequestType.UnarchiveRequest:
                    btn = driver.FindElement(By.LinkText("Unarchive"));
                    btn.Click();
                    break;
                case (int)RequestType.ModificationRequest:
                    btn = driver.FindElement(By.LinkText("Modify"));
                    btn.Click();
                    break;
                case (int)RequestType.PublicationRequest:
                    btn = driver.FindElement(By.LinkText("Publish"));
                    btn.Click();
                    break;
                case (int)RequestType.UnpublicationRequest:
                    btn = driver.FindElement(By.LinkText("Unpublish"));
                    btn.Click();
                    break;
                default:
                    btn = driver.FindElement(By.LinkText("Authorize"));
                    btn.Click();
                    break;
            }
            Utils.Sleep(7000);
            var table = create.table;
            var rowCount = 0;
            if (table != null)
            {
                var rows = create.rows;
                var rowIndexes = retVals.CheckBoxNumbers.GetIndexArray();
                rowCount = rows.Count() - 1;
                foreach (var item in rowIndexes)
                {
                    IWebElement checkbox = create.rows[item].FindElement(By.Name("SelItemIds"));
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
    public static bool CreateNewReqGenericPopUp(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var genericVal = new NewRequest(driver);
            var RequestInforVal = jsonFileReader.ReadJsonFileForNewRequestIndicator();
            Utils.Sleep(3000);
            IWebElement overlappingDiv = driver.FindElement(By.CssSelector(".col-7.text-right"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", overlappingDiv);
            IWebElement button = driver.FindElement(By.Id("btnReqSelect"));
            //or Use this
            if (!(bool)((IJavaScriptExecutor)driver).ExecuteScript(
    "var elem = arguments[0],                 " +
    "  box = elem.getBoundingClientRect(),    " +
    "  cx = box.left + box.width / 2,         " +
    "  cy = box.top + box.height / 2,         " +
    "  e = document.elementFromPoint(cx, cy); " +
    "for (; e; e = e.parentElement) {         " +
    "  if (e === elem)                        " +
    "    return true;                         " +
    "}                                        " +
    "return false;                            ", button))
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button);
            }
            Utils.Sleep(3000);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var dataSetLink = wait.Until(d => d.FindElement(By.Id("btnReqSelect")));
            //The Utils.Sleep is Inportant so the Pop-Div is loaded to the  DOM
            Utils.Sleep(4000);
            button.Click();
            Utils.Sleep(3000);
            genericVal.EnterRequestInfo(RequestInforVal.IndicatorRequestData.Title, RequestInforVal.IndicatorRequestData.Reason);
            Utils.Sleep(2000);
            genericVal.ClickSave();
            Utils.Sleep(8000);
            genericVal.ClickOk();
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
