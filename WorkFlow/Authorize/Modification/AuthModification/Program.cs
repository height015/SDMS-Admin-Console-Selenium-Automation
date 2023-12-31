﻿using AuthAuthorization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SuccessLogin;
using Commons;

namespace AuthModification;

public class Program
{
    public static void Main(string[] args)
    {
        using (var driver = new ChromeDriver())
        {
            var loginObj = new SuccessLogin.Program();
            var data = new Program();
            bool login = loginObj.LoginSuccess(driver);
            if (login)
            {
                ClickWorkFlow(driver);
                ClickApprovalModification(driver);
                Utils.Sleep(3000);
                ClickIndicatorPopUp(driver);
                Utils.Sleep(3000);
                ClickClose(driver);
            }
        }
    }

    public static void ClickClose(IWebDriver driver)
    {
        try
        {
            var closeBtn = driver.FindElement(By.CssSelector("a[href*='/workflow-mgt']"));
            closeBtn.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }

    public static void ClickWorkFlow(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Workflow Mgt."));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }

    #region Approvals   
    public static void ClickApprovalModification(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.CssSelector("a.card[href*='/workflow/processes/pending-approvals?reqType=5']"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static void ClickIndicatorPopUp(IWebDriver driver)
    {
        try
        {
            var auth = new AuthModificationObj(driver);
            Utils.Sleep(2000);
            JsonFileReader jsonFileReader = new();
            var retVal = jsonFileReader.ReadJsonFileWorkFlowSelection();
            var dropdownCat = new SelectElement(auth.dropDownCat);
            dropdownCat.SelectByIndex(retVal.WorkFlowSelection.CategoryIndex);
            var sourceType = new SelectElement(auth.dropDownType);
            sourceType.SelectByIndex(retVal.WorkFlowSelection.SourceTypeIndex);
            var table = auth.tblResult;
            if (table != null)
            {
                var rows = auth.rows;
                var btnRow = retVal.WorkFlowSelection.RoleIndex;
                if (btnRow >= 0 && btnRow < rows.Count)
                {
                    IWebElement desiredRow = rows[btnRow - 1];
                    IWebElement actionsButton = desiredRow.FindElement(By.CssSelector("button[data-toggle='dropdown']"));
                    actionsButton.Click();
                    Utils.Sleep(2000);
                    IWebElement RevBoxPopUp = desiredRow.FindElement(By.CssSelector("a[title='Approve Item']"));
                    RevBoxPopUp.Click();
                    Utils.Sleep(3000);
                    var retCom = jsonFileReader.ReadJsonFileWorkFlowReview();
                    auth.EnterRevComment(retCom.ReviewSelection.Comment);
                    if (retCom.ReviewSelection.Status == true)
                    {
                        auth.rdBtnApprove.Click();
                    }
                    else
                    {
                        auth.rdBtnDecline.Click();
                    }
                    Utils.Sleep(2000);
                    auth.ClickSubmit();
                    Utils.Sleep(3000);
                    auth.ClickOk();
                    Utils.LogSuccess($"Modification", "WorkFlow");

                }
            }
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    #endregion
}