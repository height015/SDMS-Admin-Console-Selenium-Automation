﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SuccessLogin;
using Commons;
using Newtonsoft.Json;

namespace NewTable;

public class Program
{
    private static readonly string _URL = "http://197.255.51.104:9035";

    public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    public static string jsonFileName = "Tables.json";
    public static string jsonFilePath = Path.Combine(desktopPath,
        "SeleniumTest", jsonFileName);


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
                TableCreateNewPopUp(driver);
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
            JsonFileReader jsonFileReader = new();
            var retVal = jsonFileReader.ReadJsonFileForTableDataSector();
            table.dropDownCascadeSecor.SelectDropDownByIndex(retVal.TableDataSelector.DataSectorIndexToSelect);
            driver.WaitForElementToBeClickable(table.dropDownCat, 10);
            table.dropDownCat.SelectDropDownByIndex(retVal.TableDataSelector.DataCategoryIndexToSelect);
            table.ClickContinue();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static void TableCreateNewPopUp(IWebDriver driver)
    {
        try
        {
            Utils.Sleep(1000);
            var table = new Tables(driver);
            table.ClickNew();
            Utils.Sleep(4000);
            JsonFileReader jsonFileReader = new();
            var retVal = ReadJsonFileForNewDataTable();
            driver.WaitForElementToBeClickable(table.txtBoxName, 10);
            table.EnterTableInfoData(retVal.TableNewData.Name, retVal.TableNewData.Title, retVal.TableNewData.Description);
            var dropFreq = ReadJsonFileForTableFrequency();
            table.dropDownFeq.SelectDropDownByIndex(dropFreq.TableFrequency.OptionToSelect);
            var dropUnit = ReadJsonFileForTableUnit();
            table.dropDownUnit.SelectDropDownByIndex(dropUnit.TableUnit.OptionToSelect);
            var switchBoxControl = ReadJsonFileNewDataTableSettings();
            var txtBoxVal = ReadJsonFileDataTableTxt();
            string textData1;
            string textData2;
            string textData3;
            string textData4;
            string textData5;
            if (switchBoxControl.DataTableSetting.Data1 == true)
            {
                textData1 = txtBoxVal.DataTableTxtVal.Txt1;
                string readonlyAttribute = table.txtDataLab1.GetAttribute("readonly");
                textData1 = txtBoxVal.DataTableTxtVal.Txt1;
                table.txtDataLab1.SendKeys(textData1);
                if (readonlyAttribute != null)
                {
                    var switBox = table.checkBox1;
                    Utils.Sleep(3000);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                    textData1 = txtBoxVal.DataTableTxtVal.Txt1;
                    table.txtDataLab1.SendKeys(textData1);
                }
            }

            if (switchBoxControl.DataTableSetting.Data2 == true)
            {
                textData2 = txtBoxVal.DataTableTxtVal.Txt2;
                string readonlyAttribute = table.txtDataLab2.GetAttribute("readonly");
                if (readonlyAttribute != null)
                {
                    var switBox = table.checkBox2;
                    Utils.Sleep(3000);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                    Utils.Sleep(2000);
                    textData2 = txtBoxVal.DataTableTxtVal.Txt2;
                    table.txtDataLab2.SendKeys(textData2);
                }
            }

            if (switchBoxControl.DataTableSetting.Data3 == true)
            {
                textData3 = txtBoxVal.DataTableTxtVal.Txt3;
                string readonlyAttribute = table.txtDataLab3.GetAttribute("readonly");
                if (readonlyAttribute != null)
                {
                    var switBox = table.checkBox3;
                    Utils.Sleep(3000);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                    textData3 = txtBoxVal.DataTableTxtVal.Txt3;
                    table.txtDataLab3.SendKeys(textData3);
                }
            }

            if (switchBoxControl.DataTableSetting.Data4 == true)
            {
                textData4 = txtBoxVal.DataTableTxtVal.Txt4;
                string readonlyAttribute = table.txtDataLab4.GetAttribute("readonly");
                if (readonlyAttribute != null)
                {
                    var switBox = table.checkBox4;
                    Utils.Sleep(3000);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                    textData4 = txtBoxVal.DataTableTxtVal.Txt4;
                    table.txtDataLab4.SendKeys(textData4);
                }
            }

            if (switchBoxControl.DataTableSetting.Data5 == true)
            {
                textData5 = txtBoxVal.DataTableTxtVal.Txt5;
                string readonlyAttribute = table.txtDataLab5.GetAttribute("readonly");
                if (readonlyAttribute != null)
                {
                    var switBox = table.checkBox5;
                    Utils.Sleep(3000);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                    textData5 = txtBoxVal.DataTableTxtVal.Txt5;
                    table.txtDataLab5.SendKeys(textData5);
                }
            }
            Utils.Sleep(3000);
            table.ClickSubmit();
            Utils.Sleep(4000);
            table.ClickOk();
            Utils.LogSuccess($"Create", "Tables");

        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    #endregion


    #region Utility
    private static TableNewDataContainer ReadJsonFileForNewDataTable()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                TableNewDataContainer retVal = JsonConvert.DeserializeObject<TableNewDataContainer>(jsonContent);
                return retVal;
            }
            return new TableNewDataContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new TableNewDataContainer();
        }
    }

    private static TableFrequencyContainer ReadJsonFileForTableFrequency()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<TableFrequencyContainer>(jsonContent);
                return retVal;
            }
            return new TableFrequencyContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new TableFrequencyContainer();
        }
    }
    private static TableUnitContainer ReadJsonFileForTableUnit()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<TableUnitContainer>(jsonContent);
                return retVal;
            }
            return new TableUnitContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new TableUnitContainer();
        }
    }

    private static DataTableSettingContainer ReadJsonFileNewDataTableSettings()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<DataTableSettingContainer>(jsonContent);
                return retVal;
            }
            return new DataTableSettingContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new DataTableSettingContainer();
        }
    }
    private static DataTableTxtValContainer ReadJsonFileDataTableTxt()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataTableTxtValContainer retVal = JsonConvert.DeserializeObject<DataTableTxtValContainer>(jsonContent);
                return retVal;
            }
            return new DataTableTxtValContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new DataTableTxtValContainer();
        }
    }

    #endregion
}