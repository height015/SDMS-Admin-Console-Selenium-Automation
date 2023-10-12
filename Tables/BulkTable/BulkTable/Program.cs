using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SuccessLogin;

namespace BulkTable;

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

                Sleep(3000);
                ClickTableBulk(driver);
                Sleep(3000);
                TableUploadBulkFile(driver);
                Sleep(3000);
               
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
    public static void ClickTableBulk(IWebDriver driver)
    {
        try
        {
            var tableBulk = driver.FindElement(By.LinkText("Bulk"));

            tableBulk.Click();

            Sleep(3000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static void TableUploadBulkFile(IWebDriver driver)
    {
        try
        {
            var table = new Tables(driver);
            JsonFileReader jsonFileReader = new();

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string fileName = "Table_Template.xlsx";

            string filePath = Path.Combine(desktopPath, fileName);

            Sleep(3000);

            table.btnBrowseFile.SendKeys(filePath);


            table.btnUpload.Click();

            Sleep(3000);

            table.btnClickOk.Click();

            Sleep(3000);

            var retVal = jsonFileReader.ReadJsonBulkTabe();
            bool applyAll = retVal.BulkTableNewDataCon.ApplyAll;


            var bulkTableNewDataList = retVal.BulkTableNewDataCon.BulkTableNewData;

            var tableRes = table.table;
            var rowCount = 0;
            if (tableRes != null && applyAll == false)
            {
                var rows = table.rows;
                rowCount = rows.Count();

                for (int item = 1; item < rowCount; item++)
                {

                    IWebElement updateLink = table.rows[item].FindElement(By.LinkText("Update"));

                    updateLink.Click();
                    Sleep(3000);

                    table.dropDownFeq.SelectDropDown(bulkTableNewDataList[item].FreqIndexToSelect);
                    table.dropDownUnit.SelectDropDown(bulkTableNewDataList[item].UnitIndexToSelect);

                    table.ClickUpdate();

                    Sleep(2000);

                    table.ClickOk();
                }
            }
            else if (applyAll)
            {
                IWebElement updateLink = table.rows[1].FindElement(By.LinkText("Update"));
                updateLink.Click();
                Sleep(3000);
                table.dropDownFeq.SelectDropDown(bulkTableNewDataList[1].FreqIndexToSelect);
                table.dropDownUnit.SelectDropDown(bulkTableNewDataList[1].UnitIndexToSelect);

                table.ClickUpdate();

                Sleep(2000);

                table.ClickOk();

                var applyBtn = table.btnApply;
                Sleep(3000);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", applyBtn);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", applyBtn);
                Sleep(3000);
                table.ClickOk();

            }

            Sleep(4000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");

            table.ClickSave();
            Sleep(3000);
            table.btnClickOk.Click();
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