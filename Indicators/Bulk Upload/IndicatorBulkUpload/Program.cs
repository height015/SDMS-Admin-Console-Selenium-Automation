﻿using BulkIndicator;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SuccessLogin;

namespace IndicatorBulkUpload;

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

                ClickIndicatorBulk(driver);
                Sleep(3000);

                IndicatorUploadBulkFile(driver);
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
    public static void ClickIndicatorBulk(IWebDriver driver)
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
    public static void IndicatorUploadBulkFile(IWebDriver driver)
    {
        var indi = new Indicator(driver);

        try
        {
            JsonFileReader jsonFileReader = new();

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string fileName = "Indicator_Today.xlsx";

            string filePath = Path.Combine(desktopPath, fileName);

            Sleep(3000);

            indi.btnBrowseFile.SendKeys(filePath);


            indi.btnUpload.Click();

            Sleep(3000);

            indi.btnClickOk.Click();

            Sleep(3000);

            var retVal = jsonFileReader.ReadJsonBulkIndicator();
            bool modify = retVal.BulkIndicatorNewDataCon.Modify;


            var bulkTableNewDataList = retVal.BulkIndicatorNewDataCon.BulkIndicatorNewData;

            var tableRes = indi.table;
            var rowCount = 0;
            if (modify)
            {
                if (tableRes != null)
                {
                    var rows = indi.rows;
                    rowCount = rows.Count();

                    for (int item = 1; item < rowCount; item++)
                    {

                        IWebElement updateLink = indi.rows[item].FindElement(By.LinkText("Modify"));

                        updateLink.Click();
                        Sleep(3000);

                        indi.displayOrder.SendKeys(bulkTableNewDataList[item].DisplayOrder.ToString());

                        if (bulkTableNewDataList[item].DisplayInChart == true)
                        {

                            Sleep(3000);
                            var switBox = indi.DisplayInChart;
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                            Sleep(2000);
                            indi.txtGrapTit.SendKeys(bulkTableNewDataList[item].GraphTitle);
                        }


                        Sleep(2000);

                        indi.ClickOk();

                    }

                }

            }
            if (indi.txtTopLevelBox != null)
            {
                indi.txtTopLevelBox.Click();
                int indexToSelect = retVal.BulkIndicatorNewDataCon.indexToSelect;
                Sleep(3000);
                indi.boxSel[indexToSelect].Click();

            }

            Sleep(3000);


        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
        finally
        {
            Sleep(3000);
            indi.btnSave.Click();
            Sleep(3000);
            indi.btnClickOk.Click();
            Sleep(9000);

        }
    }
    #endregion

    private static void Sleep(int time)
    {
        Thread.Sleep(time);
    }


}