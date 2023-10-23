using NewRequestTable;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Commons;
using Newtonsoft.Json;

namespace NewRequestTable;

public class Program
{
    public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    public const string jsonFileName = "Tables.json";
    public static string jsonFilePath = Path.Combine(desktopPath,
        "SeleniumTest", jsonFileName);
    public static int reqType = -1;


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
                ClickRequestType(driver);
                Utils.Sleep(3000);
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
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
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
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static void TableCatalogueSelectorPopUp(IWebDriver driver)
    {
        try
        {
            Utils.Sleep(1000);
            var table = new Tables(driver);
            driver.WaitForElementToBeClickable(table.dropDownCascadeSecor, 10);
            JsonFileReader jsonFileReader = new();
            var retVal = jsonFileReader.ReadJsonFileForTableDataSector();
            table.dropDownCascadeSecor.SelectDropDownByIndex(retVal.TableDataSelector.DataSectorIndexToSelect);
            driver.WaitForElementToBeClickable(table.dropDownCascadeSecor, 10);
            Utils.Sleep(2000);
            table.dropDownCat.SelectDropDownByIndex(retVal.TableDataSelector.DataCategoryIndexToSelect);
            table.ClickContinue();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static string ClickRequestType(IWebDriver driver)
    {
        try
        {
            JsonFileReader lx = new();
            var createSec = new NewRequests(driver);
            var retVals = lx.ReadJsonFileSelectCheckBoxes();
            reqType = retVals.CheckBoxNumbers.RequestType;
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
            driver.WaitForElementToBeClickable(createSec.table, 10);
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
            Utils.Sleep(3000);
            return rowCount.ToString();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return string.Empty;
        }
    }
    public static bool TableCreateNewReqPopUp(IWebDriver driver)
    {
        try
        {
            var table = new Tables(driver);
            var RequestInforVal = ReadJsonFileForNewRequestTable();
            Utils.Sleep(3000);
            var overlappingDiv = driver.FindElement(By.CssSelector(".col-7.text-right"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", overlappingDiv);
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
            Utils.Sleep(3000);
            var btn = driver.WaitForElementToBeClickable(driver.FindElement(By.Id("btnReqSelect")), 10);
            Utils.Sleep(4000);
            btn.Click();
            Utils.Sleep(3000);
            table.EnterRequestInfo(RequestInforVal.TableRequestData.Title, RequestInforVal.TableRequestData.Reason);
            Utils.Sleep(2000);
            table.ClickSave();
            Utils.Sleep(4000);
            table.ClickOk();
            string enumString = Enum.GetName(typeof(RequestType), reqType);
            Utils.LogSuccess(enumString, "Tables");
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }
    #endregion

    #region Utility
    private  static TableRequestDataContainer ReadJsonFileForNewRequestTable()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<TableRequestDataContainer>(jsonContent);
                return retVal;
            }
            return new TableRequestDataContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new TableRequestDataContainer();
        }
    }
    #endregion
}