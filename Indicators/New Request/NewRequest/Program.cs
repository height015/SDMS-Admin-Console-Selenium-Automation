using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SuccessLogin;
using NewRequestIndicator;
using SeleniumExtras.WaitHelpers;

namespace NewRequest;


public class Program
{
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

                //Indicator
                data.ClickDataSet(driver);
                Sleep(3000);
                ClickIndicators(driver);
                Sleep(3000);
                IndicatorCataloguePopUp(driver);
                Sleep(3000);

                ClickNewRequest(driver);
                Sleep(3000);
                ClickRequestType(driver);
                Sleep(3000);
                CreateNewReqIndicatorPopUp(driver);
                Sleep(3000);
                ClickClose(driver);
                Sleep(3000);
                //ClickIndicatorBulk(driver);
                Sleep(3000);
                //IndicatorUploadBulkFile(driver);
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

    public static bool ClickNewRequest(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/workflow/requests/requests?reqType=1'][onclick*='showLoader()']"));
            dataSetLinkNewReq.Click();

            Sleep(2000);
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
            var createSec = new NewRequest(driver);

            var retVals = lx.ReadJsonFileSelectCheckBoxes();

            var reqType = retVals.CheckBoxNumbers.RequestType;
            Sleep(3000);
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

            Sleep(2000);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("table")));


            var table = createSec.table;
            var rowCount = 0;
            if (table != null)
            {
                var rows = createSec.rows;
                var rowIndexes = retVals.CheckBoxNumbers.GetIndexArray();

                rowCount = rows.Count() - 1;

                foreach (var item in rowIndexes)
                {
                    IWebElement checkbox = createSec.rows[item].FindElement(By.Name("SelItemIds"));
                    checkbox.Click();
                    rowCount--;
                    if (rowCount <= 0)
                    {
                        break;
                    }
                }
            }
            Sleep(3000);

            return rowCount.ToString();
        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return string.Empty;
        }
    }
    public static void ClickClose(IWebDriver driver)
    {
        try
        {
            var closeBtn = driver.FindElement(By.CssSelector("a[href='/dataset/indicators']"));
            closeBtn.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }


    #region Indicators
    public static void ClickIndicators(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Indicators"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static void IndicatorCataloguePopUp(IWebDriver driver)
    {
        try
        {
            var inidi = new Indicator(driver);

            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonFileDataIndicator();

            Sleep(2000);
            //Access the Sector
            SelectElement dropdownSec = new SelectElement(inidi.dropDownCascadeSecor);
            dropdownSec.SelectByIndex(retVal.IndicatorDataSelector.SectorIndex);

            Sleep(2000);
            //Access the Category
            SelectElement dropdownCat = new SelectElement(inidi.dropDownCat);
            dropdownCat.SelectByIndex(retVal.IndicatorDataSelector.CategoryIndex);

            Sleep(2000);
            //Access the Category
            SelectElement dropdownTab = new SelectElement(inidi.dropDownTable);
            dropdownTab.SelectByIndex(retVal.IndicatorDataSelector.TableIndex);

            Sleep(3000);

            inidi.ClickContinue();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static bool CreateNewReqIndicatorPopUp(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();

            var indicatoor = new Indicator(driver);

            var RequestInforVal = jsonFileReader.ReadJsonFileForNewRequestIndicator();
            Sleep(3000);

            IWebElement overlappingDiv = driver.FindElement(By.CssSelector(".col-7.text-right"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", overlappingDiv);
            IWebElement button = driver.FindElement(By.Id("btnReqSelect"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");

            Sleep(3000);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var dataSetLink = wait.Until(d => d.FindElement(By.Id("btnReqSelect")));

            //The Sleep is Inportant so the Pop-Div is loaded to the  DOM
            Sleep(4000);

            button.Click();

            Sleep(3000);

            indicatoor.EnterRequestInfo(RequestInforVal.IndicatorRequestData.Title, RequestInforVal.IndicatorRequestData.Reason);
            Sleep(2000);

            indicatoor.ClickSave();

            Sleep(8000);

            indicatoor.ClickOk();

            return true;
        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    #endregion


    private static void Sleep(int time)
    {
        Thread.Sleep(time);
    }


}