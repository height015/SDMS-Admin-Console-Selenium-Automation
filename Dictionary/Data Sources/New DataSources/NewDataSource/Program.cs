﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SuccessLogin;
using Commons;
using Newtonsoft.Json;

namespace NewDataSource;

public class Program
{
    private static readonly string _URL = "http://197.255.51.104:9035";

    public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
   

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
                Utils.Sleep(3000);
                ClickDataSource(driver);
                Utils.Sleep(4000);
                ClickDataSourceNewRequest(driver);
                Utils.Sleep(3000);
                data.DataSourceEntry(driver);
                Utils.Sleep(3000);
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
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
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
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static bool ClickDataSourceNewRequest(IWebDriver driver)
    {
        try
        {
            var newReqBtn = driver.FindElement(By.PartialLinkText("New"));
            newReqBtn.Click();
            Utils.Sleep(3000);
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }
    public bool DataSourceEntry(IWebDriver driver)
    {
        try
        {
            var freqVal = ReadJsonFileDataSource();
            var createSec = new DataSources(driver);
            Utils.Sleep(5000);
            createSec.NewDatadataSourceEntry(freqVal.DataSource.Name, freqVal.DataSource.ShortName);
            Utils.Sleep(3000);
            createSec.ClickSubmit();
            Utils.Sleep(3000);
            createSec.ClickOk();
            Utils.LogSuccess($"Create {freqVal.DataSource.Name}", "Dictionary Data-Source");

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
    public static DataSourceContainer ReadJsonFileDataSource()
    {
        try
        {
        string jsonFileName = "DataSource.json";
        string jsonFilePath = Path.Combine(desktopPath, "SeleniumTest", jsonFileName);
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataSourceContainer retVal = JsonConvert.DeserializeObject<DataSourceContainer>(jsonContent);
                return retVal;
            }
            return new DataSourceContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new DataSourceContainer();
        }
    }

    #endregion
}
