using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SuccessLogin;
using System.Net;
using SeleniumExtras.WaitHelpers;

namespace RequestCategory;

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

                #region Category
                Sleep(3000);
                ClickDashBorad(driver);
                Sleep(3000);
                ClickDataCatalogCard(driver);
                Sleep(3000);
                ClickCategoryCard(driver);
                ClickNewRequest(driver);
                ClickRequestType(driver);
                SelectCheckBoxes(driver);
                data.CategoryRequestInfBox(driver);
                Sleep(3000);
                #endregion
            }



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
    public static string SelectCheckBoxes(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var createSec = new NewRequest(driver);

            for (int i = 1; i < createSec.rows.Count; i++)
            {
                IWebElement checkbox = createSec.rows[i].FindElement(By.Name("SelItemIds"));

                checkbox.Click();
            }

            Sleep(3000);

            return createSec.rows.Count.ToString();
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
    public static void ClickDashBorad(IWebDriver driver)
    {
        try
        {
            var dashBoard = driver.FindElement(By.LinkText("Dashboard"));
            dashBoard.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public bool CategoryRequestInfBox(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();

            var loginVal = jsonFileReader.ReadJsonFileSelectCheckBoxes();

            var createSec = new NewRequest(driver);

            var RequestInforVal = jsonFileReader.ReadJsonFileForSelectCheckBoxesProcessCatNewRequest();

            Sleep(3000);

            IWebElement overlappingDiv = driver.FindElement(By.CssSelector(".col-7.text-right"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", overlappingDiv);

            IWebElement button = driver.FindElement(By.Id("btnReqSelect"));

            if (createSec.rows.Count > 10)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button);

            }


            //or Use this

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");

            Sleep(3000);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var dataSetLink = wait.Until(d => d.FindElement(By.Id("btnReqSelect")));

            //The Sleep is Inportant so the Pop-Div is loaded to the  DOM
            Sleep(4000);

            button.Click();

            Sleep(3000);

            createSec.EnterRequestInfo(RequestInforVal.CatRequestInformation.Title, RequestInforVal.CatRequestInformation.Reason);

            Sleep(2000);

            createSec.ClickSubmit();

            Sleep(3000);

            var waitx = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var dataSetLinks = waitx.Until(d => d.FindElement(By.CssSelector("button.confirm")));

            dataSetLinks.Click();
            //createSec.ClickOk();

            return true;


        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }

    }

    #region Data Category
    public static void ClickDataCatalogCard(IWebDriver driver)
    {
        try
        {
            var cartCard = driver.FindElement(By.LinkText("Data Catalogues."));

            cartCard.Click();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static void ClickCategoryCard(IWebDriver driver)
    {
        try
        {
            Sleep(3000);
            var cartCard = driver.FindElement(By.LinkText("Categories"));
            Sleep(3000);
            cartCard.Click();
            Sleep(5000);

            var catSec = new Category(driver);

            SelectElement dropdown = new SelectElement(catSec.dropDownBox);


            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonFileForSelectOptionCatalogSelector();

            dropdown.SelectByIndex(retVal.CatalogueSelector.OptionToSelect);
            Sleep(3000);
            catSec.ClickContinue();
            Sleep(3000);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }


    #endregion

    private static void Sleep(int time)
    {
        Thread.Sleep(time);
    }


}