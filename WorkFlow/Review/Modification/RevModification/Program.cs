using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ReviewAuthorization;
using SuccessLogin;
using Commons;


namespace RevModification;

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
                Utils.Sleep(3000);
                ClickModification(driver);
                Utils.Sleep(3000);
                CreateNewDataIndicatorPopUp(driver);
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
    public static void ClickModification(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Modification"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static void CreateNewDataIndicatorPopUp(IWebDriver driver)
    {
        try
        {
            var auth = new RevModificationObj(driver);
            Utils.Sleep(2000);
            JsonFileReader jsonFileReader = new();
            var retVal = jsonFileReader.ReadJsonFileWorkFlowSelection();
            auth.dropDownCat.SelectDropDownByIndex(retVal.WorkFlowSelection.CategoryIndex);
            auth.dropDownType.SelectDropDownByIndex(retVal.WorkFlowSelection.SourceTypeIndex);
            var table = auth.tblResult;
            if (table != null)
            {
                var rows = auth.rows;
                var btnRow = retVal.WorkFlowSelection.RoleIndex;
                if (btnRow > 0 && btnRow <= rows.Count)
                {
                    var desiredRow = rows[btnRow - 1];
                    var actionsButton = desiredRow.FindElement(By.CssSelector("button[data-toggle='dropdown']"));
                    actionsButton.Click();
                    Utils.Sleep(2000);
                    var RevBoxPopUp = desiredRow.FindElement(By.CssSelector("a[title='Review Item']"));
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
                    Utils.LogSuccess($"Modification", "WorkFlow -> Review");
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