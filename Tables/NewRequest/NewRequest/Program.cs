using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SuccessLogin;
using NewRequestTable;
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
                #region Tables
                data.ClickDataSet(driver);
                ClickTableCard(driver);
                TableCatalogueSelectorPopUp(driver);

                //Sleep(3000);
                //ClickTableBulk(driver);
                //Sleep(3000);
                //TableUploadBulkFile(driver);
                //Sleep(3000);
                ClickRequestType(driver);
                Sleep(3000);
                TableCreateNewReqPopUp(driver);
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

    #region Tables
    public static void ClickTableCard(IWebDriver driver)
    {
        try
        {
            var tableCard = driver.FindElement(By.LinkText("Tables"));

            tableCard.Click();

            Sleep(3000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static void TableCatalogueSelectorPopUp(IWebDriver driver)
    {
        try
        {
            Sleep(1000);
            var table = new Tables(driver);



            SelectElement dropdown = new SelectElement(table.dropDownCascadeSecor);

            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonFileForTableDataSector();
            // Select by value

            dropdown.SelectByIndex(retVal.TableDataSelector.OptionToSelect);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


            Sleep(2000);

            table.dropDownCat.SelectDropDown(1);

            table.ClickContinue();

            Sleep(3000);

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
            var createSec = new NewRequests(driver);

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

    public static bool TableCreateNewReqPopUp(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var table = new Tables(driver);

            var RequestInforVal = jsonFileReader.ReadJsonFileForNewRequestTable();
            Sleep(3000);

            IWebElement overlappingDiv = driver.FindElement(By.CssSelector(".col-7.text-right"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", overlappingDiv);
            IWebElement button = driver.FindElement(By.Id("btnReqSelect"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");

            Sleep(3000);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var dataSetLink = wait.Until(d => d.FindElement(By.Id("btnReqSelect")));

            Sleep(4000);

            button.Click();

            Sleep(3000);

            table.EnterRequestInfo(RequestInforVal.TableRequestData.Title, RequestInforVal.TableRequestData.Reason);
            Sleep(2000);

            table.ClickSave();

            Sleep(4000);

            table.ClickOk();

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